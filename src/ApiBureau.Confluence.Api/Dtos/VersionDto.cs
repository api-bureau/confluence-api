namespace ApiBureau.Confluence.Api.Dtos;

public sealed class VersionDto
{
    public DateTimeOffset? CreatedAt { get; set; }
    public string? Message { get; set; }
    public int Number { get; set; }
    public bool MinorEdit { get; set; }
    public string? AuthorId { get; set; }
}