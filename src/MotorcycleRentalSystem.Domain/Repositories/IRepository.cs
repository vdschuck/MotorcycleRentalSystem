namespace MotorcycleRentalSystem.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(string id);

    Task<int> AddAsync(T entity);

    Task<int> UpdateAsync(T entity);
}