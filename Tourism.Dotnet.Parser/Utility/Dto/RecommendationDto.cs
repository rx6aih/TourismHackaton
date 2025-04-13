using System.Text.Json.Serialization;

namespace Tourism.Dotnet.Parser.Utility.Dto;

public class RecommendationDto
{
    public string Id { get; set; }
    [JsonPropertyName("category")]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}