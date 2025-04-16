using Amazon.SQS.Model;
using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Repositories;
using MotorcycleRentalSystem.Domain.Services;

namespace MotorcycleRentalSystem.Worker;

public class Worker(ISimpleQueueService sqs, IMotorcycleEventRepository motoEvent, ILogger<Worker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var messages = await ReceiveMessagesAsync();

            foreach (var message in messages)
            {
                try
                {
                    logger.LogInformation("Received MessageId: {id}", message.MessageId);

                    var newEvent = new MotorcycleEvent
                    {
                        Id = message.MessageId,
                        MessageBody = message.Body,
                        CreatedAt = DateTime.Now
                    };
                    await motoEvent.AddAsync(newEvent);

                    await sqs.DeleteMessageAsync(message.ReceiptHandle);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to process MessageId: {id} MessageBody: {body}", message.MessageId,
                        message.Body);
                    // TODO: Send message to DLQ
                }
            }

            await Task.Delay(30000, stoppingToken);
        }
    }

    private async Task<IEnumerable<Message>> ReceiveMessagesAsync()
    {
        try
        {
            return await sqs.ReceiveMessagesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to receive messages");
            return new List<Message>();
        }
    }
}