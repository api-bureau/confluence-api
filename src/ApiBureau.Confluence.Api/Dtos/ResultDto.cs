namespace ApiBureau.Confluence.Api.Dtos;

public class ResultDto<T>
{
    public List<T> Results { get; set; } = new();
    public int Start { get; set; }
    public int Limit { get; set; }
    public int Size { get; set; }
}

