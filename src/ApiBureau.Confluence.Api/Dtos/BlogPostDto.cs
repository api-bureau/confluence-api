using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Dtos;

public sealed class BlogPostDto
{
    public string Id { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? SpaceId { get; set; }
    public string? AuthorId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public VersionDto? Version { get; set; }
    public BodyDto? Body { get; set; }

    [JsonPropertyName("_links")]
    public ConfluenceLinks? Links { get; set; }
}