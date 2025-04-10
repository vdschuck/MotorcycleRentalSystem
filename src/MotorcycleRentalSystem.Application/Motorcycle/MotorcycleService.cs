using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public class MotorcycleService(IMotorcycleRepository repository) : IMotorcycleService
{
    public Task RegisterNewMotorcycleAsync(CreateMotorcycleRequest data)
    {
        return Task.FromResult("");
    }
}