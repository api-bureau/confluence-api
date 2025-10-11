using ApiBureau.Confluence.Api.Endpoints;

namespace ApiBureau.Confluence.Api;

public class ConfluenceClient
{
    public AttachmentEndpoint Attachment { get; }
    public BlogPostEndpoint BlogPost { get; }
    public ContentEndpoint Content { get; }
    public SpaceEndpoint Spaces { get; }

    public ConfluenceClient(ApiConnection apiConnection)
    {
        Attachment = new AttachmentEndpoint(apiConnection);
        BlogPost = new BlogPostEndpoint(apiConnection);
        Content = new ContentEndpoint(apiConnection);
        Spaces = new SpaceEndpoint(apiConnection);
    }
}