namespace ApiBureau.Confluence.Api.Dtos;

public class SpaceDto
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Status { get; set; } = null!;
}