using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class BlogPostEndpoint : BaseEndpoint
{
    private const string ResourcePath = "blogposts";

    public BlogPostEndpoint(ConfluenceHttpClient httpClient) : base(httpClient) { }

    public Task<List<BlogPostDto>> GetAllAsync(string bodyFormat = "view", int limit = 250, CancellationToken token = default)
        => HttpClient.GetAllAsync<BlogPostDto>(BuildListPath(ResourcePath, bodyFormat, limit), token);

    public Task<List<BlogPostDto>> GetAllForSpaceAsync(string spaceId, string bodyFormat = "view", int limit = 250, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(spaceId);
        return HttpClient.GetAllAsync<BlogPostDto>(BuildListPath($"spaces/{Uri.EscapeDataString(spaceId)}/blogposts", bodyFormat, limit), token);
    }

    public Task<BlogPostDto?> GetByIdAsync(string blogPostId, string bodyFormat = "view", CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(blogPostId);
        return HttpClient.GetAsync<BlogPostDto>(QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(blogPostId)}", "body-format", bodyFormat), token);
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