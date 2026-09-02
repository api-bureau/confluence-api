namespace ApiBureau.Confluence.Api;

public sealed class ConfluenceClient : IConfluenceClient
{
    public SpaceEndpoint Spaces { get; }
    public PageEndpoint Pages { get; }
    public BlogPostEndpoint BlogPosts { get; }
    public AttachmentEndpoint Attachments { get; }
    public UserEndpoint Users { get; }

    public ConfluenceClient(ConfluenceHttpClient httpClient)
    {
        Spaces = new SpaceEndpoint(httpClient);
        Pages = new PageEndpoint(httpClient);
        BlogPosts = new BlogPostEndpoint(httpClient);
        Attachments = new AttachmentEndpoint(httpClient);
        Users = new UserEndpoint(httpClient);
    }
}