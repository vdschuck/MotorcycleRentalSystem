using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure.Repositories;

public class MotorcycleEventRepository(PostgreDbContext context)
    : Repository<MotorcycleEvent>(context), IMotorcycleEventRepository
{
}