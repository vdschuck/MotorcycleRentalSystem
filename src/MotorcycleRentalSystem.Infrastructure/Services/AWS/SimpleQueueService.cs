using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MotorcycleRentalSystem.Domain.Services;
using MotorcycleRentalSystem.Infrastructure.Configuration;

namespace MotorcycleRentalSystem.Infrastructure.Services.AWS;

public class SimpleQueueService : ISimpleQueueService
{
    private readonly AWSOptions _awsOptions;
    private readonly ILogger<SimpleQueueService> _logger;
    private readonly IAmazonSQS _sqsClient;

    public SimpleQueueService(IOptions<AWSOptions> awsOptions, ILogger<SimpleQueueService> logger)
    {
        _awsOptions = awsOptions.Value;
        _logger = logger;
        var config = new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(_awsOptions.Region) };
        _sqsClient = new AmazonSQSClient(config);
    }

    public async Task SendMessageAsync(string message)
    {
        var maxRetries = _awsOptions.SQS.Retries;

        for (var retry = 0; retry <= maxRetries; retry++)
        {
            try
            {
                var request = new SendMessageRequest
                {
                    QueueUrl = _awsOptions.SQS.QueueUrl,
                    MessageBody = message
                };

                var response = await _sqsClient.SendMessageAsync(request);
                _logger.LogInformation("Message sent successfully. MessageId: {id}", response.MessageId);
            }
            catch (AmazonSQSException)
            {
                _logger.LogWarning("Trying to send the message for the {retry} time", retry);
                if (retry == maxRetries) throw;
            }
        }
    }

    public async Task<IEnumerable<Message>> ReceiveMessagesAsync()
    {
        var response = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
        {
            QueueUrl = _awsOptions.SQS.QueueUrl,
            MaxNumberOfMessages = 5,
            WaitTimeSeconds = 10
        });

        return response.Messages;
    }

    public async Task DeleteMessageAsync(string receiptHandle)
    {
        await _sqsClient.DeleteMessageAsync(_awsOptions.SQS.QueueUrl, receiptHandle);
    }
}