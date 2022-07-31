namespace ApiBureau.Confluence.Api.Endpoints;

public class SpaceEndpoint : BaseEndpoint
{
    public SpaceEndpoint(ApiConnection apiConnection) : base(apiConnection) { }

    /// <summary>
    /// Returns all spaces
    /// </summary>
    /// <returns></returns>
    public async Task<ResultDto<SpaceDto>> GetAsync()
    {
        var result = await ApiConnection.GetResultAsync<SpaceDto>(Constants.SpaceUrl);
        //var result = await _helper.GetFromJsonAsync<ResultDto<SpaceDto>>($"{ApiUrlPrefix}/{Constants.SpaceUrl}");

        return result ?? new();
    }

    /// <summary>
    /// Returns space content entities
    /// </summary>
    /// <param name="key"></param>
    /// <param name="expand"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<List<ContentDto>> GetContentAsync(string key, SpaceExpand? expand = null, int limit = 100)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        expand ??= new SpaceExpand();

        var result = await ApiConnection.GetSpaceContentAsync<ContentDto>($"{Constants.SpaceUrl}/{key}/{Constants.ContentUrl}", expand.Get(), limit);

        return result ?? new List<ContentDto>();
    }
}