using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Infrastructure;
using Testcontainers.PostgreSql;

namespace MotorcycleRentalSystem.Tests.Fixtures;

public class DatabaseContainerFixture : IAsyncLifetime
{
    public PostgreSqlContainer PostgresContainer { get; private set; }

    public DbContextOptions<PostgreDbContext> DbContextOptions { get; private set; }

    public string ConnectionString => PostgresContainer.GetConnectionString();

    public async Task InitializeAsync()
    {
        PostgresContainer = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("password")
            .Build();

        await PostgresContainer.StartAsync();

        DbContextOptions = new DbContextOptionsBuilder<PostgreDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        await using var context = CreateContext();
        await context.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await PostgresContainer.StopAsync();
    }

    public PostgreDbContext CreateContext()
    {
        return new PostgreDbContext(DbContextOptions);
    }

    public async Task ClearDatabaseAsync()
    {
        await using var context = new PostgreDbContext(DbContextOptions);
        var motorcyles = await context.Motorcycles.ToListAsync();
        context.Motorcycles.RemoveRange(motorcyles);
        await context.SaveChangesAsync();
    }
}