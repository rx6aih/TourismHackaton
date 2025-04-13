using System.Text.Json;
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
        var some = await repository.GetItemsAsync();
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
        if (repository.GetItemsAsync().Result.Any(x => x.Title == city.Title).Equals(null))
            return;
        // Создаем новый город без указания Id
        var cityEntity = new City()
        {
            Title = city.Title,
            Options = city.Options,
            Image = city.Image,
            Price = city.Price,
            Description = city.Description,
            Category = city.Category,
            Places = new List<Place>()
        };

        await repository.CreateAsync(cityEntity, cancellationToken);
        if (city.Places.Count > 0)
        {
            foreach (int placeId in city.Places)
            {
                var place = await placesRepository.GetItemByIntegerIdAsync(placeId);
                place.CityId = cityEntity.Id;
                await placesRepository.UpdateAsync(place, place.Id, cancellationToken);
            }

            await repository.UpdateAsync(cityEntity, cityEntity.Id, cancellationToken);
        }
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

    public async Task UpdateAsync(int id, string? title, List<int>? places,
        CancellationToken cancellationToken = default)
    {
        City? city = await repository.GetItemByIntegerIdAsync(id);
        if (city == null)
            return;
    
        if (!string.IsNullOrEmpty(title))
            city.Title = title;
    
        await repository.UpdateAsync(city, city.Id, cancellationToken);

        if (places != null)
        {
            foreach (var placeId in places)
            {
                Place? place = await placesRepository.GetItemByIntegerIdAsync(placeId);
                if (place != null)
                {
                    place.CityId = city.Id;
                    await placesRepository.UpdateAsync(place, place.Id, cancellationToken);
                }
            }
            await repository.UpdateAsync(city, city.Id, cancellationToken);
        }
    }
    public async Task<List<RecommendationDto>> GetPlacesForRecommendations(string cityTitle)
    {
        City? city = repository.GetItemsAsync().Result.FirstOrDefault(c => c.Title == cityTitle);
        if(city == null)
            return new List<RecommendationDto>();
        
        List<Place> places = placesRepository.GetItemsAsync().Result.Where(p => p.CityId == city.Id).ToList();
        List<RecommendationDto> recommendations = new List<RecommendationDto>();
        foreach (var place in places)
        {
            recommendations.Add(new RecommendationDto()
            {
                Id = place.Id.ToString(),
                Description = place.Rubrics,
                Title = place.FullName
            });
        }
        return recommendations;
    }

    public async Task<HttpResponseMessage> GetRecommendationsAsync(RequestRecDto placesForRecommendation,
        string requestUrl)
    {
        using HttpClient client = new HttpClient();
    
        // Убедимся, что URL корректен
        var baseUri = new Uri(requestUrl);
        client.BaseAddress = baseUri;

        // Отправляем запрос на тот же URL (без дублирования пути)
        var response = await client.PostAsJsonAsync("", placesForRecommendation);
        return response;
    }
}