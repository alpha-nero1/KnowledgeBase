using System.Collections.Concurrent;

namespace Api.ChatHub;

public interface IUserConnectionStore
{
    void Add(string user, string connectionId);
    void Remove(string connectionId);
    bool TryGetConnectionId(string user, out string connectionId);
}

public sealed class UserConnectionStore : IUserConnectionStore
{
    /// <summary>
    /// ConcurrentDictionary = THREAD SAFE DICTIONARY!
    /// https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=net-9.0
    /// </summary>
    private readonly ConcurrentDictionary<string, string> _userToConn = new();
    private readonly ConcurrentDictionary<string, string> _connToUser = new();

    public void Add(string user, string connectionId)
    {
        _userToConn[user] = connectionId;
        _connToUser[connectionId] = user;
    }

    public void Remove(string connectionId)
    {
        if (_connToUser.TryRemove(connectionId, out var user))
            _userToConn.TryRemove(user, out _);
    }

    public bool TryGetConnectionId(string user, out string connectionId)
        => _userToConn.TryGetValue(user, out connectionId);
}