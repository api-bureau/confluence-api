using System.ComponentModel.DataAnnotations;

namespace ApiBureau.Confluence.Api.Core;

public sealed class ConfluenceSettings
{
    [Required]
    [Url]
    public required string BaseUrl { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string UserApiToken { get; set; }
}