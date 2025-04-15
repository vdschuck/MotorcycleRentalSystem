using MotorcycleRentalSystem.Domain.Enums;

namespace MotorcycleRentalSystem.Domain.Entities;

public class DeliveryMan
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public string LegalEntity { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public string DriveLicenseNumber { get; private set; }

    public string DriveLicenseType { get; private set; }

    public string DriveLicensePhoto { get; private set; }

    public void UpdateDriverLicensePhoto(string photo)
    {
        DriveLicensePhoto = photo;
    }

    public bool IsDriveLicenseTypeValid()
    {
        return Enum.IsDefined(typeof(DriveLicenseType), DriveLicenseType.ToUpper());
    }

    public bool HasMotorcycleLicense()
    {
        return DriveLicenseType.Equals(Enums.DriveLicenseType.A.ToString(),
            StringComparison.InvariantCultureIgnoreCase);
    }

    public static DeliveryMan CreateDeliveryMan(string id, string name, string legalEntity, DateTime dateOfBirth,
        string driveLicenseNumber, string driveLicenseType, string driveLicensePhoto)
    {
        return new DeliveryMan
        {
            Id = id,
            Name = name,
            LegalEntity = legalEntity,
            DateOfBirth = dateOfBirth,
            DriveLicenseNumber = driveLicenseNumber,
            DriveLicenseType = driveLicenseType,
            DriveLicensePhoto = driveLicensePhoto
        };
    }
}