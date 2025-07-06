namespace api.options;

public sealed record KafkaOptions
{
    public string BootstrapServers { get; init; } = string.Empty;
    public string Topic { get; init; } = string.Empty;
}