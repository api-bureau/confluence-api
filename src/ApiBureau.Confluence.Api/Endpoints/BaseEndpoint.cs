namespace ApiBureau.Confluence.Api.Endpoints;

public class BaseEndpoint
{
    protected ConfluenceHttpClient ApiConnection { get; private set; }

    public BaseEndpoint(ConfluenceHttpClient apiConnection) => ApiConnection = apiConnection;
}