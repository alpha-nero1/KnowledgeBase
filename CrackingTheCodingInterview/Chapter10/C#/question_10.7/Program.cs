using System;

namespace question_10._7
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                4 billion ints is 2 to the power of 32 distinct integers. all positive ints are 2 to the power
                of 31, so there will be some dups to fit the extra power.

                We have 1GB of memory, which is 8 billion bits. with 8 billion bits, we can map each integer in the
                4 billion to a bit in the 1GB!

                The logic is executed like this:
                1. Create a bit vector with 4 billion bits.
                2. Initialise the BV with 0s
                3. Scan all numbers from the file and call BV.set(num, 1)
                4. Scan again the BV and get the index that first returns 0
                5. Return that index!
            */
           Program.MissingInt();
        }

        public static async void MissingInt()
        {
            long numberOfInts = 64; // Basic example.
            long bitsInAByte = 8;
            byte[] bitVector = new byte[(int) (numberOfInts / bitsInAByte)];

            // Dummy the read in ints.
            int[] readInInts = new int[5];

            // First scan.
            foreach (int i in readInInts)
            {
                /*
                    Let's unpack what is going on here, it's actually quite smart:
                    (i / bitsInAbyte) is finding the correct byte to store info in, because
                    this is almost like a 2d array.

                    |= means apply an OR to the exact byte in byte[]

                    (1 << i % 8) is getting an int (value of 1) and shifting the 1 (bit to set)
                    a certain amount of times to the left (modulus) to set the right bit
                */

                // E.g
                // i = 1 = bitVector[0] |= (1 << (1))
                // i = 2 = bitVector[0] |= (1 << (2))
                // i = 3 = bitVector[0] |= (1 << (3))
                // i = 8 = bitVector[0] |= (1 << (0))
                // ...
                // i = 9 = bitVector[1] |= (1 << (1))

                bitVector[(i / bitsInAByte)] |= (1 << (i % 8));
            }

            // Second scan.
            foreach (int i in bitVector)
            {
                for (int j = 0; j < bitsInAByte; j++)
                {
                    if (i & (1 << j))
                    {
                        // Found it!
                        Console.WriteLine("Found new int! {int}", (i * 8 + j));
                    }

                }
            }

            /*
                In terms of finding a new int with only 10MB of storage and assuming that each
                int is unique we can do the following:
                1. To fit the 10MB we can divide the total ints into chunks of 1000
                2. The first pass checks if i is part of the chunk, if so |= 1
                3. rememeber if 1 was flagged or not
                4. in the second pass you can loop the chunk to see which i was missing.
            */

            // Final note, to access the index of a bit in a byte, you need to
            // use << int shifting.
        }
    }
}
