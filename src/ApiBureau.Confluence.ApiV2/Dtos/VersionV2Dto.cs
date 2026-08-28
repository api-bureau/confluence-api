namespace ApiBureau.Confluence.ApiV2.Dtos;

public sealed class VersionV2Dto
{
    public DateTimeOffset? CreatedAt { get; set; }
    public string? Message { get; set; }
    public int Number { get; set; }
    public bool MinorEdit { get; set; }
    public string? AuthorId { get; set; }
}
