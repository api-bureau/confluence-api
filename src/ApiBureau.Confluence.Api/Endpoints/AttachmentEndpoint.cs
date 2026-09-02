using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class AttachmentEndpoint : BaseEndpoint
{
    private const string ResourcePath = "attachments";

    public AttachmentEndpoint(ConfluenceHttpClient httpClient) : base(httpClient) { }

    public Task<AttachmentDto?> GetByIdAsync(string attachmentId, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(attachmentId);
        return HttpClient.GetAsync<AttachmentDto>($"{ResourcePath}/{Uri.EscapeDataString(attachmentId)}", token);
    }

    public Task<List<AttachmentDto>> GetAllForPageAsync(string pageId, int limit = 250, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);
        var path = QueryHelpers.AddQueryString($"pages/{Uri.EscapeDataString(pageId)}/attachments", "limit", limit.ToString());
        return HttpClient.GetAllAsync<AttachmentDto>(path, token);
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