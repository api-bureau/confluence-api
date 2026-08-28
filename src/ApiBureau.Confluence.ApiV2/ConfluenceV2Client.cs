namespace ApiBureau.Confluence.ApiV2;

public sealed class ConfluenceV2Client : IConfluenceV2Client
{
    public SpaceV2Endpoint Spaces { get; }
    public PageV2Endpoint Pages { get; }
    public BlogPostV2Endpoint BlogPosts { get; }
    public AttachmentV2Endpoint Attachments { get; }
    public UserV2Endpoint Users { get; }

    public ConfluenceV2Client(ConfluenceV2HttpClient httpClient)
    {
        Spaces = new SpaceV2Endpoint(httpClient);
        Pages = new PageV2Endpoint(httpClient);
        BlogPosts = new BlogPostV2Endpoint(httpClient);
        Attachments = new AttachmentV2Endpoint(httpClient);
        Users = new UserV2Endpoint(httpClient);
    }
}