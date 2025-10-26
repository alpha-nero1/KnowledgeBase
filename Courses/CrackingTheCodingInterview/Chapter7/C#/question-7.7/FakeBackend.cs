using System;
using System.Collections.Generic;
using System.Linq;

namespace question_7._7
{
    public class AuthService
    {
        private int _userIndex = 0;
        private Dictionary<string, User> _users { get; set; } = new Dictionary<string, User>();

        public User CreateUser(string username, string password)
        {
            var userExists = _users.ContainsKey(username);
            if (userExists) throw new Exception("User already exists in system.");
            _userIndex += 1;
            _users[username] = new User { 
                Id = _userIndex,
                Username = username,
                Password = password
            };
            return _users[username];
        }

        public bool Authenticate(string username, string password)
        {
            var userExists = _users.ContainsKey(username);
            if (userExists) throw new Exception("User already exists in system.");
            return _users[username].Password == password;
        }
    }

    public class ChatService
    {
        private int _messageIndex { get; set; }

        private List<Message> _messages { get; set; } = new List<Message>();
        private readonly AuthService _authService;
        private readonly Dictionary<string, IClient> _connectedClients = new Dictionary<string, IClient>();

        public ChatService(AuthService authService)
        {
            _authService = authService;
        }

        // Connect.
        public void SetConnectedClient(IClient client)
        {
            _connectedClients[client.LoggedInUser.Username] = client;
        }

        // Add the message and notify other clients.
        public void PushMessage(string fromUsername, string toUsername, string text)
        {
            Console.WriteLine($"Pushing message {text} to {toUsername}");
            var newMessage = new Message
            {
                ToUsername = toUsername,
                FromUsername = fromUsername,
                Text = text
            };
            _messages.Append(newMessage);
            NotifyClientsOfNewMessage(fromUsername, newMessage);
        }

        // Notify all connected clients of a message.
        private void NotifyClientsOfNewMessage(string fromUsername, Message message)
        {
            foreach (var x in _connectedClients.Values)
            {
                // Hey man use 'continue' in loops and not return!
                if (x.LoggedInUser.Username == fromUsername) continue;
                Console.WriteLine($"Sending message to: {x.LoggedInUser.Username} from {fromUsername}");
                x.AcceptMessage(message);
            }
        }

        // Get a list of pushed messages.
        public List<Message> GetMessages()
        {
            return _messages;
        }
    }

    public class FakeBackend
    {
        public AuthService AuthService { get; set; } = new AuthService();
        public ChatService ChatService { get; set; }

        public FakeBackend()
        {
            ChatService = new ChatService(AuthService);
        }
    }
}
