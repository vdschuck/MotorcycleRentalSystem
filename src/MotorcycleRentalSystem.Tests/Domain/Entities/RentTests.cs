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
}