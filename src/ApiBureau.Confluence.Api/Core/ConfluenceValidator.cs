using Microsoft.Extensions.Logging;
using System.Text;

namespace ApiBureau.Confluence.Api.Core;

/// <summary>
/// Provides validation methods for Confluence settings.
/// This static class contains methods to ensure that necessary configuration settings
/// are present and valid for Confluence operations.
/// </summary>
public static class ConfluenceValidator
{
    private const string MissingSettings = "Settings are missing in the appsettings.json or secret.json";
    private const string MissingBaseUrl = "Base Url in settings is missing or empty";
    private const string MissingEmail = "Email in settings is missing or empty";
    private const string MissingUserApiToken = "User API token in settings is missing or empty";

    /// <summary>
    /// Validates the provided Confluence settings.
    /// </summary>
    /// <param name="settings">The ConfluenceSettings object to validate.</param>
    /// <param name="logger">The logger to log any validation errors.</param>
    /// <exception cref="ArgumentNullException">Thrown if settings are null.</exception>
    /// <exception cref="ArgumentException">Thrown if any of the settings properties are invalid.</exception>
    public static void ValidateSettings(ConfluenceSettings settings, ILogger logger)
    {
        if (settings is null)
        {
            logger.LogError(MissingSettings);
            throw new ArgumentNullException(nameof(settings), MissingSettings);
        }

        var errors = new StringBuilder();

        if (string.IsNullOrWhiteSpace(settings.BaseUrl))
        {
            errors.AppendLine(MissingBaseUrl);
        }

        if (string.IsNullOrWhiteSpace(settings.Email))
        {
            errors.AppendLine(MissingEmail);
        }

        if (string.IsNullOrWhiteSpace(settings.UserApiToken))
        {
            errors.AppendLine(MissingUserApiToken);
        }

        if (errors.Length > 0)
        {
            var errorMessage = errors.ToString().TrimEnd();

            logger.LogError("Settings validation errors: {errors}", errorMessage);
            throw new ArgumentException(errorMessage, nameof(settings));
        }
    }
}