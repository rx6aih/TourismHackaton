using Microsoft.EntityFrameworkCore;
using Tourism.Dotnet.Parser.DAL.Entities;
using Tourism.Dotnet.Parser.DAL.EntityTypeConfiguration;

namespace Tourism.Dotnet.Parser.DAL.Context;

public class ParserDbContext(DbContextOptions<ParserDbContext> options) : DbContext(options)
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<DaySchedule> DaySchedules { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<WorkingHours> WorkingHours { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ScheduleTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DayScheduleTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PlaceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}