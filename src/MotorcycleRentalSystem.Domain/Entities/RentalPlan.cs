namespace MotorcycleRentalSystem.Domain.Entities;

public class RentalPlan
{
    private RentalPlan(int days, decimal dailyRate, decimal penaltyRate)
    {
        Days = days;
        DailyRate = dailyRate;
        EarlyReturnPenaltyRate = penaltyRate;
    }

    public int Days { get; }
    public decimal DailyRate { get; }
    public decimal EarlyReturnPenaltyRate { get; }

    public static RentalPlan FromDays(int days)
    {
        return days switch
        {
            7 => new RentalPlan(7, 30m, 0.20m),
            15 => new RentalPlan(15, 28m, 0.40m),
            30 => new RentalPlan(30, 22m, 0m),
            45 => new RentalPlan(45, 20m, 0m),
            50 => new RentalPlan(50, 18m, 0m),
            _ => throw new InvalidOperationException("Invalid rental plan.")
        };
    }
}