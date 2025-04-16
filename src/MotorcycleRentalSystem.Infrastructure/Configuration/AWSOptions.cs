namespace MotorcycleRentalSystem.Infrastructure.Configuration;

public class AWSOptions
{
    public string Profile { get; set; }
    public string Region { get; set; }
    public S3Options S3 { get; set; }
    public SQSOptions SQS { get; set; }
}