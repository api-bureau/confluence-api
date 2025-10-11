namespace ApiBureau.Confluence.Api.Interfaces;

public interface IConfluenceClient
{
    AttachmentEndpoint Attachment { get; }
    BlogPostEndpoint BlogPost { get; }
    ContentEndpoint Content { get; }
    SpaceEndpoint Spaces { get; }
}