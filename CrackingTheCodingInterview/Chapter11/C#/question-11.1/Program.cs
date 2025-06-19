using System;

namespace question_11._1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                Find the mistake in 
                unsigned int i; 
                for (i = 100; i >= 0; --i) 
                printf("%d\n", i);
            */

            uint i; // An unsigned int is ALWAYS >= 0 (because unsigned means no negative flag attached.)
            // i > 0 applies the fix
            for (i = 100; i > 0; --i)
            {
                // Also %d for decimal formatting is incorrect.
                Console.WriteLine($"{i}");
            }
        }
    }
}
