namespace ApiBureau.Confluence.Api.Endpoints;

public class BlogPostEndpoint : BaseEndpoint
{
    public BlogPostEndpoint(ConfluenceHttpClient apiConnection) : base(apiConnection) { }

    /// <summary>
    /// Asynchronously retrieves a blog post in the specified body format, with an optional limit on the number of items
    /// returned.
    /// </summary>
    /// <param name="bodyFormat">The format in which the blog post body is returned. Common values include "storage" and "atlas_doc_format".
    /// Defaults to "storage".</param>
    /// <param name="limit">The maximum number of items to retrieve. Must be a positive integer. Defaults to 25. Maximum is 250.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ContentDto"/>
    /// representing the blog post, or <see langword="null"/> if no content is found.</returns>
    public Task<PagedResponse<BlogPostDto>?> GetAsync(string bodyFormat = "storage", int limit = 25)
    {
        return ApiConnection.GetResultV2Async<BlogPostDto>($"{Constants.BlogPostUrl}?body-format={bodyFormat}&limit={limit}");
    }
}