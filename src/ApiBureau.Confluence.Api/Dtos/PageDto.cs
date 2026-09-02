using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Dtos;

public sealed class PageDto
{
    public string Id { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? SpaceId { get; set; }
    public string? ParentId { get; set; }
    public string? ParentType { get; set; }
    public int Position { get; set; }
    public string? AuthorId { get; set; }
    public string? OwnerId { get; set; }
    public string? LastOwnerId { get; set; }
    public string? Subtype { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public VersionDto? Version { get; set; }
    public BodyDto? Body { get; set; }

    [JsonPropertyName("_links")]
    public ConfluenceLinks? Links { get; set; }
}