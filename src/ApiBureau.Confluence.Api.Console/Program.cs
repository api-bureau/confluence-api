var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((_, config) => AppConfigurationBuilder.SetupConfiguration(args, config))
        .ConfigureServices(ServiceConfiguration.SetupServices)
        .Build();

await RunApplication(hostBuilder, args);

static async Task RunApplication(IHost hostBuilder, string[] args)
{
    var serviceScopeFactory = hostBuilder.Services.GetService<IServiceScopeFactory>();

    using var scope = serviceScopeFactory?.CreateScope();

    var service = scope?.ServiceProvider.GetService<DataService>();

    if (service is null)
    {
        Console.WriteLine($"Failed to resolve {nameof(DataService)} from the service provider. Please ensure it is registered in the service collection.");

        return;
    }

    await service.RunAsync();
}