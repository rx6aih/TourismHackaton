using System.Text.Json.Serialization;

namespace Tourism.Dotnet.Parser.DAL.Entities;

public class Schedule
{
    public int Id { get; set; }
    public Guid? MondayId { get; set; }
    public Guid? TuesdayId { get; set; }
    public Guid? WednesdayId { get; set; }
    public Guid? ThursdayId { get; set; }
    public Guid? FridayId { get; set; }
    public Guid? SaturdayId { get; set; }
    public Guid? SundayId { get; set; }

    [JsonPropertyName("Fri")]
    public DaySchedule Friday { get; set; }

    [JsonPropertyName("Mon")]
    public DaySchedule Monday { get; set; }

    [JsonPropertyName("Sat")]
    public DaySchedule Saturday { get; set; }

    [JsonPropertyName("Sun")]
    public DaySchedule Sunday { get; set; }

    [JsonPropertyName("Thu")]
    public DaySchedule Thursday { get; set; }

    [JsonPropertyName("Tue")]
    public DaySchedule Tuesday { get; set; }

    [JsonPropertyName("Wed")]
    public DaySchedule Wednesday { get; set; }
}