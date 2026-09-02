using ApiBureau.Confluence.Api.Console.Services;
using ApiBureau.Confluence.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddSerilog(configuration => configuration.ReadFrom.Configuration(builder.Configuration));
builder.Services.AddConfluence(builder.Configuration);
builder.Services.AddScoped<ConfluenceConsoleService>();

using var host = builder.Build();
using var scope = host.Services.CreateScope();

var service = scope.ServiceProvider.GetRequiredService<ConfluenceConsoleService>();
return await service.RunAsync(args);