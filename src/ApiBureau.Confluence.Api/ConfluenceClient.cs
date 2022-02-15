namespace ApiBureau.Confluence.Api;

public class ConfluenceClient
{
    private readonly ConfluenceSettings _settings;
    private readonly HttpClient _client;
    private const string ApiUrlPrefix = "/wiki/rest/api";

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    public ConfluenceClient(IOptions<ConfluenceSettings> settings, HttpClient client)
    {
        if (string.IsNullOrWhiteSpace(settings.Value.BaseUrl))
            throw new ArgumentNullException(nameof(settings.Value.BaseUrl), "BaseUrl is missing in the appsettings.json or secret.json in ConfluenceSettings section.");

        _settings = settings.Value;
        _client = client;

        _client.BaseAddress = new Uri(_settings.BaseUrl);
        _client.SetBasicAuthentication(_settings.Email, _settings.UserApiToken);
        //_client.DefaultRequestHeaders.Add("Authorization", BasicAuthenticationHeaderValue.EncodeCredential(_settings.Email, _settings.UserApiToken));
    }

    /// <summary>
    /// Get page content
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expand">Use body.view in view format or body.storage in storage format</param>
    /// <returns></returns>
    public async Task<ContentDto?> GetContentAsync(int id, string expand = "body.view")
    {
        var result = await _client.GetFromJsonAsync<ContentDto>($"{ApiUrlPrefix}/content/{id}?expand={expand}");

        return result;
    }

    /// <summary>
    /// Returns all spaces
    /// </summary>
    /// <returns></returns>
    public async Task<ResultDto<SpaceDto>> GetSpaceAsync()
    {
        var result = await _client.GetFromJsonAsync<ResultDto<SpaceDto>>($"{ApiUrlPrefix}/space");

        return result ?? new();
    }

    public async Task<List<ContentDto>> GetSpaceContentAsync(string key, SpaceExpand? expand = null, int limit = 100)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        expand ??= new SpaceExpand();

        var items = new List<ContentDto>();

        //var pageResult = await _client.GetFromJsonAsync<PageResultDto<ContentDto>>($"{ApiUrlPrefix}/space/{key}/content?limit={limit}{expand.Get()}");

        var pageResult = await GetAsync<ContentDto>($"{ApiUrlPrefix}/space/{key}/content?limit={limit}{expand.Get()}");

        items.AddRange(pageResult?.Page?.Results ?? new());

        var counter = 1;

        while (pageResult?.Page?.Size > 0 && pageResult?.Page?.Size == limit)
        {
            pageResult = await GetAsync<ContentDto>($"{ApiUrlPrefix}/space/{key}/content?limit={limit}&start={limit * counter}{expand.Get()}");

            items.AddRange(pageResult?.Page?.Results ?? new());

            counter++;
        }

        return items;
    }

    private Task<PageResultDto<T>?> GetAsync<T>(string url)
        => _client.GetFromJsonAsync<PageResultDto<T>>(url);

    private async Task<List<T>> GetAsync2<T>(string url, List<T> items)
    {
        var pageResult = await _client.GetFromJsonAsync<PageResultDto<T>>(url);

        if (pageResult?.Page is null) return items;

        items.AddRange(pageResult.Page.Results ?? new());

        return items;
    }
}