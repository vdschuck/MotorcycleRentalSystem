using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure.Repositories;

public class RentRepository(PostgreDbContext context) : IRentRepository
{
    public Task<Rent?> GetByIdAsync(string id)
    {
        return context.Rents.FindAsync(id).AsTask();
    }

    public Task<int> UpdateAsync(Rent rent)
    {
        context.Rents.Update(rent);
        return context.SaveChangesAsync();
    }

    public Task<int> AddAsync(Rent rent)
    {
        context.Rents.Add(rent);
        return context.SaveChangesAsync();
    }
}