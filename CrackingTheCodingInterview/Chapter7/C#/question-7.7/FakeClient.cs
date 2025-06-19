using System;

namespace question_7._7
{
    public interface IClient
    {
        User LoggedInUser { get; set; }
        FakeBackend Backend { get; set; }
        void AcceptMessage(Message message);
        void SendMessage(string message, string to);
    }

    public class Client : IClient
    {
        public User LoggedInUser { get; set; }
        public FakeBackend Backend { get; set; }

        public void AcceptMessage(Message message)
        {
            Console.WriteLine($"Client {LoggedInUser.Username} received message: {message.Text}");
        }

        public void SendMessage(string message, string to)
        {
            Backend.ChatService.PushMessage(LoggedInUser.Username, to, message);
        }

    }
}