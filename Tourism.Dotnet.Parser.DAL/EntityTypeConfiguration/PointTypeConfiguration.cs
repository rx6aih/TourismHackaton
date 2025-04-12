using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Dotnet.Parser.DAL.Entities;

namespace Tourism.Dotnet.Parser.DAL.EntityTypeConfiguration;

public class PointTypeConfiguration : IEntityTypeConfiguration<Point>
{
    public void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id).IsUnique();
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}