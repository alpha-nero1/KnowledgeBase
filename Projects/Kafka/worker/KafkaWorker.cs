using Confluent.Kafka;
using Microsoft.Extensions.Options;
using worker.options;

namespace worker;

public sealed class KafkaWorker : BackgroundService
{
    private readonly ILogger<KafkaWorker> _logger;
    private readonly KafkaOptions _options;
    private IConsumer<Ignore, string>? _consumer;

    public KafkaWorker(
        IOptions<KafkaOptions> opts,
        ILogger<KafkaWorker> logger
    )
    {
        _options = opts.Value;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cfg = new ConsumerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            GroupId          = _options.GroupId,
            AutoOffsetReset  = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Ignore, string>(cfg).Build();
        _consumer.Subscribe(_options.Topic);

        _logger.LogInformation("Kafka consumer started on {Topic}", _options.Topic);

        return Task.Run(() =>
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    // Keep calling consume!
                    var cr = _consumer.Consume(stoppingToken);
                    _logger.LogInformation("📨 {Value}", cr.Message.Value);
                }
            }
            catch (OperationCanceledException) { /* normal shutdown */ }
            finally
            {
                _consumer?.Close();   // commit offsets and leave group
                _consumer?.Dispose();
            }
        }, stoppingToken);
    }
}
