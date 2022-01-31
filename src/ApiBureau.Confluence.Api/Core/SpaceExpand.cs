namespace ApiBureau.Confluence.Api.Core;

public class SpaceExpand
{
    private List<string> _items = new();

    public string Get() => _items.Count == 0 ? "" : "&expand=" + string.Join(",", _items);

    public SpaceExpand AddVersion() => AddItem("version");

    public SpaceExpand AddAncestors() => AddItem("ancestors");

    public SpaceExpand AddChildren(string type = "attachment") => AddItem($"children.{type}");

    /// <summary>
    /// Body can be view or storage
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public SpaceExpand AddBody(string type = "view") => AddItem($"body.{type}");

    public SpaceExpand AddItem(string text)
    {
        _items.Add(text);

        return this;
    }
}

