using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tourism.Dotnet.Parser.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Implementations;
using Tourism.Dotnet.Parser.DAL.Interfaces;

namespace Tourism.Dotnet.Parser.DAL.Extensions;

public static class DbExtension
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        services.AddDbContext<ParserDbContext>(x =>
            x.UseNpgsql("Server=postgres-parser;Port=5432;Database=Parser;User Id=postgres;Password=postgres"
            ));
        return services;
    }
}