using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public record CreateMotorcycleRequest
{
    [JsonPropertyName("identificador")] public string Id { get; init; }

    [JsonPropertyName("ano")] public int Year { get; init; }

    [JsonPropertyName("modelo")] public string Model { get; init; }

    [JsonPropertyName("placa")] public string Plate { get; init; }
}