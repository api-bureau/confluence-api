using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.ApiV2.Endpoints;

public sealed class SpaceV2Endpoint : BaseV2Endpoint
{
    private const string ResourcePath = "spaces";

    public SpaceV2Endpoint(ConfluenceV2HttpClient httpClient) : base(httpClient) { }

    public Task<PagedV2Response<SpaceV2Dto>?> GetAsync(int limit = 25, string? key = null, CancellationToken token = default)
        => HttpClient.GetPageAsync<SpaceV2Dto>(BuildListPath(limit, key), token);

    public Task<List<SpaceV2Dto>> GetAllAsync(int limit = 250, string? key = null, CancellationToken token = default)
        => HttpClient.GetAllAsync<SpaceV2Dto>(BuildListPath(limit, key), token);

    public Task<SpaceV2Dto?> GetByIdAsync(string spaceId, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(spaceId);
        return HttpClient.GetAsync<SpaceV2Dto>($"{ResourcePath}/{Uri.EscapeDataString(spaceId)}", token);
    }

    private static string BuildListPath(int limit, string? key)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);

        var query = new Dictionary<string, string?> { ["limit"] = limit.ToString() };
        if (!string.IsNullOrWhiteSpace(key)) query["keys"] = key;

        return QueryHelpers.AddQueryString(ResourcePath, query);
    }
}