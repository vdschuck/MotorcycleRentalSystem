using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Domain.Repositories;

public interface IMotorcycleRepository
{
    Task<List<Motorcycle>> GetAllAsync();

    Task<Motorcycle?> GetByIdAsync(string id);

    Task AddAsync(Motorcycle moto);
}