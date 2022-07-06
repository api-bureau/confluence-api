namespace ApiBureau.Confluence.Api.Dtos;

public class ContentDto : ContentBaseDto<int>
{
    public int SpaceId { get; set; }
    public BodyDto Body { get; set; } = null!;
    public VersionDto? Version { get; set; }
    public SpaceDto? Space { get; set; }
    public List<ContentBaseDto<int>> Ancestors { get; set; } = new();
    public ChildrenDto? Children { get; set; }
    public ExtensionsDto Extensions { get; set; } = null!;
}

public class ChildrenDto
{
    public ResultDto<AttachmentDto>? Attachment { get; set; }
}

public class AttachmentDto : ContentBaseDto<string>
{
    public AttachmentExtensionDto? Extensions { get; set; }
}

public class AttachmentExtensionDto
{
    public string MediaType { get; set; } = null!;
    public int FileSize { get; set; }
}

public class ExtensionsDto
{
    public int Position { get; set; }
}