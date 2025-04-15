using Microsoft.Extensions.Logging;
using MotorcycleRentalSystem.Domain.Repositories;

namespace MotorcycleRentalSystem.Application.DeliveryMan;

public class DeliveryManService(IDeliveryManRepository deliveryManRepository, ILogger<DeliveryManService> logger)
    : IDeliveryManService
{
    public Task<int> RegisterDeliveryMan(CreateDeliveryManRequest data)
    {
        logger.LogInformation("Registering delivery man");
        var deliveryMan = Domain.Entities.DeliveryMan.CreateDeliveryMan(data.Id, data.Name, data.LegalEntity,
            data.DateOfBirth, data.DriveLicenseNumber, data.DriveLicenseType, data.DriveLicensePhoto);

        if (!deliveryMan.IsDriveLicenseTypeValid())
            return Task.FromResult(0);

        return deliveryManRepository.AddAsync(deliveryMan);
    }

    public async Task<int> UploadDriverLicensePhoto(string id, UploadDriverLicensePhotoRequest data)
    {
        logger.LogInformation("Uploading driver license photo");
        // TODO: upload photo for S3
        var photoPath = string.Empty;
        var deliveryMan = await deliveryManRepository.GetByIdAsync(id);
        if (deliveryMan is null) return 0;
        deliveryMan.UpdateDriverLicensePhoto(photoPath);
        return await deliveryManRepository.UpdateAsync(deliveryMan);
    }
}