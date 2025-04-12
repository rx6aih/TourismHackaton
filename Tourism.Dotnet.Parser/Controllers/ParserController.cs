using Microsoft.AspNetCore.Mvc;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.Services;
using Tourism.Dotnet.Parser.Utility.Dto;

namespace Tourism.Dotnet.Parser.Controllers;

[ApiController]
[Route("/api/parser")]
public class ParserController(IHttpClientFactory factory) : ControllerBase
{
    [HttpGet("gis")]
    public async Task<IActionResult> GisParse([FromQuery] string city, [FromQuery] int page)
    {
        GisParser gisParser = new GisParser(factory);
        GisDTO dtos = await gisParser.FetchPlaces(city,page);
        List<Place> places = await gisParser.ConvertToPlaces(dtos);
        return Ok(places);
    }
}