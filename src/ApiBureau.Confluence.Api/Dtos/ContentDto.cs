namespace ApiBureau.Confluence.Api.Dtos
{
    public class ContentDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public BodyDto Body { get; set; } = default!;
    }
}
