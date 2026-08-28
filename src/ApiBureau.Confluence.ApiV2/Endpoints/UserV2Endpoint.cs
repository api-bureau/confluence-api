namespace ApiBureau.Confluence.ApiV2.Endpoints;

public sealed class UserV2Endpoint : BaseV2Endpoint
{
    public UserV2Endpoint(ConfluenceV2HttpClient httpClient) : base(httpClient) { }

    public async Task<List<UserV2Dto>> GetByIdsAsync(IEnumerable<string> accountIds, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(accountIds);

        var ids = accountIds.Where(id => !string.IsNullOrWhiteSpace(id)).Distinct(StringComparer.Ordinal).ToArray();
        if (ids.Length == 0) return [];

        var response = await HttpClient.PostAsync<object, PagedV2Response<UserV2Dto>>(
            "users-bulk",
            new { accountIds = ids },
            token).ConfigureAwait(false);

        return response?.Results ?? [];
    }
}