using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Tourism.Dotnet.Parser.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.DAL.Implementations;
using Tourism.Dotnet.Parser.Services;

namespace Tourism.Dotnet.Parser.Controllers;
[ApiController]
[Route("/api/places")]
public class PlacesController(IHttpClientFactory factory, Repository<Place> repository, ParserDbContext context) : ControllerBase
{
    private readonly PlacesService _service = new PlacesService(factory, repository,context);

    [HttpPost("/addPlaces")]
    public async Task AddPlaces([FromBody] List<Place> places)
    {
        await _service.AddPlaces(places);
    }

    [HttpPost("/addPlace")]
    public async Task AddPlace([FromBody] Place place)
    {
        await _service.AddPlaces(place);
    }
    
    [HttpGet("/fetchPlaces")]
    public async Task<List<Place>> FetchPlaces([FromQuery][Optional]string city, [FromQuery][Optional]int page)
    {
        return await _service.ParseAndAddPlaces(city, page);
    }

    [HttpGet("/getAllPlaces")]
    public async Task<List<Place>> GetAllPlaces(CancellationToken cancellationToken = default)
    {
        return await _service.GetAllPlaces(cancellationToken);
    }

    [HttpDelete("/deletePlace")]
    public async Task DeletePlace([FromQuery] Guid id)
    {
        await _service.DeletePlace(id);
    }
}