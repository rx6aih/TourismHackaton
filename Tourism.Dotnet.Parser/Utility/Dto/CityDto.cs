namespace Tourism.Dotnet.Parser.Utility.Dto;

public class CityDto
{
    public string Image { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; } = string.Empty;
    public List<string>? Options { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Price { get; set; } = string.Empty;
    public List<int> Places { get; set; } = new List<int>();
}