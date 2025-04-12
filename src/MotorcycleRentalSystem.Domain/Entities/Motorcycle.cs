namespace MotorcycleRentalSystem.Domain.Entities;

public class Motorcycle
{
    public string Id { get; private set; }
    public int Year { get; private set; }
    public string Plate { get; private set; }
    public string Model { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public void UpdatePlate(string newPlate)
    {
        if (string.IsNullOrEmpty(newPlate)) throw new ArgumentException("Placa não pode ser vazia.");
        Plate = newPlate;
    }

    public void Rent()
    {
        if (!IsAvailable) throw new InvalidOperationException("Moto já está alugada.");
        IsAvailable = false;
    }

    public void Devolve()
    {
        IsAvailable = true;
    }

    public void CreateNewMotorcycle(string id, int year, string plateNumber, string model)
    {
        Id = id;
        Year = year;
        Plate = plateNumber;
        Model = model;
    }
}