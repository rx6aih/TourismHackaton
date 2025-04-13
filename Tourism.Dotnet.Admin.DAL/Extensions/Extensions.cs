using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tourism.Dotnet.Admin.DAL.Context;
using Tourism.Dotnet.Parser.DAL.Implementations;
using Tourism.Dotnet.Parser.DAL.Interfaces;

namespace Tourism.Dotnet.Admin.DAL.Extensions;

public static class Extensions
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddDbContext<AdminDbContext>(x =>
            x.UseNpgsql("Server=postgres-parser;Port=5432;Database=Parser;User Id=postgres;Password=postgres"
            ));
        return services;
    }
}