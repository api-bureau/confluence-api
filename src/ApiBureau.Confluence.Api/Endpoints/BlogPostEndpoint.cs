using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class BlogPostEndpoint
{
    private const string ResourcePath = "blogposts";
    private readonly ConfluenceHttpClient _httpClient;

    internal BlogPostEndpoint(ConfluenceHttpClient httpClient)
        => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public Task<IReadOnlyList<BlogPostDto>> GetAllAsync(
        string bodyFormat = "view",
        int limit = 250,
        CancellationToken cancellationToken = default)
        => _httpClient.GetAllAsync<BlogPostDto>(
            BuildListPath(ResourcePath, bodyFormat, limit),
            cancellationToken);

    public Task<IReadOnlyList<BlogPostDto>> GetAllForSpaceAsync(
        string spaceId,
        string bodyFormat = "view",
        int limit = 250,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(spaceId);
        return _httpClient.GetAllAsync<BlogPostDto>(
            BuildListPath($"spaces/{Uri.EscapeDataString(spaceId)}/blogposts", bodyFormat, limit),
            cancellationToken);
    }

    public Task<BlogPostDto?> GetByIdAsync(
        string blogPostId,
        string bodyFormat = "view",
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(blogPostId);
        ArgumentException.ThrowIfNullOrWhiteSpace(bodyFormat);
        return _httpClient.GetAsync<BlogPostDto>(
            QueryHelpers.AddQueryString($"{ResourcePath}/{Uri.EscapeDataString(blogPostId)}", "body-format", bodyFormat),
            cancellationToken);
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