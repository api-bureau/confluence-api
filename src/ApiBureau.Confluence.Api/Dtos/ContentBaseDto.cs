namespace ApiBureau.Confluence.Api.Dtos;

public class ContentBaseDto<T>
{
    public T Id { get; set; }
    public string Type { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Title { get; set; } = null!;
}