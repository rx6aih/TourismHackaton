using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Dotnet.Parser.DAL.Entities;

namespace Tourism.Dotnet.Parser.DAL.EntityTypeConfiguration;

public class PlaceTypeConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.HasIndex(p => p.Id).IsUnique();
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd(); 
        
        builder
            .HasOne(p => p.Point)
            .WithOne()
            .HasForeignKey<Place>(p => p.PointId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(p => p.Schedule)
            .WithOne()
            .HasForeignKey<Place>(p => p.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(p => p.City)
            .WithMany()
            .HasForeignKey(p => p.CityId).OnDelete(DeleteBehavior.Cascade);
    }
}
