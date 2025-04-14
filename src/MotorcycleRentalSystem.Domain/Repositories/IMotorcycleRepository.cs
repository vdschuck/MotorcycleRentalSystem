using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Domain.Repositories;

public interface IMotorcycleRepository
{
    Task<List<Motorcycle>> FindAllAsync(string? plate, CancellationToken cancellationToken = default);

    Task<Motorcycle?> GetByIdAsync(string id);

    Task<int> AddAsync(Motorcycle moto);

    Task<int> UpdateAsync(Motorcycle moto);

    Task<int> DeleteByIdAsync(string id);
}