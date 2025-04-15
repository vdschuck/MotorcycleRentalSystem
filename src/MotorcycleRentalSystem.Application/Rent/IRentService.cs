namespace MotorcycleRentalSystem.Application.Rent;

public interface IRentService
{
    Task<GetRentResponse?> ConsultRentAsync(string id);

    Task<decimal> MotorcycleDevolveProcessAsync(string id, DevolveMotorcycleRequest data);

    Task MotorcycleRentalProcessAsync(RentMotorcycleRequest data);
}