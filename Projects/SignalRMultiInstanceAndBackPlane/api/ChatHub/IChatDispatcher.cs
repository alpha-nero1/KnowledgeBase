using Microsoft.AspNetCore.SignalR;

namespace Api.ChatHub;

/// <summary>
/// Standard way of sending messages over SignalR, uses IUserConnectionStore to do so.
/// </summary>
public interface IChatDispatcher
{
    Task SendAsync(string recipientUser, string senderUser, string message);
    Task BroadcastAsync(string senderUser, string message);
}

public sealed class ChatDispatcher : IChatDispatcher
{
    private readonly IHubContext<ChatHub, IChatHub> _hub;
    private readonly IUserConnectionStore _store;

    public ChatDispatcher(
        IHubContext<ChatHub, IChatHub> hub,
        IUserConnectionStore store
    )
    {
        _hub = hub;
        _store = store;
    }

    public async Task SendAsync(string recipient, string sender, string message)
    {
        if (_store.TryGetConnectionId(recipient, out var connId))
            await _hub.Clients.Client(connId).ReceiveMessage(sender, message);
    }

    public Task BroadcastAsync(string sender, string message) =>
        _hub.Clients.All.ReceiveMessage(sender, message);
}