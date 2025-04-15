using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Tests.Domain.Entities;

public class RentTests
{
    [Fact]
    public void CreateNewRental_ShouldInitializePropertiesCorrectly()
    {
        var deliveryManId = "delivery123";
        var motorcycleId = "moto456";
        var startDate = new DateTime(2025, 4, 10);
        var expectedEndDate = new DateTime(2025, 4, 15);
        var endDate = expectedEndDate;
        var plan = 3;

        var rent = Rent.CreateNewRental(deliveryManId, motorcycleId, startDate, endDate, expectedEndDate, plan);

        Assert.Equal(deliveryManId, rent.DeliveryManId);
        Assert.Equal(motorcycleId, rent.MotorcycleId);
        Assert.Equal(startDate, rent.StartDate);
        Assert.Equal(endDate, rent.EndDate);
        Assert.Equal(expectedEndDate, rent.ExpectedEndDate);
        Assert.Equal(plan, rent.Plan);
        Assert.Equal(10, rent.DailyRate);
    }

    [Fact]
    public void ConcludeRental_ShouldUpdateEndDate()
    {
        var rent = Rent.CreateNewRental("d1", "m1", DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-1), DateTime.Now,
            1);
        var newEndDate = DateTime.Now;

        rent.ConcludeRental(newEndDate);

        Assert.Equal(newEndDate, rent.EndDate);
    }

    [Fact]
    public void CalculateTotalAmount_ShouldThrowException_WhenDatesAreNotProvided()
    {
        var rent = Rent.CreateNewRental("d1", "m1", default, default, default, 15);
        var exception = Assert.Throws<InvalidOperationException>(() => rent.CalculateTotalAmount());
        Assert.Equal("The start date, end date, and expected end date must all be provided", exception.Message);
    }

    [Fact]
    public void CalculateTotalAmount_ShouldThrowException_WhenEndDateIsBeforeStartDate()
    {
        var rent = Rent.CreateNewRental("d1", "m1", DateTime.Parse("2025-04-01"), DateTime.Parse("2025-03-30"),
            DateTime.Parse("2025-04-10"), 15);
        var exception = Assert.Throws<InvalidOperationException>(() => rent.CalculateTotalAmount());
        Assert.Equal("The end date must be later than the start date", exception.Message);
    }

    [Fact]
    public void CalculateTotalAmount_ShouldCalculateBaseAmountCorrectly()
    {
        var rent = Rent.CreateNewRental("d1", "m1", DateTime.Parse("2025-04-01"), DateTime.Parse("2025-04-10"),
            DateTime.Parse("2025-04-10"), 15);
        var totalAmount = rent.CalculateTotalAmount();
        var expectedAmount = 9 * 28m; // 9 days * 28 per day
        Assert.Equal(expectedAmount, totalAmount);
    }

    [Fact]
    public void CalculateTotalAmount_ShouldApplyLateReturnCharge_WhenReturnIsAfterExpected()
    {
        var rent = Rent.CreateNewRental("d1", "m1", DateTime.Parse("2025-04-01"), DateTime.Parse("2025-04-12"),
            DateTime.Parse("2025-04-10"), 15);
        var totalAmount = rent.CalculateTotalAmount();
        var expectedAmount = 11 * 28m + 2 * 50m; // 11 days + 2 days penalty (50%)
        Assert.Equal(expectedAmount, totalAmount);
    }
}