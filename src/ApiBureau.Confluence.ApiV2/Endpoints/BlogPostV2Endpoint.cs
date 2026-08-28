using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.ApiV2.Endpoints;

public sealed class BlogPostV2Endpoint : BaseV2Endpoint
{
    private const string ResourcePath = "blogposts";

    public BlogPostV2Endpoint(ConfluenceV2HttpClient httpClient) : base(httpClient) { }

    public Task<List<BlogPostV2Dto>> GetAllAsync(string bodyFormat = "view", int limit = 250, CancellationToken token = default)
        => HttpClient.GetAllAsync<BlogPostV2Dto>(BuildListPath(ResourcePath, bodyFormat, limit), token);

    public Task<List<BlogPostV2Dto>> GetAllForSpaceAsync(string spaceId, string bodyFormat = "view", int limit = 250, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(spaceId);
        return HttpClient.GetAllAsync<BlogPostV2Dto>(BuildListPath($"spaces/{Uri.EscapeDataString(spaceId)}/blogposts", bodyFormat, limit), token);
    }

    public Task<BlogPostV2Dto?> GetByIdAsync(string blogPostId, string bodyFormat = "view", CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(blogPostId);
        return HttpClient.GetAsync<BlogPostV2Dto>(QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(blogPostId)}", "body-format", bodyFormat), token);
    }

    private static string BuildListPath(string path, string bodyFormat, int limit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(bodyFormat);
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);
        return QueryHelpers.AddQueryString(path, new Dictionary<string, string?>
        {
            ["body-format"] = bodyFormat,
            ["limit"] = limit.ToString()
        });
    }
}