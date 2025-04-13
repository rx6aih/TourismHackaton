using System.Text;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.Utility;
using Tourism.Dotnet.Parser.Utility.Dto;
using DaySchedule = Tourism.Dotnet.Parser.DAL.Entities.DaySchedule;
using Schedule = Tourism.Dotnet.Parser.DAL.Entities.Schedule;

namespace Tourism.Dotnet.Parser.Services;

public class GisParser(IHttpClientFactory factory)
{
    public async Task<GisDto> FetchPlaces(string city, int page, CancellationToken cancellationToken = default)
    {
        var client = factory.CreateClient("httpClient");
        return await client.GetFromJsonAsync<GisDto>($"https://catalog.api.2gis.com/3.0/items?q={city}&key={ApiKeys.Gis}&fields=items.point,items.reviews,items.schedule,items.address,items.rubrics&$page={page}", cancellationToken);
    }

public async Task<List<Place>> ConvertToPlaces(GisDto gisDto, CancellationToken cancellationToken = default)
{
    List<Place> places = new List<Place>();
    
    foreach (var dto in gisDto.Result.Items)
    {
        if (dto == null) continue;

        var rubricsBuilder = new StringBuilder();
        if (dto.Rubrics != null)
        {
            foreach (var rubric in dto.Rubrics)
            {
                rubricsBuilder.Append(rubric.Name).Append(" ");
            }
        }

        string address = string.Empty;
        if (dto.Address?.Components != null && dto.Address.Components.Any())
        {
            var component = dto.Address.Components[0];
            address = $"{component.Street} {component.Number}".Trim();
        }

        Point point = null;
        if (dto.Point != null)
        {
            point = new Point
            {
                lon = dto.Point.lon,
                lat = dto.Point.lat
            };
        }

        Schedule schedule = null;
        if (dto.Schedule != null)
        {
            schedule = new Schedule
            {
                MondayId = dto.Schedule.MondayId,
                TuesdayId = dto.Schedule.TuesdayId,
                WednesdayId = dto.Schedule.WednesdayId,
                ThursdayId = dto.Schedule.ThursdayId,
                FridayId = dto.Schedule.FridayId,
                SaturdayId = dto.Schedule.SaturdayId,
                SundayId = dto.Schedule.SundayId,
                Friday = MapDaySchedule(dto.Schedule.Friday),
                Monday = MapDaySchedule(dto.Schedule.Monday),
                Saturday = MapDaySchedule(dto.Schedule.Saturday),
                Sunday = MapDaySchedule(dto.Schedule.Sunday),
                Thursday = MapDaySchedule(dto.Schedule.Thursday),
                Tuesday = MapDaySchedule(dto.Schedule.Tuesday),
                Wednesday = MapDaySchedule(dto.Schedule.Wednesday)
            };
        }

        Place place = new Place()
        {
            FullName = dto.FullName,
            ImageUrl = string.Empty,
            Name = dto.Name,
            Address = address,
            Rating = dto.Reviews?.GeneralRating.ToString(),
            Rubrics = rubricsBuilder.ToString().Trim(),
            Point = point,
            Schedule = schedule
        };
        
        places.Add(place);
    }
    
    return places;
}

private DaySchedule MapDaySchedule(Tourism.Dotnet.Parser.DAL.Entities.DaySchedule source)
{
    if (source == null) return null;
    
    return new DaySchedule
    {
        Id = source.Id,
        WorkingHours = source.WorkingHours?.Select(wh => new WorkingHours
        {
            Id = wh.Id,
            From = wh.From,
            To = wh.To
        }).ToList() ?? new List<WorkingHours>()
    };
}
}