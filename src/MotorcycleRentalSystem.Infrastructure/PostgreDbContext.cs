using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Infrastructure;

public class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : DbContext(options)
{
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Rent> Rents { get; set; }

    public DbSet<DeliveryMan> DeliveryMans { get; set; }

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
        modelBuilder.Entity<Rent>().Property(m => m.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Rent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MotorcycleId).IsRequired();
            entity.Property(e => e.DeliveryManId).IsRequired();
            entity.Property(e => e.Plan);
            entity.Property(e => e.StartDate).IsRequired().HasMaxLength(10);
            entity.Property(e => e.EndDate).IsRequired().HasMaxLength(10);
            entity.Property(e => e.ExpectedEndDate).IsRequired().HasMaxLength(10);

            entity.HasOne(e => e.Motorcycle)
                .WithMany()
                .HasForeignKey(e => e.MotorcycleId)
                .IsRequired();
        });

        // DeliveryMan
        modelBuilder.Entity<DeliveryMan>().ToTable("DeliveryMan", "public");
        modelBuilder.Entity<DeliveryMan>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LegalEntity).IsRequired().HasMaxLength(50);
            entity.Property(e => e.DateOfBirth).IsRequired().HasMaxLength(10);
            entity.Property(e => e.DriveLicenseNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.DriveLicenseType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.DriveLicensePhoto).IsRequired();
        });
    }
}