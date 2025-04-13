using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Application.DeliveryMan;

public class UploadDriverLicensePhotoRequest
{
    [JsonPropertyName("imagem_cnh")]
    public string DriveLicensePhoto { get; set; }
}