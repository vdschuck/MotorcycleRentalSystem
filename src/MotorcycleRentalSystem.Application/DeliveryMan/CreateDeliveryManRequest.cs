using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.DeliveryMan;

public record CreateDeliveryManRequest
{
    [JsonPropertyName("identificador")]
    public string Id { get; init; }

    [JsonPropertyName("nome")]
    public string Name { get; init; }

    [JsonPropertyName("cnpj")]
    public string LegalEntity { get; init; }

    [JsonPropertyName("data_nascimento")]
    public DateTime DateOfBirth { get; init; }

    [JsonPropertyName("numero_cnh")]
    public string DriveLicenseNumber { get; init; }

    [JsonPropertyName("tipo_cnh")]
    public string DriveLicenseType { get; init; }

    [JsonPropertyName("imagem_cnh")]
    public string DriveLicensePhoto { get; init; }
}