using Amazon.SQS.Model;

namespace MotorcycleRentalSystem.Domain.Services;

public interface ISimpleQueueService
{
    Task SendMessageAsync(string message);

    Task<IEnumerable<Message>> ReceiveMessagesAsync();

    Task DeleteMessageAsync(string receiptHandle);
}