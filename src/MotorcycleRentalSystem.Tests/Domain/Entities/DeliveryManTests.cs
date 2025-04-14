using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Tests.Domain.Entities;

public class DeliveryManTests
{
    [Fact]
    public void CreateDeliveryMan_ShouldInitializeAllPropertiesCorrectly()
    {
        var id = "delivery123";
        var name = "João Silva";
        var legalEntity = "12345678900";
        var birthDate = new DateTime(1990, 5, 20);
        var licenseNumber = "CNH1234567";
        var licenseType = "A";
        var licensePhoto = "photo_url.jpg";

        var deliveryMan =
            DeliveryMan.CreateDeliveryMan(id, name, legalEntity, birthDate, licenseNumber, licenseType, licensePhoto);

        Assert.Equal(id, deliveryMan.Id);
        Assert.Equal(name, deliveryMan.Name);
        Assert.Equal(legalEntity, deliveryMan.LegalEntity);
        Assert.Equal(birthDate, deliveryMan.DateOfBirth);
        Assert.Equal(licenseNumber, deliveryMan.DriveLicenseNumber);
        Assert.Equal(licenseType, deliveryMan.DriveLicenseType);
        Assert.Equal(licensePhoto, deliveryMan.DriveLicensePhoto);
    }

    [Fact]
    public void UpdateDriverLicensePhoto_ShouldChangePhotoSuccessfully()
    {
        var deliveryMan = DeliveryMan.CreateDeliveryMan("id1", "Maria", "11111111111", DateTime.Now.AddYears(-30),
            "CNH0001", "A", "old_photo.jpg");

        var newPhoto = "new_photo.jpg";

        deliveryMan.UpdateDriverLicensePhoto(newPhoto);

        Assert.Equal(newPhoto, deliveryMan.DriveLicensePhoto);
    }
}