using Microsoft.EntityFrameworkCore;
using Tourism.Dotnet.Parser.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.DAL.Implementations;
using Tourism.Dotnet.Parser.Utility.Dto;

namespace Tourism.Dotnet.Parser.Services;

public class CityService(Repository<City> repository, Repository<Place> placesRepository, ParserDbContext parserDbContext)
{
    public async Task<List<City>> GetAllAsync()
    {
        return await parserDbContext.Cities
            .Include(p=>p.Places)
            .ToListAsync();
    }

    public async Task<City> GetByIdAsync(int id)
    {
        return await repository.GetItemByIntegerIdAsync(id);
    }

    public async Task AddAsync(CityDto city, CancellationToken cancellationToken = default)
    {
        // Создаем новый город без указания Id
        var cityEntity = new City()
        {
            Title = city.Title,
            Options = city.Options,
            Price = city.Price,
            Description = city.Description,
            Category = city.Category,
            Places = new List<Place>()
        };

        await repository.CreateAsync(cityEntity, cancellationToken);
    
        foreach (int placeId in city.Places)
        {
            var place = await placesRepository.GetItemByIntegerIdAsync(placeId);
            place.CityId = cityEntity.Id;
            await placesRepository.UpdateAsync(place, place.Id, cancellationToken);
        }
    
        await repository.UpdateAsync(cityEntity, cityEntity.Id, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {    
        var city = await parserDbContext.Cities
            .Include(c => c.Places)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (city != null)
        {
            foreach (var place in city.Places)
            {
                place.CityId = null;
                place.City = null;
            }

            parserDbContext.Cities.Remove(city);
            await parserDbContext.SaveChangesAsync();
        }   
    }

    public async Task UpdateAsync(int id ,string? title, List<int>? places, CancellationToken cancellationToken = default)
    {
        var city = await repository.GetItemByIntegerIdAsync(id);
        city.Title = title;
        await repository.CreateAsync(city, cancellationToken);

        foreach (var placeId in places)
        {
            var place = await placesRepository.GetItemByIntegerIdAsync(placeId);
            place.CityId = city.Id;
            await placesRepository.UpdateAsync(place, place.Id, cancellationToken);
        }
        await repository.UpdateAsync(city, city.Id, cancellationToken);
    }
}