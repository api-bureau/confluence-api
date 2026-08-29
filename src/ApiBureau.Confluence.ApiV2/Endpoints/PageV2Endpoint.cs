using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.ApiV2.Endpoints;

public sealed class PageV2Endpoint : BaseV2Endpoint
{
    private const string ResourcePath = "pages";

    public PageV2Endpoint(ConfluenceV2HttpClient httpClient) : base(httpClient) { }

    public Task<PagedV2Response<PageV2Dto>?> GetAsync(string? bodyFormat = null, int limit = 25, CancellationToken token = default)
        => HttpClient.GetPageAsync<PageV2Dto>(BuildListPath(ResourcePath, bodyFormat, limit), token);

    public Task<List<PageV2Dto>> GetAllAsync(string? bodyFormat = null, int limit = 250, CancellationToken token = default)
        => HttpClient.GetAllAsync<PageV2Dto>(BuildListPath(ResourcePath, bodyFormat, limit), token);

    public Task<PagedV2Response<PageV2Dto>?> GetForSpaceAsync(string spaceId, string? bodyFormat = null, int limit = 25, CancellationToken token = default)
        => HttpClient.GetPageAsync<PageV2Dto>(BuildSpacePath(spaceId, bodyFormat, limit), token);

    public Task<List<PageV2Dto>> GetAllForSpaceAsync(string spaceId, string? bodyFormat = null, int limit = 250, CancellationToken token = default)
        => HttpClient.GetAllAsync<PageV2Dto>(BuildSpacePath(spaceId, bodyFormat, limit), token);

    public Task<PageV2Dto?> GetByIdAsync(string pageId, string bodyFormat = "view", CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        return HttpClient.GetAsync<PageV2Dto>(QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(pageId)}", "body-format", bodyFormat), token);
    }

    public Task<List<ContentPropertyV2Dto>> GetPropertiesAsync(string pageId, int limit = 100, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        var path = QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(pageId)}/properties", "limit", limit.ToString());
        return HttpClient.GetAllAsync<ContentPropertyV2Dto>(path, token);
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
