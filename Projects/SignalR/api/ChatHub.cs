using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private static readonly Dictionary<string, string> _userConnections = new();

    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext()?.Request.Query["user"];
        if (string.IsNullOrWhiteSpace(username))
        {
            return base.OnConnectedAsync();
        }

        _userConnections[username!] = Context.ConnectionId;
        _logger.LogInformation("User {user} connected!", username!);
        return base.OnConnectedAsync();
    }

    public async Task SendMessage(string recipient, string message)
    {
        var userName = Context.User?.Identity?.Name;
        if (userName == null)
        {
            throw new HubException("Unauthorized user");
        }
        _logger.LogInformation("{Sender} -> {Recipient}: {Message}", userName, recipient, message);
        if (_userConnections.TryGetValue(recipient, out var recipientConnectionId))
        {
            await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", userName, message);
        }
    }

    /// <summary>
    /// Invoked when a message is sent to any user on the hub.
    /// </summary>
    public async Task BroadcastMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}