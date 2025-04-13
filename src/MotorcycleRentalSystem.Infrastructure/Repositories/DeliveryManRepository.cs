using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure.Repositories;

public class DeliveryManRepository(PostgreDbContext context) : Repository<DeliveryMan>(context), IDeliveryManRepository
{
}