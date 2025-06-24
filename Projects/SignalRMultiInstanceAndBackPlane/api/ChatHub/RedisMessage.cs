namespace Api.ChatHub;

public class RedisMessage
{
    public string Recipient { get; set; } = "";
    public string Sender { get; set; } = "System";
    public string Message { get; set; } = "";
    public DateTimeOffset? Timestamp { get; set; }
}