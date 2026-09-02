using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using System.Net.Http.Headers;
using System.Text;

namespace ApiBureau.Confluence.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddConfluence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ConfluenceSettings>()
            .Bind(configuration.GetSection(nameof(ConfluenceSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddHttpClient<ConfluenceHttpClient>((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<ConfluenceSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl.TrimEnd('/') + "/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{settings.Email}:{settings.UserApiToken}")));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd("ApiBureau.Confluence.Api/1.0");
            })
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30)))
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(
            [
                TimeSpan.FromMilliseconds(200),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3)
            ]));

        services.AddSingleton<IConfluenceClient, ConfluenceClient>();

        return services;
    }
}