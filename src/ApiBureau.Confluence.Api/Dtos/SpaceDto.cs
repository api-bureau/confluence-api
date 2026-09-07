using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Dtos;

public sealed class SpaceDto
{
    public string Id { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? AuthorId { get; set; }
    public string? SpaceOwnerId { get; set; }
    public string? HomepageId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public SpaceDescriptionDto? Description { get; set; }

    [JsonPropertyName("_links")]
    public ConfluenceLinks? Links { get; set; }
}
