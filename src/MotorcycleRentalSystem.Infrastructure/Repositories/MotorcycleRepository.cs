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

    public Task<int> UpdateAsync(Motorcycle moto)
    {
        context.Motorcycles.Update(moto);
        return context.SaveChangesAsync();
    }

    public Task<int> DeleteByIdAsync(string id)
    {
        context.Motorcycles.Where(motorcycle => motorcycle.Id == id).ExecuteDeleteAsync();
        return context.SaveChangesAsync();
    }

    public Task<int> AddAsync(Motorcycle moto)
    {
        context.Motorcycles.Add(moto);
        return context.SaveChangesAsync();
    }
}