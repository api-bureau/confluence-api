using System.Net;

namespace ApiBureau.Confluence.ApiV2.Http;

public sealed class ConfluenceV2ApiException : HttpRequestException
{
    public string? ResponseBody { get; }

    public ConfluenceV2ApiException(HttpStatusCode statusCode, string? reasonPhrase, string? responseBody)
        : base($"Confluence API returned {(int)statusCode} {reasonPhrase}.", null, statusCode)
    {
        ResponseBody = responseBody;
    }
}
