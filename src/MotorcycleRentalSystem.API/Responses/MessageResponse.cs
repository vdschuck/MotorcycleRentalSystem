using System.Text.Json.Serialization;

namespace MotorcycleRentalSystem.Responses;

public record MessageResponse
{
    [JsonPropertyName("mensagem")]
    public string Message { get; private set; }

    [JsonPropertyName("dados")]
    public object Data { get; private set; }

    public static MessageResponse From(string message)
    {
        return new MessageResponse { Message = message };
    }

    public static MessageResponse From<T>(string message, T data)
    {
        return new MessageResponse { Message = message, Data = data };
    }
}