using System.Text.Json;

namespace ApiBureau.Confluence.ApiV2.Dtos;

public sealed class ContentPropertyV2Dto
{
    public string Id { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public JsonElement Value { get; set; }
    public VersionV2Dto? Version { get; set; }

    public string? ValueAsString => Value.ValueKind == JsonValueKind.String ? Value.GetString() : null;
}
