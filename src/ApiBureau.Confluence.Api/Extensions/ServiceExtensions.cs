using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace ApiBureau.Confluence.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddConfluence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConfluenceSettings>(options => configuration.GetSection(nameof(ConfluenceSettings)).Bind(options));

        services.AddHttpClient<ConfluenceClient>()
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(20))
            .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(3) }));

        return services;
    }
}