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

        private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
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

        public async Task<ContentDto?> GetContentAsync(int id, string expand = "body.view")
        {
            var result = await _client.GetFromJsonAsync<ContentDto>($"/wiki/rest/api/content/{id}?expand={expand}");

            return result;
        }
    }
}
