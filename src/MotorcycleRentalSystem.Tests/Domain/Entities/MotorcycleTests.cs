using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Tests.Domain.Entities;

public class MotorcycleTests
{
    [Fact]
    public void CreateNewMotorcycle_ShouldInitializePropertiesCorrectly()
    {
        var id = "moto123";
        var year = 2023;
        var plate = "ABC1234";
        var model = "Honda CG";

        var motorcycle = Motorcycle.CreateNewMotorcycle(id, year, plate, model);

        Assert.Equal(id, motorcycle.Id);
        Assert.Equal(year, motorcycle.Year);
        Assert.Equal(plate, motorcycle.Plate);
        Assert.Equal(model, motorcycle.Model);
        Assert.True(motorcycle.IsAvailable);
    }

    [Fact]
    public void UpdatePlate_ShouldChangePlate()
    {
        var motorcycle = Motorcycle.CreateNewMotorcycle("1", 2022, "OLD123", "Yamaha");
        var newPlate = "NEW456";

        motorcycle.UpdatePlate(newPlate);

        Assert.Equal(newPlate, motorcycle.Plate);
    }

    [Fact]
    public void UpdatePlate_WithEmptyValue_ShouldThrowException()
    {
        var motorcycle = Motorcycle.CreateNewMotorcycle("1", 2022, "OLD123", "Yamaha");

        var ex = Assert.Throws<ArgumentException>(() => motorcycle.UpdatePlate(string.Empty));
        Assert.Equal("Placa não pode ser vazia.", ex.Message);
    }

    [Fact]
    public void Rent_WhenAvailable_ShouldSetIsAvailableToFalse()
    {
        var motorcycle = Motorcycle.CreateNewMotorcycle("1", 2022, "ABC123", "Suzuki");

        motorcycle.Rent();

        Assert.False(motorcycle.IsAvailable);
    }

    [Fact]
    public void Rent_WhenNotAvailable_ShouldThrowException()
    {
        var motorcycle = Motorcycle.CreateNewMotorcycle("1", 2022, "ABC123", "Suzuki");
        motorcycle.Rent(); // Already rented

        var ex = Assert.Throws<InvalidOperationException>(() => motorcycle.Rent());
        Assert.Equal("Moto já está alugada.", ex.Message);
    }

    [Fact]
    public void Devolve_ShouldSetIsAvailableToTrue()
    {
        var motorcycle = Motorcycle.CreateNewMotorcycle("1", 2022, "ABC123", "Suzuki");
        motorcycle.Rent();

        motorcycle.Devolve();

        Assert.True(motorcycle.IsAvailable);
    }
}