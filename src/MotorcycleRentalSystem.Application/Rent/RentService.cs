using Microsoft.Extensions.Logging;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Application.Rent;

public class RentService(
    IRentRepository repository,
    IMotorcycleRepository motorcycleRepository,
    ILogger<RentService> logger)
    : IRentService
{
    public Task ConsultRentAsync(string id)
    {
        logger.LogInformation("Consulting rent with {id}", id);
        return repository.GetByIdAsync(id);
    }

    public async Task<(int, int)> MotorcycleDevolveProcessAsync(string id)
    {
        logger.LogInformation("Devolving motorcycle rent with {id}", id);
        var rent = await repository.GetByIdAsync(id);
        if (rent == null) return (0, 0);
        rent.ConcludeRental();
        var rentUpdate = await repository.UpdateAsync(rent);

        logger.LogInformation("Making the motorcycle available for new rental {id}", rent.MotorcycleId);
        var moto = await motorcycleRepository.GetByIdAsync(rent.MotorcycleId);
        if (moto == null) return (rentUpdate, 0);
        moto.Devolve();
        var motoUpdate = await motorcycleRepository.UpdateAsync(moto);

        return (rentUpdate, motoUpdate);
    }

    public async Task<(int, int)> MotorcycleRentalProcessAsync(RentMotorcycleRequest data)
    {
        logger.LogInformation("Renting motorcycle with {motoId} {deliveryManId}", data.MotorcycleId,
            data.DeliveryManId);
        var rental = new Domain.Entities.Rent();
        rental.CreateNewRental(data.MotorcycleId, data.DeliveryManId, data.StartDate, data.EndDate,
            data.ExpectedEndDate, data.Plan);
        var rentUpdate = await repository.AddAsync(rental);

        logger.LogInformation("Renting motorcycle with {id}", data.MotorcycleId);
        var moto = await motorcycleRepository.GetByIdAsync(data.MotorcycleId);
        if (moto == null) return (rentUpdate, 0);
        moto.Rent();
        var motoUpdate = await motorcycleRepository.UpdateAsync(moto);

        return (rentUpdate, motoUpdate);
    }
}