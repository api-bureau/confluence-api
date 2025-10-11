using System.ComponentModel.DataAnnotations;

namespace ApiBureau.Confluence.Api.Core;

public class ConfluenceSettings
{
    /// <summary>
    /// The absolute base URL of the Confluence API (e.g., https://your-name.atlassian.net).
    /// </summary>
    [Required(ErrorMessage = "BaseUrl is required.")]
    [Url]
    public required string BaseUrl { get; set; }

    /// <summary>
    /// The API key used to authenticate requests to Confluence.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    public required string Email { get; set; }

    /// <summary>
    /// The API token used to authenticate requests to Confluence.
    /// </summary>
    [Required(ErrorMessage = "UserApiToken is required.")]
    public required string UserApiToken { get; set; }
}