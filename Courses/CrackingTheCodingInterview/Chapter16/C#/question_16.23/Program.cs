/*
    16.23 Rand 7 from Rand 5
*/

using System;

namespace question_16._23
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(RandSeven());
        }

        public static int RandSeven()
        {
            while (true)
            {
                // While loop because it could take some goes to get it right
                // Because we must use rand five twice in order to get results means
                // there is a 1/25 chance of a sequence occuring.
                // if whjat comes out of num is less than 21 then lets return the modulus of 7.
                int num = 5 * RandFive() + RandFive();
                if (num < 21)
                {
                    return num % 7;
                }
            }
        }

        public static int RandFive()
        {
            var rand = new Random();
            return rand.Next(1, 6);
        }
    }
}
