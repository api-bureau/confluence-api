using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Responses;

public sealed class PagedResponse<T>
{
    public List<T> Results { get; set; } = [];

    [JsonPropertyName("_links")]
    public ConfluenceLinks? Links { get; set; }
}
