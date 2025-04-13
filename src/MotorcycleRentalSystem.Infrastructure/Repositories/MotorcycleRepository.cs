using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure.Repositories;

public class MotorcycleRepository(PostgreDbContext context) : Repository<Motorcycle>(context), IMotorcycleRepository
{
    public Task<int> DeleteByIdAsync(string id)
    {
        context.Motorcycles.Where(motorcycle => motorcycle.Id == id).ExecuteDeleteAsync();
        return context.SaveChangesAsync();
    }
}