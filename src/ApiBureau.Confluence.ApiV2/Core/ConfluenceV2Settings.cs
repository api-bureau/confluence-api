using System.ComponentModel.DataAnnotations;

namespace ApiBureau.Confluence.ApiV2.Core;

public sealed class ConfluenceV2Settings
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