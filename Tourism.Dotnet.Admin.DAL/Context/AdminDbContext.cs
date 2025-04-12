using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tourism.Dotnet.Admin.DAL.Entities;
using Tourism.Dotnet.Admin.DAL.EntityTypeConfiguration;

namespace Tourism.Dotnet.Admin.DAL.Context;

public class AdminDbContext(DbContextOptions<AdminDbContext> options,
    IOptions<AuthorizationOptions> authOptions) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
        
        base.OnModelCreating(modelBuilder);
    }
}