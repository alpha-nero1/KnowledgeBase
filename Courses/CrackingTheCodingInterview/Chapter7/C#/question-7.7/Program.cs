using System;
using System.Threading.Tasks;

// For sake of simplicity, this solution does not include 'chats' rather just one
// singles forum where messages are pushed.
namespace question_7._7
{
    class Program
    {
        static void Main(string[] args)
        {
            var backend = new FakeBackend();
            var userOne = backend.AuthService.CreateUser("ale1", "password1");
            var userTwo = backend.AuthService.CreateUser("ale2", "password2");
            SimulateChat(backend, userOne, userTwo).GetAwaiter().GetResult();
        }

        private static async Task SimulateChat(
            FakeBackend backend,
            User userOne,
            User userTwo
        )
        {
            Console.WriteLine($"Begining chat between {userOne.Username} and {userTwo.Username}");
            var clientOne = new Client
            {
                LoggedInUser = userOne,
                Backend = backend
            };
            var clientTwo = new Client
            {
                LoggedInUser = userTwo,
                Backend = backend
            };
            backend.ChatService.SetConnectedClient(clientOne);
            backend.ChatService.SetConnectedClient(clientTwo);
            // User one sends message to user two.
            await Task.Delay(1000);
            clientOne.SendMessage("Hey there man!", "ale2");
            await Task.Delay(1000);
            clientTwo.SendMessage("Hey!!", "ale1");
        }
    }
}
