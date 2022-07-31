namespace ApiBureau.Confluence.Api.Endpoints;

public class AttachmentEndpoint : BaseEndpoint
{
    public AttachmentEndpoint(ApiConnection apiConnection) : base(apiConnection) { }

    public Task<Stream> GetAsync(int contentId, string attachmentId)
    {
        if (string.IsNullOrWhiteSpace(attachmentId))
            throw new ArgumentNullException(nameof(attachmentId));

        return ApiConnection.GetStreamAsync($"{Constants.ContentUrl}/{contentId}/child/attachment/{attachmentId}/download");

        //return await _helper.GetStreamAsync($"{ApiUrlPrefix}/{Constants.ContentUrl}/{contentId}/child/attachment/{attachmentId}/download");
    }
}