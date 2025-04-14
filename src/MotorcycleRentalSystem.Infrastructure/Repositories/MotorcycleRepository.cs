using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure.Repositories;

public class MotorcycleRepository(PostgreDbContext context) : Repository<Motorcycle>(context), IMotorcycleRepository
{
    public Task<int> DeleteByIdAsync(string id)
    {
        var motorcycle = context.Motorcycles.Find(id);

        if (motorcycle == null)
            return Task.FromResult(0);

        context.Motorcycles.Remove(motorcycle);
        return context.SaveChangesAsync();
    }

    public Task<List<Motorcycle>> FindAllAsync(string? plate, CancellationToken cancellationToken = default)
    {
        var query = context.Motorcycles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(plate))
        {
            query = query.Where(moto => moto.Plate.ToLower() == plate.ToLower());
        }

        return query.ToListAsync(cancellationToken);
    }
}