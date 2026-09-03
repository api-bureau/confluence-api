using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class SpaceEndpoint
{
    private const string ResourcePath = "spaces";
    private readonly ConfluenceHttpClient _httpClient;

    internal SpaceEndpoint(ConfluenceHttpClient httpClient)
        => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public Task<PagedResponse<SpaceDto>?> GetPageAsync(
        int limit = 25,
        string? key = null,
        CancellationToken cancellationToken = default)
        => _httpClient.GetPageAsync<SpaceDto>(BuildListPath(limit, key), cancellationToken);

    public Task<IReadOnlyList<SpaceDto>> GetAllAsync(
        int limit = 250,
        string? key = null,
        CancellationToken cancellationToken = default)
        => _httpClient.GetAllAsync<SpaceDto>(BuildListPath(limit, key), cancellationToken);

    public Task<SpaceDto?> GetByIdAsync(string spaceId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(spaceId);
        return _httpClient.GetAsync<SpaceDto>(
            $"{ResourcePath}/{Uri.EscapeDataString(spaceId)}",
            cancellationToken);
    }

    private static string BuildListPath(int limit, string? key)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);

        var query = new Dictionary<string, string?> { ["limit"] = limit.ToString() };
        if (!string.IsNullOrWhiteSpace(key)) query["keys"] = key;

        return QueryHelpers.AddQueryString(ResourcePath, query);
    }
}