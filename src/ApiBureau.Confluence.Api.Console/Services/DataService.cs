using ApiBureau.Confluence.Api.Core;
using ApiBureau.Confluence.Api.Interfaces;

namespace ApiBureau.Confluence.Api.Console.Services;

public class DataService
{
    private readonly IConfluenceClient _client;
    private readonly ILogger<DataService> _logger;

    public DataService(IConfluenceClient client, ILogger<DataService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        //await GetBlogPostsAsync();

        //return;

        var spaces = await GetSpacesAsync();

        foreach (var space in spaces)
        {
            _logger.LogInformation(space.Name);

            if (space.Name == "Intranet") // e.g Tech
            {
                var content = await GetContentDtoAsync(space.Key);

                foreach (var item in content)
                {
                    _logger.LogInformation(" - {Title} ({Type})", item.Title, item.Type);
                }
            }
        }
    }

    public async Task<List<SpaceDto>> GetSpacesAsync()
    {
        var items = await _client.Spaces.GetAsync();

        return items.Results;
    }

    public async Task GetBlogPostsAsync()
    {
        var result = await _client.BlogPost.GetAsync();

        _logger.LogInformation("Total blog posts: {Total}", result?.Results.Count);

        _logger.LogInformation("Blog posts: {BlogPosts}", result?.Results.Select(x => x.Title).ToList());
    }

    public async Task<List<ContentDto>> GetContentDtoAsync(string key)
    {
        var expand = new SpaceExpand().AddBody().AddVersion().AddAncestors().AddChildren();

        var items = await _client.Spaces.GetContentAsync(key, expand, 100);

        return items;
    }
}