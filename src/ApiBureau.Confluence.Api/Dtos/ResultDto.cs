using System.Text.Json.Serialization;

namespace ApiBureau.Confluence.Api.Dtos;

public class ResultDto<T>
{
    public List<T> Results { get; set; } = new();
    public int Start { get; set; }
    public int Limit { get; set; }
    public int Size { get; set; }

    [JsonPropertyName("_links")]
    public LinksDto? Links { get; set; }
}

public class LinksDto {
    public string? Next { get; set; }
    public string? Prev { get; set; }
}

public class PageResultDto<T>
{
    public ResultDto<T>? Page { get; set; }
}