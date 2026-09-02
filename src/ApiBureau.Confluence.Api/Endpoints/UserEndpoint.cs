namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class UserEndpoint : BaseEndpoint
{
    public UserEndpoint(ConfluenceHttpClient httpClient) : base(httpClient) { }

    public async Task<List<UserDto>> GetByIdsAsync(IEnumerable<string> accountIds, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(accountIds);

        var ids = accountIds.Where(id => !string.IsNullOrWhiteSpace(id)).Distinct(StringComparer.Ordinal).ToArray();
        if (ids.Length == 0) return [];

        var response = await HttpClient.PostAsync<object, PagedResponse<UserDto>>(
            "users-bulk",
            new { accountIds = ids },
            token).ConfigureAwait(false);

        return response?.Results ?? [];
    }
}