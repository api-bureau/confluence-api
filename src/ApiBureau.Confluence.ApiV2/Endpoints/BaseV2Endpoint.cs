namespace ApiBureau.Confluence.ApiV2.Endpoints;

public abstract class BaseV2Endpoint
{
    protected ConfluenceV2HttpClient HttpClient { get; }

    protected BaseV2Endpoint(ConfluenceV2HttpClient httpClient) => HttpClient = httpClient;
}