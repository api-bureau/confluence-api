using ApiBureau.Confluence.ApiV2.Console.Services;
using ApiBureau.Confluence.ApiV2.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddSerilog(configuration => configuration.ReadFrom.Configuration(builder.Configuration));
builder.Services.AddConfluenceV2(builder.Configuration);
builder.Services.AddScoped<ConfluenceV2ConsoleService>();

using var host = builder.Build();
using var scope = host.Services.CreateScope();

var service = scope.ServiceProvider.GetRequiredService<ConfluenceV2ConsoleService>();
return await service.RunAsync(args);