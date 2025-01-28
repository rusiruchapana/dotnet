namespace dotnet_learn.Models.DTO.Request;

public class AddRegionRequestDTO
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}