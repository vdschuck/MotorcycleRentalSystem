using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public record UpdateMotorcycleRequest
{
    [JsonPropertyName("placa")]
    public string Plate { get; init; }
}