using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MotorcycleRentalSystem.Domain.Repositories;
using MotorcycleRentalSystem.Domain.Services;
using MotorcycleRentalSystem.Infrastructure.Configuration;

namespace MotorcycleRentalSystem.Application.DeliveryMan;

public class DeliveryManService(
    IDeliveryManRepository deliveryManRepository,
    ISimpleStorageService s3,
    IOptions<AWSOptions> awsOptions,
    ILogger<DeliveryManService> logger)
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

    public async Task UploadDriverLicensePhoto(string id, UploadDriverLicensePhotoRequest data)
    {
        var deliveryMan = await deliveryManRepository.GetByIdAsync(id);
        if (deliveryMan is null)
            throw new InvalidOperationException("The informed driver was not found");

        logger.LogInformation("Uploading driver license photo");
        var imageBytes = Convert.FromBase64String(data.DriveLicensePhoto);
        var filename = $"cnh_{id}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
        var urlImage = await s3.UploadImagemAsync(imageBytes, filename, awsOptions.Value.S3.DriveLicensePhoto);
        deliveryMan.UpdateDriverLicensePhoto(urlImage);
        await deliveryManRepository.UpdateAsync(deliveryMan);
    }
}