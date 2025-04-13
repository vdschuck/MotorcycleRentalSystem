namespace MotorcycleRentalSystem.Application.Rent;

public interface IRentService
{
    Task<GetRentResponse?> ConsultRentAsync(string id);

    Task<(int, int)> MotorcycleDevolveProcessAsync(string id, DevolveMotorcycleRequest data);

    Task<(int, int)> MotorcycleRentalProcessAsync(RentMotorcycleRequest data);
}