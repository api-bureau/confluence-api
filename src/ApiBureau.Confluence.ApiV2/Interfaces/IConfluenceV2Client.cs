namespace ApiBureau.Confluence.ApiV2.Interfaces;

public interface IConfluenceV2Client
{
    SpaceV2Endpoint Spaces { get; }
    PageV2Endpoint Pages { get; }
    BlogPostV2Endpoint BlogPosts { get; }
    AttachmentV2Endpoint Attachments { get; }
    UserV2Endpoint Users { get; }
}
