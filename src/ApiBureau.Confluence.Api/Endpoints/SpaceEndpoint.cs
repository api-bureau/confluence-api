using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class SpaceEndpoint : BaseEndpoint
{
    private const string ResourcePath = "spaces";

    public SpaceEndpoint(ConfluenceHttpClient httpClient) : base(httpClient) { }

    public Task<PagedResponse<SpaceDto>?> GetAsync(int limit = 25, string? key = null, CancellationToken token = default)
        => HttpClient.GetPageAsync<SpaceDto>(BuildListPath(limit, key), token);

    public Task<List<SpaceDto>> GetAllAsync(int limit = 250, string? key = null, CancellationToken token = default)
        => HttpClient.GetAllAsync<SpaceDto>(BuildListPath(limit, key), token);

    public Task<SpaceDto?> GetByIdAsync(string spaceId, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(spaceId);
        return HttpClient.GetAsync<SpaceDto>($"{ResourcePath}/{Uri.EscapeDataString(spaceId)}", token);
    }

    private static string BuildListPath(int limit, string? key)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);

        var query = new Dictionary<string, string?> { ["limit"] = limit.ToString() };
        if (!string.IsNullOrWhiteSpace(key)) query["keys"] = key;

        return QueryHelpers.AddQueryString(ResourcePath, query);
    }
}