using ApiBureau.Confluence.ApiV2.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ApiBureau.Confluence.ApiV2.Console.Services;

public sealed class ConfluenceV2ConsoleService
{
    private readonly IConfluenceV2Client _client;
    private readonly ILogger<ConfluenceV2ConsoleService> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web) { WriteIndented = true };

    public ConfluenceV2ConsoleService(IConfluenceV2Client client, ILogger<ConfluenceV2ConsoleService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<int> RunAsync(string[] args, CancellationToken token = default)
    {
        if (args.Length == 0 || IsHelp(args[0]))
        {
            WriteHelp();
            return 0;
        }

        try
        {
            switch (args[0].ToLowerInvariant())
            {
                case "spaces":
                    await WriteJsonAsync(await _client.Spaces.GetAllAsync(key: GetOptional(args, 1), token: token));
                    break;
                case "space":
                    await WriteJsonAsync(await _client.Spaces.GetByIdAsync(GetRequired(args, 1, "space id"), token));
                    break;
                case "space-pages":
                    await WriteJsonAsync(await _client.Pages.GetAllForSpaceAsync(GetRequired(args, 1, "space id"), token: token));
                    break;
                case "page":
                    await WriteJsonAsync(await _client.Pages.GetByIdAsync(GetRequired(args, 1, "page id"), token: token));
                    break;
                case "inspect-page":
                    await InspectPageAsync(GetRequired(args, 1, "page id"), token);
                    break;
                case "properties":
                    await WriteJsonAsync(await _client.Pages.GetPropertiesAsync(GetRequired(args, 1, "page id"), token: token));
                    break;
                case "attachments":
                    await WriteJsonAsync(await _client.Attachments.GetAllForPageAsync(GetRequired(args, 1, "page id"), token: token));
                    break;
                case "blogposts":
                    var spaceId = GetOptional(args, 1);
                    var posts = string.IsNullOrWhiteSpace(spaceId)
                        ? await _client.BlogPosts.GetAllAsync(token: token)
                        : await _client.BlogPosts.GetAllForSpaceAsync(spaceId, token: token);
                    await WriteJsonAsync(posts);
                    break;
                case "download":
                    await DownloadAsync(
                        GetRequired(args, 1, "attachment id"),
                        GetRequired(args, 2, "output file"),
                        token);
                    break;
                default:
                    System.Console.Error.WriteLine($"Unknown command '{args[0]}'.");
                    WriteHelp();
                    return 1;
            }

            return 0;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Confluence v2 command failed");
            return 1;
        }
    }

    private async Task InspectPageAsync(string pageId, CancellationToken token)
    {
        var page = await _client.Pages.GetByIdAsync(pageId, token: token);

        await WriteJsonAsync(page);

        System.Console.WriteLine();
        System.Console.WriteLine("----- BODY.VIEW HTML -----");
        System.Console.WriteLine(page?.Body?.View?.Value ?? "<no body.view returned>");
        System.Console.WriteLine("----- END BODY.VIEW HTML -----");

        var properties = await _client.Pages.GetPropertiesAsync(pageId, token: token);
        var attachments = await _client.Attachments.GetAllForPageAsync(pageId, token: token);
        var authorIds = new[] { page?.AuthorId, page?.Version?.AuthorId }
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id!)
            .Distinct(StringComparer.Ordinal);
        var users = await _client.Users.GetByIdsAsync(authorIds, token);

        System.Console.WriteLine();
        System.Console.WriteLine("----- RELATED DATA -----");
        await WriteJsonAsync(new { properties, attachments, users });
    }

    private async Task DownloadAsync(string attachmentId, string outputFile, CancellationToken token)
    {
        await using var source = await _client.Attachments.DownloadAsync(attachmentId, token);
        await using var target = File.Create(outputFile);
        await source.CopyToAsync(target, token);
        System.Console.WriteLine($"Downloaded attachment {attachmentId} to {Path.GetFullPath(outputFile)}");
    }

    private Task WriteJsonAsync<T>(T value)
    {
        System.Console.WriteLine(JsonSerializer.Serialize(value, _jsonOptions));
        return Task.CompletedTask;
    }

    private static string GetRequired(string[] args, int index, string name)
        => args.Length > index && !string.IsNullOrWhiteSpace(args[index])
            ? args[index]
            : throw new ArgumentException($"Missing required {name}.");

    private static string? GetOptional(string[] args, int index)
        => args.Length > index && !string.IsNullOrWhiteSpace(args[index]) ? args[index] : null;

    private static bool IsHelp(string value)
        => value.Equals("help", StringComparison.OrdinalIgnoreCase) || value is "-h" or "--help";

    private static void WriteHelp()
    {
        System.Console.WriteLine("Confluence REST API v2 inspection console");
        System.Console.WriteLine();
        System.Console.WriteLine("  spaces [key]");
        System.Console.WriteLine("  space <space-id>");
        System.Console.WriteLine("  space-pages <space-id>");
        System.Console.WriteLine("  page <page-id>");
        System.Console.WriteLine("  inspect-page <page-id>");
        System.Console.WriteLine("  properties <page-id>");
        System.Console.WriteLine("  attachments <page-id>");
        System.Console.WriteLine("  blogposts [space-id]");
        System.Console.WriteLine("  download <attachment-id> <output-file>");
    }
}