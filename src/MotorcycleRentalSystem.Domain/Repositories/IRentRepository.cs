using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Domain.Repositories;

public interface IRentRepository
{
    Task<Rent?> GetByIdAsync(string id);

    Task<int> UpdateAsync(Rent rent);

    Task<int> AddAsync(Rent rent);
}