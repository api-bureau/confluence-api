using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Responses;

public sealed class PagedResponse<T>
{
    public List<T> Results { get; set; } = [];

    [JsonPropertyName("_links")]
    public ConfluenceLinks? Links { get; set; }
}

public sealed class ConfluenceLinks
{
    public string? Next { get; set; }
    public string? Base { get; set; }
    public string? Self { get; set; }
    public string? Webui { get; set; }
    public string? Editui { get; set; }
    public string? Tinyui { get; set; }
    public string? Download { get; set; }
}