using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class PageEndpoint
{
    private const string ResourcePath = "pages";
    private readonly ConfluenceHttpClient _httpClient;

    internal PageEndpoint(ConfluenceHttpClient httpClient)
        => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public Task<PagedResponse<PageDto>?> GetPageAsync(
        string? bodyFormat = null,
        int limit = 25,
        CancellationToken cancellationToken = default)
        => _httpClient.GetPageAsync<PageDto>(
            BuildListPath(ResourcePath, bodyFormat, limit),
            cancellationToken);

    public Task<IReadOnlyList<PageDto>> GetAllAsync(
        string? bodyFormat = null,
        int limit = 250,
        CancellationToken cancellationToken = default)
        => _httpClient.GetAllAsync<PageDto>(
            BuildListPath(ResourcePath, bodyFormat, limit),
            cancellationToken);

    public Task<PagedResponse<PageDto>?> GetPageForSpaceAsync(
        string spaceId,
        string? bodyFormat = null,
        int limit = 25,
        CancellationToken cancellationToken = default)
        => _httpClient.GetPageAsync<PageDto>(
            BuildSpacePath(spaceId, bodyFormat, limit),
            cancellationToken);

    public Task<IReadOnlyList<PageDto>> GetAllForSpaceAsync(
        string spaceId,
        string? bodyFormat = null,
        int limit = 250,
        CancellationToken cancellationToken = default)
        => _httpClient.GetAllAsync<PageDto>(
            BuildSpacePath(spaceId, bodyFormat, limit),
            cancellationToken);

    public Task<PageDto?> GetByIdAsync(
        string pageId,
        string bodyFormat = "view",
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        ArgumentException.ThrowIfNullOrWhiteSpace(bodyFormat);
        return _httpClient.GetAsync<PageDto>(
            QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(pageId)}", "body-format", bodyFormat),
            cancellationToken);
    }

    public Task<IReadOnlyList<ContentPropertyDto>> GetAllPropertiesAsync(
        string pageId,
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);
        var path = QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(pageId)}/properties", "limit", limit.ToString());
        return _httpClient.GetAllAsync<ContentPropertyDto>(path, cancellationToken);
    }

    private static string BuildSpacePath(string spaceId, string? bodyFormat, int limit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(spaceId);
        return BuildListPath($"spaces/{Uri.EscapeDataString(spaceId)}/pages", bodyFormat, limit);
    }

    private static string BuildListPath(string path, string? bodyFormat, int limit)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);

        var query = new Dictionary<string, string?>
        {
            ["limit"] = limit.ToString()
        };
        if (!string.IsNullOrWhiteSpace(bodyFormat)) query["body-format"] = bodyFormat;
        return QueryHelpers.AddQueryString(path, query);
    }
}