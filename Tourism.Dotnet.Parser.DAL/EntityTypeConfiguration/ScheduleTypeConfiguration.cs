using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Dotnet.Parser.DAL.Entities;

namespace Tourism.Dotnet.Parser.DAL.EntityTypeConfiguration;

public class ScheduleTypeConfiguration: IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> modelBuilder)
        {
            modelBuilder
                .HasOne(s => s.Monday)
                .WithOne()
                .HasForeignKey<Schedule>(s => s.MondayId)
                .OnDelete(DeleteBehavior.Cascade);
    
            modelBuilder
                .HasOne(s => s.Tuesday)
                .WithOne()
                .HasForeignKey<Schedule>(s => s.TuesdayId)
                .OnDelete(DeleteBehavior.Cascade);
    
            modelBuilder
                .HasOne(s => s.Wednesday)
                .WithOne()
                .HasForeignKey<Schedule>(s => s.WednesdayId)
                .OnDelete(DeleteBehavior.Cascade);
    
            modelBuilder
                .HasOne(s => s.Thursday)
                .WithOne()
                .HasForeignKey<Schedule>(s => s.ThursdayId)
                .OnDelete(DeleteBehavior.Cascade);
    
            modelBuilder
                .HasOne(s => s.Friday)
                .WithOne()
                .HasForeignKey<Schedule>(s => s.FridayId)
                .OnDelete(DeleteBehavior.Cascade);
    
            modelBuilder
                .HasOne(s => s.Saturday)
                .WithOne()
                .HasForeignKey<Schedule>(s => s.SaturdayId)
                .OnDelete(DeleteBehavior.Cascade);
    
            modelBuilder
                .HasOne(s => s.Sunday)
                .WithOne()
                .HasForeignKey<Schedule>(s => s.SundayId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.HasIndex(p => p.Id).IsUnique();
            modelBuilder.HasKey(p => p.Id);
        }
}
