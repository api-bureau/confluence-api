using System.Net;

namespace ApiBureau.Confluence.Api.Http;

public sealed class ConfluenceApiException : HttpRequestException
{
    public string? ResponseBody { get; }

    public ConfluenceApiException(HttpStatusCode statusCode, string? reasonPhrase, string? responseBody)
        : base($"Confluence API returned {(int)statusCode} {reasonPhrase}.", null, statusCode)
    {
        ResponseBody = responseBody;
    }
}