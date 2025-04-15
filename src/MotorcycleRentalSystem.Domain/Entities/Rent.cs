namespace MotorcycleRentalSystem.Domain.Entities;

public class Rent
{
    public string Id { get; private set; }
    public string DeliveryManId { get; private set; }
    public string MotorcycleId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime ExpectedEndDate { get; private set; }
    public int Plan { get; private set; }
    public int DailyRate { get; private set; }

    public DeliveryMan DeliveryMan { get; private set; }
    public Motorcycle Motorcycle { get; private set; }

    public static Rent CreateNewRental(string deliveryManId, string motorcycleId, DateTime startDate, DateTime endDate,
        DateTime expectedEndTime, int plan)
    {
        return new Rent
        {
            DeliveryManId = deliveryManId,
            MotorcycleId = motorcycleId,
            StartDate = startDate,
            EndDate = endDate,
            ExpectedEndDate = expectedEndTime,
            Plan = plan,
            DailyRate = 10
        };
    }

    public void ConcludeRental(DateTime devolveDate)
    {
        EndDate = devolveDate;
    }

    public decimal CalculateTotalAmount()
    {
        if (StartDate == default || EndDate == default || ExpectedEndDate == default)
            throw new InvalidOperationException(
                "The start date, end date, and expected end date must all be provided");

        if (EndDate <= StartDate)
            throw new InvalidOperationException("The end date must be later than the start date");

        var rentalPlan = RentalPlan.FromDays(Plan);
        var actualDays = (EndDate - StartDate).Days;
        var baseAmount = actualDays * rentalPlan.DailyRate;

        if (IsEarlyReturn())
        {
            var unusedDays = (ExpectedEndDate - EndDate).Days;
            var penalty = unusedDays * rentalPlan.DailyRate * rentalPlan.EarlyReturnPenaltyRate;
            baseAmount += penalty;
        }
        else if (IsLateReturn())
        {
            var extraDays = (EndDate - ExpectedEndDate).Days;
            var extraCharge = extraDays * 50m;
            baseAmount += extraCharge;
        }

        return baseAmount;
    }

    private bool IsLateReturn()
    {
        return EndDate > ExpectedEndDate;
    }

    private bool IsEarlyReturn()
    {
        return EndDate < ExpectedEndDate;
    }
}