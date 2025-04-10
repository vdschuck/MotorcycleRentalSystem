namespace MotorcycleRentalSystem.Domain.Entities;

public class Rent
{
    public string DeliveryManId { get;private set; }
    public string MotorcycleId { get;private set; }
    public DateTime StartDate { get;private set; }
    public DateTime EndDate { get;private set; }
    public DateTime ExpectedEndDate { get;private set; }
    public int Plan { get;private set; }
}