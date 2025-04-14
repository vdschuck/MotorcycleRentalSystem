using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public record GetMotorcycleResponse
{
    [JsonPropertyName("identificador")]
    public string Id { get; init; }

    [JsonPropertyName("ano")]
    public int Year { get; init; }

    [JsonPropertyName("placa")]
    public string Plate { get; init; }

    [JsonPropertyName("modelo")]
    public string Model { get; init; }

    public static GetMotorcycleResponse FromEntity(Domain.Entities.Motorcycle motorcycle)
    {
        return new GetMotorcycleResponse
        {
            Id = motorcycle.Id,
            Year = motorcycle.Year,
            Plate = motorcycle.Plate,
            Model = motorcycle.Model
        };
    }
}