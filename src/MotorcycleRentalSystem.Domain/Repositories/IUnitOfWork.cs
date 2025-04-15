namespace MotorcycleRentalSystem.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IMotorcycleRepository Motorcycles { get; }

    IDeliveryManRepository DeliveryMan { get; }

    IRentRepository Rents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}