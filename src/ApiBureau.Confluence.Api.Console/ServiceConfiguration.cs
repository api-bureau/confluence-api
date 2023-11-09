public static class ServiceConfiguration
{
    public static void SetupServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(context.Configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .CreateLogger();

            builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            builder.AddSerilog(logger);
        });

        services.AddConfluence(context.Configuration);
        services.AddScoped<DataService>();
    }
}