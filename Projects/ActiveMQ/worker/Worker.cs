using System.Text.Json;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace worker;

public class Worker : BackgroundService
{
    private const string ActiveMQUrl = "activemq:tcp://localhost:61616";
    private readonly IMessageHandler _messageHandler;
    private readonly ILogger<Worker> _logger;

    private IConnection? _connection;
    private ISession? _session;
    private IMessageConsumer? _consumer;

    public Worker(IMessageHandler messageHandler, ILogger<Worker> logger)
    {
        _messageHandler = messageHandler;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory(ActiveMQUrl);
        _connection = factory.CreateConnection();
        _connection.ClientId = "demo-subscriber";
        _connection.Start();

        _session = _connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
        var destination = _session.GetTopic("demo.topic");

        _consumer = _session.CreateDurableConsumer(
            destination,
            "demo-subscription",
            null,
            false
        );

        // += adds to the list of delegates.
        _consumer.Listener += message =>
        {
            try
            {
                var text = (message as ITextMessage)?.Text ?? "";
                _messageHandler.HandleMessage(JsonSerializer.Deserialize<DataMessage>(text)!);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Message handling failed");
            }
        };

        _logger.LogInformation("Subscribed to topic: demo.topic");

        // Complete task and let listener run in background
        return Task.CompletedTask;
    }

    /// <summary>
    /// StopAsync and Dispose to handle graceful SHUTDOWN
    /// </summary>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Shutting down ActiveMQ connection");

        _consumer?.Close();
        _session?.Close();
        _connection?.Close();

        return base.StopAsync(cancellationToken);
    }
    
    public override void Dispose()
    {
        _consumer?.Dispose();
        _session?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}