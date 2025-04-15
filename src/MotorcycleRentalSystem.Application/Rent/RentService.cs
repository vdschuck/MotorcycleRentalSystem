using Microsoft.Extensions.Logging;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Application.Rent;

public class RentService(
    IUnitOfWork unitOfWork,
    ILogger<RentService> logger)
    : IRentService
{
    public async Task MotorcycleRentalProcessAsync(RentMotorcycleRequest data)
    {
        logger.LogInformation("Renting motorcycle with MotoId {motoId} and DeliveryManId {deliveryManId}",
            data.MotorcycleId, data.DeliveryManId);

        var deliveryMan = await unitOfWork.DeliveryMan.GetByIdAsync(data.DeliveryManId);
        if (deliveryMan is null || !deliveryMan.HasMotorcycleLicense())
            throw new InvalidOperationException("The delivery man does not have a type A driver's license");

        var rental = Domain.Entities.Rent.CreateNewRental(data.MotorcycleId, data.DeliveryManId, data.StartDate,
            data.EndDate,
            data.ExpectedEndDate, data.Plan);
        var rentResult = await unitOfWork.Rents.AddAsync(rental);

        logger.LogInformation("Updating motorcycle with id {id}", data.MotorcycleId);
        var moto = await unitOfWork.Motorcycles.GetByIdAsync(data.MotorcycleId);
        if (moto == null || !moto.IsAvailableForRent())
            throw new InvalidOperationException("The motorcycle informed is not registered or not available");

        moto.Rent();
        var motoResult = await unitOfWork.Motorcycles.UpdateAsync(moto);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<GetRentResponse?> ConsultRentAsync(string id)
    {
        logger.LogInformation("Consulting rent with {id}", id);
        var result = await unitOfWork.Rents.GetByIdAsync(id);
        return result is null ? null : GetRentResponse.FromEntity(result);
    }

    public async Task<decimal> MotorcycleDevolveProcessAsync(string id, DevolveMotorcycleRequest data)
    {
        logger.LogInformation("Devolving motorcycle rent with id {id}", id);
        var rent = await unitOfWork.Rents.GetByIdAsync(id);
        if (rent == null)
            throw new InvalidOperationException("The rent informed is not registered");

        logger.LogInformation("Calculate the total amount due for rent id {id}", id);
        rent.ConcludeRental(data.DevolveDate);
        var totalAmountDue = rent.CalculateTotalAmount();
        await unitOfWork.Rents.UpdateAsync(rent);

        logger.LogInformation("Making the motorcycle available for new rental {id}", rent.MotorcycleId);
        var moto = await unitOfWork.Motorcycles.GetByIdAsync(rent.MotorcycleId);
        if (moto == null)
            throw new InvalidOperationException("The motorcycle informed is not registered");

        moto.Devolve();
        await unitOfWork.Motorcycles.UpdateAsync(moto);

        await unitOfWork.SaveChangesAsync();
        return totalAmountDue;
    }
}