using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Domain.Repositories;

public interface IDeliveryManRepository
{
    Task<int> AddAsync(DeliveryMan deliveryMan);

    Task<DeliveryMan?> GetByIdAsync(string id);

    Task<int> UpdateAsync(DeliveryMan deliveryMan);
}