using Microsoft.AspNetCore.Mvc;
using Tourism.Dotnet.Parser.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.DAL.Implementations;
using Tourism.Dotnet.Parser.Services;
using Tourism.Dotnet.Parser.Utility.Dto;

namespace Tourism.Dotnet.Parser.Controllers;

[ApiController]
[Route("/api/city")]
public class CityController(Repository<City> cititesRepository, Repository<Place> placesRepository, ParserDbContext context) : ControllerBase
{
    CityService service = new CityService(cititesRepository, placesRepository,context);
    [HttpGet]
    public async Task<List<City>> Get()
    {
        return await service.GetAllAsync();
    }

    [HttpPost]
    public async Task AddCity([FromBody] CityDto cityDto)
    {
        await service.AddAsync(cityDto);
    }

    [HttpDelete]
    public async Task DeleteCity(int id)
    {
        await service.DeleteAsync(id);
    }

    [HttpPut]
    public async Task UpdateCity([FromQuery] int id, [FromQuery] string name, [FromQuery]List<int> places)
    {
        await service.UpdateAsync(id, name, places);
    }
}