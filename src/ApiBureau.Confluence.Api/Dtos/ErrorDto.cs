namespace ApiBureau.Confluence.Api.Dtos;

public class ErrorDto
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public bool Success => Message is null || StatusCode > 0;
}