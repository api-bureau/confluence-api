using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class PageEndpoint : BaseEndpoint
{
    private const string ResourcePath = "pages";

    public PageEndpoint(ConfluenceHttpClient httpClient) : base(httpClient) { }

    public Task<PagedResponse<PageDto>?> GetAsync(string? bodyFormat = null, int limit = 25, CancellationToken token = default)
        => HttpClient.GetPageAsync<PageDto>(BuildListPath(ResourcePath, bodyFormat, limit), token);

    public Task<List<PageDto>> GetAllAsync(string? bodyFormat = null, int limit = 250, CancellationToken token = default)
        => HttpClient.GetAllAsync<PageDto>(BuildListPath(ResourcePath, bodyFormat, limit), token);

    public Task<PagedResponse<PageDto>?> GetForSpaceAsync(string spaceId, string? bodyFormat = null, int limit = 25, CancellationToken token = default)
        => HttpClient.GetPageAsync<PageDto>(BuildSpacePath(spaceId, bodyFormat, limit), token);

    public Task<List<PageDto>> GetAllForSpaceAsync(string spaceId, string? bodyFormat = null, int limit = 250, CancellationToken token = default)
        => HttpClient.GetAllAsync<PageDto>(BuildSpacePath(spaceId, bodyFormat, limit), token);

    public Task<PageDto?> GetByIdAsync(string pageId, string bodyFormat = "view", CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        return HttpClient.GetAsync<PageDto>(QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(pageId)}", "body-format", bodyFormat), token);
    }

    public Task<List<ContentPropertyDto>> GetPropertiesAsync(string pageId, int limit = 100, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        var path = QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(pageId)}/properties", "limit", limit.ToString());
        return HttpClient.GetAllAsync<ContentPropertyDto>(path, token);
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