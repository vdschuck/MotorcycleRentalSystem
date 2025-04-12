namespace MotorcycleRentalSystem.Domain.Entities;

public class Rent
{
    public string DeliveryManId { get; private set; }
    public string MotorcycleId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime ExpectedEndDate { get; private set; }
    public int Plan { get; private set; }


    public DeliveryMan DeliveryMan { get; private set; }
    public Motorcycle Motorcycle { get; private set; }

    public void ConcludeRental()
    {
        EndDate = DateTime.Now;
    }

    public void CreateNewRental(string deliveryManId, string motorcycleId, DateTime startDate, DateTime endDate,
        DateTime expectedEndTime, int plan)
    {
        DeliveryManId = deliveryManId;
        MotorcycleId = motorcycleId;
        StartDate = startDate;
        EndDate = endDate;
        ExpectedEndDate = expectedEndTime;
        Plan = plan;
    }
}