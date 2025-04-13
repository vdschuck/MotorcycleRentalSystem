namespace MotorcycleRentalSystem.Application.DeliveryMan;

public interface IDeliveryManService
{
    Task<int> RegisterDeliveryMan(CreateDeliveryManRequest data);

    Task<int> UploadDriverLicensePhoto(string id, UploadDriverLicensePhotoRequest data);
}