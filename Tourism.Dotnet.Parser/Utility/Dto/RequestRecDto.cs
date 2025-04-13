using System.Text.Json.Serialization;

namespace Tourism.Dotnet.Parser.Utility.Dto;

public class RequestRecDto
{
    public string? Query { get; set; } = string.Empty;
    [JsonPropertyName("places")]
    public RecommendationDto[]? Recommendations { get; set; }
    public string Language { get; set; } = "english";
}