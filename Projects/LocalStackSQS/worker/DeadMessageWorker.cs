using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using common;
using common.options;
using Microsoft.Extensions.Options;

namespace worker;

public class DeadMessageWorker : BackgroundService
{
    private readonly ILogger<DeadMessageWorker> _logger;
    private readonly IAmazonSQS _sqs;
    private readonly string _queueUrl;

    public DeadMessageWorker(
        IAmazonSQS sqs,
        ILogger<DeadMessageWorker> logger,
        IOptions<AwsOptions> options
    )
    {
        _sqs = sqs;
        _logger = logger;
        _queueUrl = options.Value.SQS.DeadQueueUrl;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Dead Message Worker Running");

        while (!stoppingToken.IsCancellationRequested)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 5,
                WaitTimeSeconds = 10
            };

            var response = await _sqs.ReceiveMessageAsync(request, stoppingToken);

            foreach (var message in response.Messages)
            {
                try
                {
                    _logger.LogInformation($"Received message: {message.Body}");
                    await ProcessMessage(JsonSerializer.Deserialize<BasicMessage>(message.Body)!);

                    // Delete after processing
                    await _sqs.DeleteMessageAsync(_queueUrl, message.ReceiptHandle, stoppingToken);
                }
                catch (Exception e)
                {
                    // Just log, nothing to do now, because DeleteMessageAsync was never called, it will get
                    // picked up again later.
                    _logger.LogError(e, "Received message: {body} encountered an issue", message.Body);
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task ProcessMessage(BasicMessage message)
    {
        _logger.LogInformation("Processing message {message}", message);
        await Task.Delay(1000);
        _logger.LogInformation("Processing complete {message}", message);
    }
}
