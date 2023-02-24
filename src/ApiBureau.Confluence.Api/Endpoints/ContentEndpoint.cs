namespace ApiBureau.Confluence.Api.Endpoints;

public class ContentEndpoint : BaseEndpoint
{
    public ContentEndpoint(ApiConnection apiConnection) : base(apiConnection) { }

    /// <summary>
    /// Returns a content entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expand">Use body.view in view format or body.storage in storage format</param>
    /// <returns></returns>
    public Task<ContentDto?> GetAsync(int id, string expand = "body.view")
        => ApiConnection.GetAsync<ContentDto>($"{Constants.ContentUrl}/{id}?expand={expand}");

    /// <summary>
    /// Returns content properties
    /// </summary>
    /// <returns></returns>
    public async Task<ResultDto<PropertyDto>> GetPropertiesAsync(int contentId)
    {
        return await ApiConnection.GetResultAsync<PropertyDto>($"{Constants.ContentUrl}/{contentId}/property") ?? new();
        //return await response.Content.ReadFromJsonAsync<ResultDto<PropertyDto>>() ?? new();
    }
}