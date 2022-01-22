namespace ApiBureau.Confluence.Api.Dtos
{
    public class ContentDto : ContentBaseDto
    {
        public int SpaceId { get; set; }
        public BodyDto Body { get; set; } = default!;
        public VersionDto? Version { get; set; }
        public SpaceDto? Space { get; set; }

        public List<ContentBaseDto> Ancestors { get; set; } = new();
    }
}
