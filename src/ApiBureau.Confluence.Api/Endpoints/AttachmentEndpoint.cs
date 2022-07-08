namespace ApiBureau.Confluence.Api.Endpoints;

public class AttachmentEndpoint
{
    private readonly HttpHelper _helper;

    public AttachmentEndpoint(HttpHelper helper) => _helper = helper;

    public Task<Stream> GetAsync(int contentId, string attachmentId)
    {
        if (string.IsNullOrWhiteSpace(attachmentId))
            throw new ArgumentNullException(nameof(attachmentId));

        return _helper.GetStreamAsync($"{Constants.ContentUrl}/{contentId}/child/attachment/{attachmentId}/download");

        //return await _helper.GetStreamAsync($"{ApiUrlPrefix}/{Constants.ContentUrl}/{contentId}/child/attachment/{attachmentId}/download");
    }
}