using System;

namespace C_
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine($"Magic index is: {FindMagicIndex(new int[] { 1, 2, 3, 3, 4, 5, 7, 7, 7, 7 })}");
        }

        static int? FindMagicIndex(int[] arr)
        {
            int indexCounter = 0;
            foreach (int el in arr) {
                if (el == indexCounter)
                {
                    return el;
                }
                // Look at the new index.
                indexCounter += 1;
            }
            return null;
        }
    }
}
