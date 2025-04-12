using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Rent;

public record RentMotorcycleRequest
{
    [JsonPropertyName("entregador_id")] public string DeliveryManId { get; init; }

    [JsonPropertyName("moto_id")] public string MotorcycleId { get; init; }

    [JsonPropertyName("data_inicio")] public DateTime StartDate { get; init; }

    [JsonPropertyName("data_termino")] public DateTime EndDate { get; init; }

    [JsonPropertyName("data_previsao_termino")]
    public DateTime ExpectedEndDate { get; init; }

    [JsonPropertyName("plano")] public int Plan { get; init; }
}