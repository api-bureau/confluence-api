using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.ApiV2.Responses;

public sealed class PagedV2Response<T>
{
    public List<T> Results { get; set; } = [];

    [JsonPropertyName("_links")]
    public ConfluenceV2Links? Links { get; set; }
}

public sealed class ConfluenceV2Links
{
    public string? Next { get; set; }
    public string? Base { get; set; }
    public string? Self { get; set; }
    public string? Webui { get; set; }
    public string? Editui { get; set; }
    public string? Tinyui { get; set; }
    public string? Download { get; set; }
}
