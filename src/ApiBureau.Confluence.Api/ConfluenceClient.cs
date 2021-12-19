using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ApiBureau.Confluence.Api
{
    public class ConfluenceClient
    {
        private readonly ConfluenceSettings _settings;
        private readonly HttpClient _client;
        private string? _accessToken;
        //private DateTime? _tokenExpireTime;
        private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public ConfluenceClient(IOptions<ConfluenceSettings> settings, HttpClient client)
        {
            _settings = settings.Value;
            _client = client;

            _client.DefaultRequestHeaders.Add("LicenseKey", _settings.LicenseKey);
        }

        public async Task AuthenticateAsync()
        {
            var request = new PasswordTokenRequest
            {
                UserName = _settings.UserName,
                Password = _settings.Password,
                Address = _settings.BaseUrl + "/auth/login" 
            };

            var token = await _client.RequestPasswordTokenAsync(request);

            if (token is null) return;

            _accessToken = token.AccessToken;

            _client.SetBearerToken(_accessToken);
        }
    }
}
