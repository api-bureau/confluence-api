namespace ApiBureau.Confluence.Api.Responses;

public class ErrorResponse
{
    public int Status { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public bool Success => Code is null || Status == 200;
}