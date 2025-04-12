using System.Text.Json.Serialization;

namespace Tourism.Dotnet.Parser.DAL.Entities;

public class DaySchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [JsonPropertyName("working_hours")]
    public List<WorkingHours> WorkingHours { get; set; } = new List<WorkingHours>();
}