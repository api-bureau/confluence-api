using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Dtos;

public sealed class BodyDto
{
    public BodyRepresentationDto? Storage { get; set; }
    public BodyRepresentationDto? View { get; set; }

    [JsonPropertyName("atlas_doc_format")]
    public BodyRepresentationDto? AtlasDocFormat { get; set; }
}
