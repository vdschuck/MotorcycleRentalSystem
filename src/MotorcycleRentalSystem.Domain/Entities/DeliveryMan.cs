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
}

