using System.Text.Json.Serialization;

namespace Tourism.Dotnet.Parser.DAL.Entities;

public class WorkingHours
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("from")]
    public string From { get; set; }

    [JsonPropertyName("to")]
    public string To { get; set; }
}