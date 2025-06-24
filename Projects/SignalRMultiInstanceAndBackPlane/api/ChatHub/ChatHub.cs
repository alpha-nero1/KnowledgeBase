using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.ChatHub;

public interface IChatHub
{
    Task ReceiveMessage(string recipient, string message);
}


[Authorize]
public class ChatHub : Hub<IChatHub>
{
    private readonly ILogger<ChatHub> _logger;
    private readonly IUserConnectionStore _userConnectionStore;
    private readonly IChatDispatcher _chatDispatcher;

    public ChatHub(
        ILogger<ChatHub> logger,
        IUserConnectionStore userConnectionStore,
        IChatDispatcher chatDispatcher
    )
    {
        _logger = logger;
        _userConnectionStore = userConnectionStore;
        _chatDispatcher = chatDispatcher;
    }

    public override Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext()?.Request.Query["user"];
        if (string.IsNullOrWhiteSpace(username))
        {
            return base.OnConnectedAsync();
        }

        _userConnectionStore.Add(username!, Context.ConnectionId);
        _logger.LogInformation("User {user} connected!", username!);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? ex)
    {
        _userConnectionStore.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(ex);
    }

    public async Task SendMessage(string recipient, string message)
    {
        var userName = Context.User?.Identity?.Name;
        if (userName == null)
        {
            throw new HubException("Unauthorized user");
        }
        _logger.LogInformation("{Sender} -> {Recipient}: {Message}", userName, recipient, message);
        await _chatDispatcher.SendAsync(recipient, userName, message);
    }

    /// <summary>
    /// Invoked when a message is sent to any user on the hub.
    /// </summary>
    public Task BroadcastMessage(string user, string message)
        => _chatDispatcher.BroadcastAsync(user, message);
}