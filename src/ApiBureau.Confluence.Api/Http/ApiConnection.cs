namespace ApiBureau.Confluence.Api.Http;

public class ApiConnection
{
    private readonly HttpClient _client;
    private readonly ConfluenceSettings _settings;
    private const string ApiUrlPrefix = "/wiki/rest/api";

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    public ApiConnection(HttpClient httpClient, IOptions<ConfluenceSettings> settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Value.BaseUrl))
            throw new ArgumentNullException(nameof(settings.Value.BaseUrl), "BaseUrl is missing in the appsettings.json or secret.json in ConfluenceSettings section.");

        _settings = settings.Value;
        _client = httpClient;

        _client.BaseAddress = new Uri(_settings.BaseUrl);
        _client.SetBasicAuthentication(_settings.Email, _settings.UserApiToken);

        //_client.DefaultRequestHeaders.Add("Authorization", BasicAuthenticationHeaderValue.EncodeCredential(_settings.Email, _settings.UserApiToken));
    }

    public Task<Stream> GetStreamAsync(string url)
        => _client.GetStreamAsync($"{ApiUrlPrefix}/{url}");

    public Task<ResultDto<T>?> GetResultAsync<T>(string url)
        => _client.GetFromJsonAsync<ResultDto<T>>($"{ApiUrlPrefix}/{url}");

    public Task<PageResultDto<T>?> GetPageResultAsync<T>(string url)
        => _client.GetFromJsonAsync<PageResultDto<T>>(url);

    public Task<T?> GetAsync<T>(string url)
        => _client.GetFromJsonAsync<T>($"{ApiUrlPrefix}/{url}");

    /// <summary>
    /// Returns space content entities
    /// </summary>
    /// <param name="key"></param>
    /// <param name="expand"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<List<T>> GetSpaceContentAsync<T>(string url, string expand = "", int limit = 100)
    {
        var items = new List<T>();

        var pageResult = await GetPageResultAsync<T>($"{ApiUrlPrefix}/{url}?limit={limit}{expand}");

        items.AddRange(pageResult?.Page?.Results ?? new());

        var counter = 1;

        while (pageResult?.Page?.Size > 0 && pageResult?.Page?.Size == limit)
        {
            pageResult = await GetPageResultAsync<T>($"{ApiUrlPrefix}/{url}?limit={limit}&start={limit * counter}{expand}");

            items.AddRange(pageResult?.Page?.Results ?? new());

            counter++;
        }

        return items;
    }

    //private Task<PageResultDto<T>?> GetAsync<T>(string url)
    //    => _client.GetFromJsonAsync<PageResultDto<T>>(url);

    //private async Task<List<T>> GetAsync2<T>(string url, List<T> items)
    //{
    //    var pageResult = await _client.GetFromJsonAsync<PageResultDto<T>>(url);

    //    if (pageResult?.Page is null) return items;

    //    items.AddRange(pageResult.Page.Results ?? new());

    //    return items;
    //}
}
