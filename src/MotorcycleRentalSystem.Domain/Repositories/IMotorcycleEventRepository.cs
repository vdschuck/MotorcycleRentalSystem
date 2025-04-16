using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Domain.Repositories;

public interface IMotorcycleEventRepository
{
    Task<int> AddAsync(MotorcycleEvent entity);
}