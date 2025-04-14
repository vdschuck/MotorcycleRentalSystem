using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public record SearchMotorcycleRequest
{
    [JsonPropertyName("placa")]
    public string Plate { get; init; }
}