using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Dotnet.Parser.DAL.Entities;

namespace Tourism.Dotnet.Parser.DAL.EntityTypeConfiguration;


public class DayScheduleTypeConfiguration : IEntityTypeConfiguration<DaySchedule>
{
    public void Configure(EntityTypeBuilder<DaySchedule> builder)
    {
        builder
            .HasMany(d => d.WorkingHours)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
} 