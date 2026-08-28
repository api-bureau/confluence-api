using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.ApiV2.Dtos;

public sealed class AttachmentV2Dto
{
    public string Id { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
    public string? PageId { get; set; }
    public string? BlogPostId { get; set; }
    public string? CustomContentId { get; set; }
    public string? MediaType { get; set; }
    public string? MediaTypeDescription { get; set; }
    public string? Comment { get; set; }
    public string? FileId { get; set; }
    public long? FileSize { get; set; }
    public string? WebuiLink { get; set; }
    public string? DownloadLink { get; set; }
    public VersionV2Dto? Version { get; set; }

    [JsonPropertyName("_links")]
    public ConfluenceV2Links? Links { get; set; }
}
