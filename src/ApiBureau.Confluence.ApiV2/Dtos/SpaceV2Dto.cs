using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.ApiV2.Dtos;

public sealed class SpaceV2Dto
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
    public SpaceDescriptionV2Dto? Description { get; set; }

    [JsonPropertyName("_links")]
    public ConfluenceV2Links? Links { get; set; }
}

public sealed class SpaceDescriptionV2Dto
{
    public BodyRepresentationV2Dto? Plain { get; set; }
    public BodyRepresentationV2Dto? View { get; set; }
}
