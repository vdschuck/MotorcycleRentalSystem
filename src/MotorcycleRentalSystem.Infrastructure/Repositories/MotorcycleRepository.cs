using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure.Repositories;

public class MotorcycleRepository(PostgreDbContext context) : IMotorcycleRepository
{
    public Task<List<Motorcycle>> GetAllAsync()
    {
        return context.Motorcycles.ToListAsync();
    }

    public Task<Motorcycle?> GetByIdAsync(string id)
    {
        return context.Motorcycles.FindAsync(id).AsTask();
    }

    public Task AddAsync(Motorcycle moto)
    {
        context.Motorcycles.Add(moto);
        return context.SaveChangesAsync();
    }
}