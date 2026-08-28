using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.ApiV2.Endpoints;

public sealed class AttachmentV2Endpoint : BaseV2Endpoint
{
    private const string ResourcePath = "attachments";

    public AttachmentV2Endpoint(ConfluenceV2HttpClient httpClient) : base(httpClient) { }

    public Task<AttachmentV2Dto?> GetByIdAsync(string attachmentId, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(attachmentId);
        return HttpClient.GetAsync<AttachmentV2Dto>($"{ResourcePath}/{Uri.EscapeDataString(attachmentId)}", token);
    }

    public Task<List<AttachmentV2Dto>> GetAllForPageAsync(string pageId, int limit = 250, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);
        var path = QueryHelpers.AddQueryString($"pages/{Uri.EscapeDataString(pageId)}/attachments", "limit", limit.ToString());
        return HttpClient.GetAllAsync<AttachmentV2Dto>(path, token);
    }

    public async Task<Stream> DownloadAsync(string attachmentId, CancellationToken token = default)
    {
        var attachment = await GetByIdAsync(attachmentId, token).ConfigureAwait(false)
            ?? throw new InvalidOperationException($"Attachment '{attachmentId}' was not returned by Confluence.");

        var downloadLink = attachment.DownloadLink ?? attachment.Links?.Download;
        if (string.IsNullOrWhiteSpace(downloadLink))
            throw new InvalidOperationException($"Attachment '{attachmentId}' does not contain a download link.");

        return await HttpClient.GetStreamAsync(downloadLink, token).ConfigureAwait(false);
    }
}
