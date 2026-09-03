namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class UserEndpoint
{
    private readonly ConfluenceHttpClient _httpClient;

    internal UserEndpoint(ConfluenceHttpClient httpClient)
        => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<IReadOnlyList<UserDto>> GetByIdsAsync(
        IEnumerable<string> accountIds,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accountIds);

        var ids = accountIds.Where(id => !string.IsNullOrWhiteSpace(id)).Distinct(StringComparer.Ordinal).ToArray();
        if (ids.Length == 0) return [];

        var response = await _httpClient.PostAsync<object, PagedResponse<UserDto>>(
            "users-bulk",
            new { accountIds = ids },
            cancellationToken).ConfigureAwait(false);

        return response?.Results ?? [];
    }
}
