namespace ApiBureau.Confluence.Api;

public class ConfluenceClient : IConfluenceClient
{
    public AttachmentEndpoint Attachment { get; }
    public BlogPostEndpoint BlogPost { get; }
    public ContentEndpoint Content { get; }
    public SpaceEndpoint Spaces { get; }

    public ConfluenceClient(ConfluenceHttpClient apiConnection)
    {
        Attachment = new AttachmentEndpoint(apiConnection);
        BlogPost = new BlogPostEndpoint(apiConnection);
        Content = new ContentEndpoint(apiConnection);
        Spaces = new SpaceEndpoint(apiConnection);
    }
}