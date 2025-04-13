using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.Rent;

public record GetRentResponse(
    string deliveryManId,
    string motorcycleId,
    DateTime startDate,
    DateTime endDate,
    DateTime expectedEndDate,
    int plan,
    int dailyRate)
{
    [JsonPropertyName("entregador_id")]
    public string DeliveryManId { get; init; }

    [JsonPropertyName("moto_id")]
    public string MotorcycleId { get; init; }

    [JsonPropertyName("data_inicio")]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("data_termino")]
    public DateTime EndDate { get; init; }

    [JsonPropertyName("data_previsao_termino")]
    public DateTime ExpectedEndDate { get; init; }

    [JsonPropertyName("plano")]
    public int Plan { get; init; }

    [JsonPropertyName("valor_diaria")]
    public int DailyRate { get; init; }

    public static GetRentResponse FromEntity(Domain.Entities.Rent rent)
    {
        return new GetRentResponse(rent.DeliveryManId, rent.MotorcycleId, rent.StartDate, rent.EndDate,
            rent.ExpectedEndDate, rent.Plan, rent.DailyRate);
    }
}