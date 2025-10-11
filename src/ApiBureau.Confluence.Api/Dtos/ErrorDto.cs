namespace ApiBureau.Confluence.Api.Dtos;

public class ErrorDto
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public bool Success => Message is null || StatusCode > 0;
}

public class ErrorsDtoV2
{
    public List<ErrorDtoV2> Errors { get; set; } = [];
}

public class ErrorDtoV2
{
    public int Status { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public bool Success => Code is null || Status == 200;
}