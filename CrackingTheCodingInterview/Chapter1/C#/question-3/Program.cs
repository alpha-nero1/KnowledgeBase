using System;

namespace question_3
{
    public static class StringExtensions
    {
        public static string Urlify(this string url)
        {
            return string.Join("%20", url.Split(" "));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chapter 1 - Question 3");
            Console.WriteLine($"a. = {"https://whatarepoplesayingabout.com/topics/blue herd".Urlify()}");
            Console.WriteLine($"b. = {"https://whatarepoplesayingabout.com/topics/jack hammer ack dammer".Urlify()}");
            Console.WriteLine($"b. = {"https://whatarepoplesayingabout.com/topics/smack bammer ack tammer".Urlify()}");
        }
    }
}
