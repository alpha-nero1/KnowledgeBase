using System;

namespace question_5._6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"getSwapRequired(12, 87) = {Program.GetSwapRequired(12, 87)}");
            Console.WriteLine($"getSwapRequired(15, 29) = {Program.GetSwapRequired(15, 29)}");
        }

        // Find how many bit swaps we would need to convert value a to value b.
        private static int GetSwapRequired(int a, int b) {
            int count = 0;
            // We can use c = c & (c - 1) to keep clearing the least significant bit
            // untill c = 0
            for (int c = a ^ b; c != 0; c &= c - 1) {
                // Example: 
                // a = 00001111 (15), b = 0001 1101
                // XOR = 00010010
                // XOR = XOR & XOR - 1 (00010010 & 00010001) // LAST BIT DESTROYED!
            //     a = 00001010 (10), 
            //     b = 00010100 (20)
            //     x = 00011110 
            // x - 1 = 00011101
            //     r = 00011100 // LAST BIT DESTROYED VALUE &= VALUE - 1
                count += 1;
            }
            return count;
        }
    }
}
