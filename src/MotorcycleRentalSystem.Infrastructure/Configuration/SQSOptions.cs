namespace MotorcycleRentalSystem.Infrastructure.Configuration;

public class SQSOptions
{
    public string QueueUrl { get; set; }

    public int Retries { get; set; }
}