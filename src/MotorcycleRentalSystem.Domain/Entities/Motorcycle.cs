namespace MotorcycleRentalSystem.Domain.Entities;

public class Motorcycle {
    public string Id { get; private set; }
    public int Year { get; private set; }
    public string Plate { get; private set; }
    public string Model { get; private set; }
    public bool IsAvailable { get; private set; }

    public void Rent() {
        if (!IsAvailable)
        {
            throw new InvalidOperationException("Moto já está alugada.");
        } 
        IsAvailable = false;
    }

    public void Devolve() => IsAvailable = true;
}