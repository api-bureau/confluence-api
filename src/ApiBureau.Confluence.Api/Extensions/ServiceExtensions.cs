using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net.Http.Headers;

namespace ApiBureau.Confluence.Api.Extensions;

/// <summary>
/// Dependency injection helpers for registering the Confluence API client.
/// </summary>
public static class ServiceExtensions
{
    public static IServiceCollection AddConfluence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ConfluenceSettings>()
            .Bind(configuration.GetSection(nameof(ConfluenceSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient<ConfluenceHttpClient>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<ConfluenceSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{settings.Email}:{settings.UserApiToken}")));
                client.DefaultRequestHeaders.UserAgent.ParseAdd("ApiBureau.Confluence.Api/1.0");
            })
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(20))
            .AddTransientHttpErrorPolicy(pb => pb.WaitAndRetryAsync(
            [
                TimeSpan.FromMilliseconds(200),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3)
            ]));

        services.AddSingleton<IConfluenceClient, ConfluenceClient>();

        return services;
    }
}