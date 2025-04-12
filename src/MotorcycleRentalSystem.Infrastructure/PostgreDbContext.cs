using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Infrastructure;

public class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : DbContext(options)
{
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Rent> Rents { get; set; }

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

        // Rent
        modelBuilder.Entity<Rent>().ToTable("Rent", "public");
        modelBuilder.Entity<Rent>(entity =>
        {
            entity.HasKey(e => new { e.MotorcycleId, e.DeliveryManId });
            entity.Property(e => e.MotorcycleId).IsRequired();
            entity.Property(e => e.DeliveryManId).IsRequired();
            entity.Property(e => e.Plan);
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
            entity.Property(e => e.ExpectedEndDate).IsRequired();

            entity.HasOne(e => e.Motorcycle)
                .WithMany()
                .HasForeignKey(e => e.MotorcycleId)
                .IsRequired();
        });
    }
}