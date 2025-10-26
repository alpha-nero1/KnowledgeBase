using System.Collections.Generic;

namespace question_7._7
{
    public class User {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Message {
        public int Id { get; set; }
        public string FromUsername { get; set; }
        public string ToUsername { get; set; }
        public string Text { get; set; }
    }
}