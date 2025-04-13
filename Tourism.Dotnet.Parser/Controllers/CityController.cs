using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Tourism.Dotnet.Parser.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.DAL.Implementations;
using Tourism.Dotnet.Parser.Services;
using Tourism.Dotnet.Parser.Utility;
using Tourism.Dotnet.Parser.Utility.Dto;

namespace Tourism.Dotnet.Parser.Controllers;

[ApiController]
[Route("/api/city")]
public class CityController(Repository<City> cititesRepository, Repository<Place> placesRepository, ParserDbContext context, IHttpClientFactory factory) : ControllerBase
{
    CityService _service = new (cititesRepository, placesRepository,context);
    HttpClient _client = factory.CreateClient();
    [HttpGet]
    public async Task<List<City>> Get()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task AddCity([FromBody] CityDto cityDto)
    {
        await _service.AddAsync(cityDto);
    }

    [HttpDelete]
    public async Task DeleteCity(int id)
    {
        await _service.DeleteAsync(id);
    }

    [HttpPut]
    public async Task UpdateCity([FromQuery] int id, [FromQuery] string name, [FromQuery]List<int> places)
    {
        await _service.UpdateAsync(id, name, places);
    }

    [HttpGet("/recommendations")]
    public async Task<IActionResult> GetRecommendations([FromBody]RequestRecDto placesForRecommendation, [FromQuery]string requestUrl)
    {
        return Ok(await _service.GetRecommendationsAsync(placesForRecommendation, requestUrl));
    }
}