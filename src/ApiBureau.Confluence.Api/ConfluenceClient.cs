using ApiBureau.Confluence.Api.Core;
using ApiBureau.Confluence.Api.Dtos;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace ApiBureau.Confluence.Api
{
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

        public async Task<ResultDto<ContentDto>> GetSpaceContentsAsync(string key, SpaceExpand? expand = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var result = await _client.GetFromJsonAsync<ResultDto<ContentDto>>($"{ApiUrlPrefix}/space/{key}/content{expand?.Get() ?? ""}");

            return result ?? new();
        }
    }
}
