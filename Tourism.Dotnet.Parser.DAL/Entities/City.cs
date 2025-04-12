namespace Tourism.Dotnet.Parser.DAL.Entities;

public class City
{
    public int Id { get; set; }
    public string Image { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public List<string>? Options { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Price { get; set; } = string.Empty;
    public List<Place>? Places { get; set; }
}