namespace MotorcycleRentalSystem.Application.Motorcycle;

public interface IMotorcycleService
{
    Task<int> RegisterNewMotorcycleAsync(CreateMotorcycleRequest data);

    Task<int> UpdatePlateNumberAsync(string id, UpdateMotorcycleRequest data);

    Task<int> DeleteMotorcycleAsync(string id);

    Task<GetMotorcycleResponse?> GetMotorcycleByIdAsync(string id);

    Task<List<GetMotorcycleResponse>> GetAllMotorcyclesAsync();
}