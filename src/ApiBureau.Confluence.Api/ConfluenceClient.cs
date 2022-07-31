using ApiBureau.Confluence.Api.Endpoints;
using ApiBureau.Confluence.Api.Http;

namespace ApiBureau.Confluence.Api;

public class ConfluenceClient
{
    private readonly ApiConnection _apiConnection;

    public AttachmentEndpoint Attachment { get; set; }
    public ContentEndpoint Content { get; set; }
    public SpaceEndpoint Spaces { get; set; }

    public ConfluenceClient(HttpClient client, IOptions<ConfluenceSettings> settings)
    {
        _apiConnection = new ApiConnection(client, settings);

        Attachment = new AttachmentEndpoint(_apiConnection);
        Content = new ContentEndpoint(_apiConnection);
        Spaces = new SpaceEndpoint(_apiConnection);
    }
}