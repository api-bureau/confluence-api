using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.ApiV2.Dtos;

public sealed class BodyV2Dto
{
    public BodyRepresentationV2Dto? Storage { get; set; }
    public BodyRepresentationV2Dto? View { get; set; }

    [JsonPropertyName("atlas_doc_format")]
    public BodyRepresentationV2Dto? AtlasDocFormat { get; set; }
}

public sealed class BodyRepresentationV2Dto
{
    public string? Representation { get; set; }
    public string? Value { get; set; }
}