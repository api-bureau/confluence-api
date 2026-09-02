using System.Text.Json;

namespace ApiBureau.Confluence.Api.Dtos;

public sealed class ContentPropertyDto
{
    public string Id { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public JsonElement Value { get; set; }
    public VersionDto? Version { get; set; }

    public string? ValueAsString => Value.ValueKind == JsonValueKind.String ? Value.GetString() : null;
}