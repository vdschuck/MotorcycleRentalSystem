namespace MotorcycleRentalSystem.Domain.Entities;

public class MotorcycleEvent
{
    public string Id { get; set; }

    public string MessageBody { get; set; }

    public DateTime CreatedAt { get; set; }
}