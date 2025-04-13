using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public record GetMotorcycleResponse(string id, int year, string plate, string model)
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
        return new GetMotorcycleResponse(motorcycle.Id, motorcycle.Year, motorcycle.Plate, motorcycle.Model);
    }
}