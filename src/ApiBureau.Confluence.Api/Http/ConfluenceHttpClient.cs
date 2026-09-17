using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ApiBureau.Confluence.Api.Http;

public sealed class ConfluenceHttpClient
{
    private const string ApiPrefix = "wiki/api/v2";

    private readonly HttpClient _client;
    private readonly ILogger<ConfluenceHttpClient> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    public ConfluenceHttpClient(HttpClient client, ILogger<ConfluenceHttpClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string path, CancellationToken token = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, BuildRequestPath(path));
        using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.NoContent) return default;

        await EnsureSuccessAsync(response, token).ConfigureAwait(false);

        try
        {
            return await response.Content.ReadFromJsonAsync<T>(_jsonOptions, token).ConfigureAwait(false);
        }
        catch (JsonException exception)
        {
            _logger.LogError(exception, "Failed to deserialize Confluence v2 response from {Path}", path);
            throw;
        }
    }

    public Task<PagedResponse<T>?> GetPageAsync<T>(string path, CancellationToken token = default)
        => GetAsync<PagedResponse<T>>(path, token);

    public async Task<IReadOnlyList<T>> GetAllAsync<T>(string path, CancellationToken token = default)
    {
        var items = new List<T>();
        var visitedPaths = new HashSet<string>(StringComparer.Ordinal);
        string? next = path;

        while (!string.IsNullOrWhiteSpace(next) && visitedPaths.Add(next))
        {
            var page = await GetPageAsync<T>(next, token).ConfigureAwait(false);

            if (page is null) break;

            items.AddRange(page.Results);
            next = page.Links?.Next;
        }

        return items;
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string path, TRequest content, CancellationToken token = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, BuildRequestPath(path))
        {
            Content = JsonContent.Create(content, options: _jsonOptions)
        };
        using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.NoContent) return default;

        await EnsureSuccessAsync(response, token).ConfigureAwait(false);

        return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, token).ConfigureAwait(false);
    }

    public async Task<Stream> GetStreamAsync(string path, CancellationToken token = default)
        => await _client.GetStreamAsync(BuildRequestPath(path), token).ConfigureAwait(false);

    private static string BuildRequestPath(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        if (Uri.TryCreate(path, UriKind.Absolute, out _)) return path;

        var isRootRelativeLink = path.StartsWith('/');
        var relativePath = path.TrimStart('/');

        if (relativePath.StartsWith("wiki/", StringComparison.OrdinalIgnoreCase))
        {
            return relativePath;
        }

        if (isRootRelativeLink)
        {
            return $"wiki/{relativePath}";
        }

        return $"{ApiPrefix}/{relativePath}";
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken token)
    {
        if (response.IsSuccessStatusCode) return;

        var responseBody = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);

        throw new ConfluenceApiException(response.StatusCode, response.ReasonPhrase, responseBody);
    }
}