using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Infrastructure.Repositories;

public class Repository<T>(DbContext context) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public Task<List<T>> GetAllAsync()
    {
        return _dbSet.ToListAsync();
    }

    public Task<T?> GetByIdAsync(string id)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    public Task<int> AddAsync(T entity)
    {
        _dbSet.Add(entity);
        return context.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return context.SaveChangesAsync();
    }
}