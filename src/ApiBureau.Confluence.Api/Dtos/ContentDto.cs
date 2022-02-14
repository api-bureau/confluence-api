namespace ApiBureau.Confluence.Api.Dtos
{
    public class ContentDto : ContentBaseDto<int>
    {
        public int SpaceId { get; set; }
        public BodyDto Body { get; set; } = default!;
        public VersionDto? Version { get; set; }
        public SpaceDto? Space { get; set; }
        public List<ContentBaseDto<int>> Ancestors { get; set; } = new();
        public ChildrenDto? Children { get; set; }
    }

    public class ChildrenDto
    {
        public ResultDto<AttachmentDto>? Attachment { get; set; }
    }

    public class AttachmentDto : ContentBaseDto<string>
    {
        public ExtensionDto? Extensions { get; set; }
    }

    public class ExtensionDto
    {
        public string MediaType { get; set; } = null!;
    }
}
