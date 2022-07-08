namespace ApiBureau.Confluence.Api.Endpoints;

public class ContentEndpoint
{
    private readonly HttpHelper _helper;

    public ContentEndpoint(HttpHelper helper) => _helper = helper;

    /// <summary>
    /// Returns a content entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expand">Use body.view in view format or body.storage in storage format</param>
    /// <returns></returns>
    public Task<ContentDto?> GetAsync(int id, string expand = "body.view")
        => _helper.GetAsync<ContentDto>($"{Constants.ContentUrl}/{id}?expand={expand}");

    /// <summary>
    /// Returns content properties
    /// </summary>
    /// <returns></returns>
    public async Task<ResultDto<PropertyDto>> GetPropertiesAsync(int contentId)
    {
        return await _helper.GetResultAsync<PropertyDto>($"{Constants.ContentUrl}/{contentId}/property") ?? new();
        //return await response.Content.ReadFromJsonAsync<ResultDto<PropertyDto>>() ?? new();
    }
}
