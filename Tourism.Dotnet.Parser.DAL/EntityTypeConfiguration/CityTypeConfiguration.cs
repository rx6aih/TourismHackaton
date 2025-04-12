using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Dotnet.Parser.DAL.Entities;

namespace Tourism.Dotnet.Parser.DAL.EntityTypeConfiguration;

public class CityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasMany(x => x.Places)
            .WithOne(p=>p.City)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(c => c.Id).IsUnique();
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd(); 
    }
}