using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Responses;

public class PagedResponse<T> : ErrorsResponse
{
    public List<T> Results { get; set; } = [];

    [JsonPropertyName("_links")]
    public LinksDto? Links { get; set; }
}