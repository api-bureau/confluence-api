namespace ApiBureau.Confluence.Api.Dtos;

public class BlogPostDto : ContentBaseDto<string>
{
    public string? SpaceId { get; set; }
    public string? AuthordId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public BodyDtoV2 Body { get; set; } = null!;
}

public class BodyDtoV2
{
    public StorageDto? Storage { get; set; }
}

public class StorageDto
{
    public string? Representation { get; set; }
    public string? Value { get; set; }
}