using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Responses;

public record MessageResponse
{
    [JsonPropertyName("mensagem")]
    public string Message { get; set; }

    public static MessageResponse From(string message)
    {
        return new MessageResponse { Message = message };
    }
}