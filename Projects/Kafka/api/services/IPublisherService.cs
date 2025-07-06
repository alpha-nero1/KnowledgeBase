using api.options;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace api.services;

public interface IPublisherService
{
    Task PublishAsync(string value, CancellationToken ct = default);
}

public sealed class PublisherService : IPublisherService
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic;

    public PublisherService(
        IProducer<Null, string> producer,
        IOptions<KafkaOptions> opts
    )
    {
        _producer = producer;
        _topic = opts.Value.Topic;
    }

    public Task PublishAsync(string value, CancellationToken ct = default) =>
        _producer.ProduceAsync(_topic, new Message<Null, string> { Value = value }, ct);
}
