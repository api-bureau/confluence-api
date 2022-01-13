namespace ApiBureau.Confluence.Api.Core;

public class SpaceExpand
{
    private string _expandText = string.Empty;

    public string Get() => string.IsNullOrWhiteSpace(_expandText) ? "" : "?expand=" + _expandText;

    public SpaceExpand AddVersion() => AddItem("version");

    public SpaceExpand AddBody(string type = "body") => AddItem("storage." + type);

    public SpaceExpand AddItem(string text)
    {

        _expandText += text + ",";

        return this;
    }
}

