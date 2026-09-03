using Microsoft.AspNetCore.WebUtilities;

namespace ApiBureau.Confluence.Api.Endpoints;

public sealed class AttachmentEndpoint
{
    private const string ResourcePath = "attachments";
    private readonly ConfluenceHttpClient _httpClient;

    internal AttachmentEndpoint(ConfluenceHttpClient httpClient)
        => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public Task<AttachmentDto?> GetByIdAsync(string attachmentId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(attachmentId);
        return _httpClient.GetAsync<AttachmentDto>(
            $"{ResourcePath}/{Uri.EscapeDataString(attachmentId)}",
            cancellationToken);
    }

    public Task<IReadOnlyList<AttachmentDto>> GetAllForPageAsync(
        string pageId,
        int limit = 250,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageId);
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);
        var path = QueryHelpers.AddQueryString($"pages/{Uri.EscapeDataString(pageId)}/attachments", "limit", limit.ToString());
        return _httpClient.GetAllAsync<AttachmentDto>(path, cancellationToken);
    }

    public async Task<Stream> DownloadAsync(string attachmentId, CancellationToken cancellationToken = default)
    {
        var attachment = await GetByIdAsync(attachmentId, cancellationToken).ConfigureAwait(false)
            ?? throw new InvalidOperationException($"Attachment '{attachmentId}' was not returned by Confluence.");

        var downloadLink = attachment.DownloadLink ?? attachment.Links?.Download;
        if (string.IsNullOrWhiteSpace(downloadLink))
            throw new InvalidOperationException($"Attachment '{attachmentId}' does not contain a download link.");

        return await _httpClient.GetStreamAsync(downloadLink, cancellationToken).ConfigureAwait(false);
    }
}