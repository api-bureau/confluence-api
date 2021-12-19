using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace ApiBureau.Confluence.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCloudCall(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfluenceClient>(options => configuration.GetSection(nameof(ConfluenceClient)).Bind(options));

            services.AddHttpClient<ConfluenceClient>()
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(20))
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(3) }));

            return services;
        }
    }
}
