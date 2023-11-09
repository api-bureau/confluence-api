using ApiBureau.Confluence.Api.Core;

namespace ApiBureau.Confluence.Api.Console.Services;

public class DataService
{
    private readonly ConfluenceClient _client;
    private readonly ILogger<DataService> _logger;

    public DataService(ConfluenceClient client, ILogger<DataService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        var spaces = await GetSpacesAsync();

        foreach (var space in spaces)
        {
            _logger.LogInformation(space.Name);

            if (space.Name == "MySpacke") // e.g Tech
            {
                var content = await GetContentDtoAsync(space.Key);
            }
        }
    }

    public async Task<List<SpaceDto>> GetSpacesAsync()
    {
        var items = await _client.Spaces.GetAsync();

        return items.Results;
    }

    public async Task<List<ContentDto>> GetContentDtoAsync(string key)
    {
        var expand = new SpaceExpand().AddBody().AddVersion().AddAncestors().AddChildren();

        var items = await _client.Spaces.GetContentAsync(key, expand, 100);

        return items;
    }
}