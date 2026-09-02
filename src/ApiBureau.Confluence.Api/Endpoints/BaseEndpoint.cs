namespace ApiBureau.Confluence.Api.Endpoints;

public abstract class BaseEndpoint
{
    protected ConfluenceHttpClient HttpClient { get; }

    protected BaseEndpoint(ConfluenceHttpClient httpClient) => HttpClient = httpClient;
}