namespace MotorcycleRentalSystem.Application.Rent;

public interface IRentService
{
    Task ConsultRentAsync(string id);

    Task<(int, int)> MotorcycleDevolveProcessAsync(string id);

    Task<(int, int)> MotorcycleRentalProcessAsync(RentMotorcycleRequest data);
}