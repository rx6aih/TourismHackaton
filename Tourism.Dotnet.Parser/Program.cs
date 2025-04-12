using System.Text.Json.Serialization;
using Tourism.Dotnet.Parser.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.DAL.Extensions;
using Tourism.Dotnet.Parser.DAL.Implementations;
using Tourism.Dotnet.Parser.DAL.Interfaces;
using Tourism.Dotnet.Parser.Utility;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("httpClient", o => { });
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<Repository<Place>>();
builder.Services.AddTransient<Repository<City>>();
builder.Services.AddTransient<ParserDbContext>();
builder.Services.AddDal();
var app = builder.Build();
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<ParserDbContext>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
