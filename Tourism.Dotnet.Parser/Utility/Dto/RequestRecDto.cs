namespace Tourism.Dotnet.Parser.Utility.Dto;

public class RequestRecDto
{
    public string? Query { get; set; } = string.Empty;
    public RecommendationDto[]? Recommendations { get; set; }
    public string Language { get; set; } = "english";
}