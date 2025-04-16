namespace MotorcycleRentalSystem.Application.DeliveryMan;

public interface IDeliveryManService
{
    Task<int> RegisterDeliveryMan(CreateDeliveryManRequest data);

    Task UploadDriverLicensePhoto(string id, UploadDriverLicensePhotoRequest data);
}