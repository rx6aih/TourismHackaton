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
            .WithOne(p => p.City)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.Id).IsUnique();
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        var cities =  new List<City>
        {
            new City()
            {
                Id = 1,
                Image = "https://upload.wikimedia.org/wikipedia/commons/4/4d/Krasnodar_teatr.jpg",
                Title = "Краснодар",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 2,
                Image = "https://cdn.tripster.ru/photos/3111ac1d-f823-4254-850a-d044c0f43dd4.jpg",
                Title = "Сочи",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 3,
                Image = "https://cdn.tripster.ru/thumbs2/e2fa5262-4bd7-11ee-8656-d6c2bdfda223.1220x600.jpeg",
                Title = "Геленджик",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 4,
                Image = "https://cdn.tripster.ru/thumbs2/3dd20b6a-5e6d-11ee-a16e-cac32b1340bf.1220x600.jpeg",
                Title = "Анапа",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 5,
                Image = "https://vashotel-a.akamaihd.net/0000000240723647/x300/bb5f91d94ac69898555766ccd2576e25.jpg",
                Title = "Туапсе",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 6,
                Image = "https://guide-tours.ru/wp-content/uploads/2023/03/lazarevskoe-ili-loo-jpg.webp",
                Title = "Лоо",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 7,
                Image = "https://sutochno.ru/doc/images/galleries/182/eysk2.jpg",
                Title = "Ейск",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 8,
                Image =
                    "https://tripplanet.ru/wp-content/uploads/europe/russia/novorossysk/dostoprimechatelnosti-novorossijska.jpg",
                Title = "Новороссийск",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            },
            new City()
            {
                Id = 9,
                Image = "https://upload.wikimedia.org/wikipedia/commons/a/ad/%D4%B1%D6%80%D5%B4%D5%A1%D5%BE%D5%AB%D6%80%D5%AB_%D5%AF%D5%A5%D5%B6%D5%BF%D6%80%D5%B8%D5%B6%D5%A1%D5%AF%D5%A1%D5%B6_%D5%B0%D6%80%D5%A1%D5%BA%D5%A1%D5%AC%D5%A1%D5%AF.jpg",
                Title = "Армавир",
                Description = "string",
                Options = ["string"],
                Category = "string",
                Price = "string",
                Places = []
            }

        };
        builder.HasData(cities);
    }
}

