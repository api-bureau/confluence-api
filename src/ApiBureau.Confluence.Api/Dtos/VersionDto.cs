namespace ApiBureau.Confluence.Api.Dtos;

public class VersionDto
{
    public DateTime When { get; set; }

    public ByDto By { get; set; } = null!;

    public class ByDto
    {
        public string PublicName { get; set; } = null!;
    }
}
