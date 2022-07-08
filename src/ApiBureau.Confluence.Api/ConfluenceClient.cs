using ApiBureau.Confluence.Api.Endpoints;

namespace ApiBureau.Confluence.Api;

public class ConfluenceClient
{
    private readonly HttpHelper _httpHelper;

    public AttachmentEndpoint Attachment { get; set; }
    public ContentEndpoint Content { get; set; }
    public SpaceEndpoint Spaces { get; set; }

    public ConfluenceClient(HttpClient client, IOptions<ConfluenceSettings> settings)
    {
        _httpHelper = new HttpHelper(client, settings);

        Attachment = new AttachmentEndpoint(_httpHelper);
        Content = new ContentEndpoint(_httpHelper);
        Spaces = new SpaceEndpoint(_httpHelper);
    }
}