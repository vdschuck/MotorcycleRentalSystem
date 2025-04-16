using Bogus;
using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Infrastructure;
using MotorcycleRentalSystem.Infrastructure.Repositories;
using MotorcycleRentalSystem.Tests.Fixtures;

namespace MotorcycleRentalSystem.Tests.Infrastructure.Repositories;

[Collection("Database collection")]
public class MotorcycleRepositoryTests
{
    private readonly PostgreDbContext _context;
    private readonly Faker _faker;
    private readonly DatabaseContainerFixture _fixture;
    private readonly MotorcycleRepository _repository;

    public MotorcycleRepositoryTests(DatabaseContainerFixture fixture)
    {
        _faker = new Faker();
        _fixture = fixture;
        _context = _fixture.CreateContext();
        _repository = new MotorcycleRepository(_context);
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldDeleteMotorcycle_WhenExists()
    {
        await _fixture.ClearDatabaseAsync();

        var motorcycle = Motorcycle.CreateNewMotorcycle(
            _faker.Random.Guid().ToString(),
            _faker.Random.Int(2000, 2025),
            _faker.Random.String2(7),
            _faker.Vehicle.Model()
        );

        _context.Motorcycles.Add(motorcycle);
        await _context.SaveChangesAsync();

        var result = await _repository.DeleteByIdAsync(motorcycle.Id);
        Assert.Equal(1, result);
        var deletedMotorcycle = await _context.Motorcycles.FindAsync(motorcycle.Id);
        Assert.Null(deletedMotorcycle);
    }

    [Fact]
    public async Task FindAllAsync_ShouldReturnMotorcycles_WhenPlateMatches()
    {
        await _fixture.ClearDatabaseAsync();

        var motorcycle1 = Motorcycle.CreateNewMotorcycle(
            _faker.Random.Guid().ToString(),
            _faker.Random.Int(2000, 2025),
            "ABC1234",
            _faker.Vehicle.Model()
        );
        var motorcycle2 = Motorcycle.CreateNewMotorcycle(
            _faker.Random.Guid().ToString(),
            _faker.Random.Int(2000, 2025),
            _faker.Random.String2(7),
            _faker.Vehicle.Model()
        );

        _context.Motorcycles.AddRange(motorcycle1, motorcycle2);
        await _context.SaveChangesAsync();

        var result = await _repository.FindAllAsync("ABC1234");
        Assert.Single(result);
        Assert.Equal("ABC1234", result.First().Plate);
    }

    [Fact]
    public async Task FindAllAsync_ShouldReturnAllMotorcycles_WhenPlateIsNullOrEmpty()
    {
        await _fixture.ClearDatabaseAsync();

        var motorcycle1 = Motorcycle.CreateNewMotorcycle(
            _faker.Random.Guid().ToString(),
            _faker.Random.Int(2000, 2025),
            _faker.Random.String2(7),
            _faker.Vehicle.Model()
        );
        var motorcycle2 = Motorcycle.CreateNewMotorcycle(
            _faker.Random.Guid().ToString(),
            _faker.Random.Int(2000, 2025),
            _faker.Random.String2(7),
            _faker.Vehicle.Model()
        );

        _context.Motorcycles.AddRange(motorcycle1, motorcycle2);
        await _context.SaveChangesAsync();

        var result = await _repository.FindAllAsync(string.Empty);
        Assert.Equal(2, result.Count);
    }
}