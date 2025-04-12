using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tourism.Dotnet.Parser.DAL.Entities;

public class Place
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? Rating { get; set; } = string.Empty;
    public string? Rubrics { get; set; }
    
    public int? CityId { get; set; }
    public int? PointId { get; set; }
    public int? ScheduleId { get; set; }
    [JsonIgnore]
    public City? City { get; set; }
    public Point? Point { get; set; }
    public Schedule? Schedule { get; set; }
}