using ApiBureau.Confluence.Api.Endpoints;

namespace ApiBureau.Confluence.Api;

public class ConfluenceClient
{
    private readonly ApiConnection _apiConnection;

    public AttachmentEndpoint Attachment { get; }
    public ContentEndpoint Content { get; }
    public SpaceEndpoint Spaces { get; }

    public ConfluenceClient(HttpClient client, IOptions<ConfluenceSettings> settings)
    {
        _apiConnection = new ApiConnection(client, settings);

        Attachment = new AttachmentEndpoint(_apiConnection);
        Content = new ContentEndpoint(_apiConnection);
        Spaces = new SpaceEndpoint(_apiConnection);
    }
}