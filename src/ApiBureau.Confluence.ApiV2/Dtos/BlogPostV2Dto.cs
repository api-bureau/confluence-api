using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.ApiV2.Dtos;

public sealed class BlogPostV2Dto
{
    public string Id { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? SpaceId { get; set; }
    public string? AuthorId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public VersionV2Dto? Version { get; set; }
    public BodyV2Dto? Body { get; set; }

    [JsonPropertyName("_links")]
    public ConfluenceV2Links? Links { get; set; }
}
