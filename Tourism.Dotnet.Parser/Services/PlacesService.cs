using Microsoft.EntityFrameworkCore;
using Tourism.Dotnet.Parser.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.DAL.Implementations;

namespace Tourism.Dotnet.Parser.Services;

public class PlacesService(IHttpClientFactory factory, Repository<Place> repository,Repository<City> cityRepository, ParserDbContext context)
{
    CityService _cityService = new (cityRepository,repository,context);
    public async Task<List<Place>> ParseAndAddPlaces(string city, int page, CancellationToken cancellationToken = default)
    {
        City? currentCity = await context.Cities.Where(x => x.Title == city).FirstOrDefaultAsync();
        if (currentCity == null)
            return new List<Place>();
        GisParser parser = new GisParser(factory);
        List<Place> places = await parser.ConvertToPlaces(await parser.FetchPlaces(city, page, cancellationToken), cancellationToken);
        foreach (var place in places)
        {
            if (place.Point != null)
            {
                await context.Points.AddAsync(place.Point);
                await context.SaveChangesAsync();
                place.PointId = place.Point.Id;
            }

            if (place.Schedule != null)
            {
                await SaveScheduleWithDays(place.Schedule);
                await context.SaveChangesAsync();
                place.ScheduleId = place.Schedule.Id;
            }
            
            await context.Places.AddAsync(place);
            await context.SaveChangesAsync();

        }
        List<int> placesIds = places.Select(x => x.Id).ToList();
        await _cityService.UpdateAsync(currentCity.Id,city, placesIds);
        return places;
    }

    public async Task<Place?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Places.Where(x=>x.Name==name).FirstOrDefaultAsync();
    }
    public async Task AddPlaces(Place place, CancellationToken cancellationToken = default)
    {
        await repository.CreateAsync(place, cancellationToken);
    }
    public async Task AddPlaces(List<Place> places, CancellationToken cancellationToken = default)
    {
        foreach (var place in places)
        {
            await repository.CreateAsync(place, cancellationToken);
        }
    }

    public async Task<List<Place>> GetAllPlaces(CancellationToken cancellationToken = default)
    {
        return await context.Places
            .Include(p => p.Point)
            .Include(p => p.Schedule)
            .ThenInclude(s=>s.Monday).ThenInclude(w=>w.WorkingHours)
            .Include(p => p.Schedule)
            .ThenInclude(s=>s.Tuesday).ThenInclude(w=>w.WorkingHours)
            .Include(p => p.Schedule)
            .ThenInclude(s=>s.Thursday).ThenInclude(w=>w.WorkingHours)
            .Include(p => p.Schedule)
            .ThenInclude(s=>s.Sunday).ThenInclude(w=>w.WorkingHours)
            .Include(p => p.Schedule)
            .ThenInclude(s=>s.Saturday).ThenInclude(w=>w.WorkingHours)
            .Include(p => p.Schedule)
            .ThenInclude(s=>s.Friday).ThenInclude(w=>w.WorkingHours)
            .Include(p => p.Schedule)
            .ThenInclude(s=>s.Wednesday).ThenInclude(w=>w.WorkingHours)
            .ToListAsync(cancellationToken);
    }

    private async Task SaveScheduleWithDays(Schedule schedule)
    {
        await context.Schedules.AddAsync(schedule);
    
        // Сохраняем дни недели
        var days = new List<DaySchedule>
        {
            schedule.Monday,
            schedule.Tuesday,
            schedule.Wednesday,
            schedule.Thursday,
            schedule.Friday,
            schedule.Saturday,
            schedule.Sunday,
        };

        foreach (var day in days.Where(d => d != null))
        {
            await context.DaySchedules.AddAsync(day);
        }
    }
    
    public async Task DeletePlace(Guid id)
    {
        await repository.DeleteAsync(await repository.GetItemByIdAsync(id));
    }
}