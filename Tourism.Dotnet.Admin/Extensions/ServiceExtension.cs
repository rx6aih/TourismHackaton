using Tourism.Dotnet.Admin.Services;
using Tourism.Dotnet.Admin.Utility;
using Tourism.Dotnet.Admin.Utility.Jwt;

namespace Tourism.Dotnet.Admin.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<JwtProvider>();
        services.AddScoped<PasswordHasher>();
        return services;
    }
}