using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public record GetMotorcycleResponse(Domain.Entities.Motorcycle motorcycle)
{
    [JsonPropertyName("identificador")] public string Id { get; set; } = motorcycle.Id;

    [JsonPropertyName("ano")] public int Year { get; set; } = motorcycle.Year;

    [JsonPropertyName("placa")] public string Plate { get; set; } = motorcycle.Plate;

    [JsonPropertyName("modelo")] public string Model { get; set; } = motorcycle.Model;
}