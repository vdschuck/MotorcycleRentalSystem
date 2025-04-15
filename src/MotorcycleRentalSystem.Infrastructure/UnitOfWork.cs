using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure;

public class UnitOfWork(
    PostgreDbContext context,
    IMotorcycleRepository motorcycles,
    IDeliveryManRepository deliveryMan,
    IRentRepository rents)
    : IUnitOfWork
{
    public IMotorcycleRepository Motorcycles { get; } = motorcycles;
    public IDeliveryManRepository DeliveryMan { get; } = deliveryMan;
    public IRentRepository Rents { get; } = rents;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}