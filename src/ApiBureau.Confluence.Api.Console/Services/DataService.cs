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
        var dtos = await GetSpacesAsync();

        foreach (var dto in dtos)
        {
            _logger.LogInformation(dto.Name);
        }
    }

    public async Task<List<SpaceDto>> GetSpacesAsync()
    {
        var items = await _client.Spaces.GetAsync();

        return items.Results;
    }
}