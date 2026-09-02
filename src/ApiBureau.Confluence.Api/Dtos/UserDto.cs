namespace ApiBureau.Confluence.Api.Dtos;

public sealed class UserDto
{
    public string AccountId { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? PublicName { get; set; }
    public string? Email { get; set; }
    public string? TimeZone { get; set; }
    public string? AccountStatus { get; set; }
    public string? AccountType { get; set; }
    public bool IsExternalCollaborator { get; set; }
}