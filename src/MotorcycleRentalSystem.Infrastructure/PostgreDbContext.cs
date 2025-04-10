using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Infrastructure;

public class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : DbContext(options)
{
    public DbSet<Motorcycle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Motorcycle
        modelBuilder.Entity<Motorcycle>().ToTable("Motorcycle", "public");
        modelBuilder.Entity<Motorcycle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Plate).IsRequired().HasMaxLength(7);
            entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Year).IsRequired().HasMaxLength(4);
            entity.Property(e => e.IsAvailable).IsRequired().HasDefaultValue(true);
        });
    }
}