using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Rent;

public record DevolveMotorcycleRequest
{
    [JsonPropertyName("data_devolucao")]
    public DateTime DevolveDate { get; init; }
}