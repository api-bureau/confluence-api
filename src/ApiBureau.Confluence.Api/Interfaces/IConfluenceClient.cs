namespace ApiBureau.Confluence.Api.Interfaces;

public interface IConfluenceClient
{
    SpaceEndpoint Spaces { get; }
    PageEndpoint Pages { get; }
    BlogPostEndpoint BlogPosts { get; }
    AttachmentEndpoint Attachments { get; }
    UserEndpoint Users { get; }
}