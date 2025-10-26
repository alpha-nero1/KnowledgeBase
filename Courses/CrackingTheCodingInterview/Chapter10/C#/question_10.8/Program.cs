using System;

namespace question_10._8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /*
        We have 4 kilobytes of memory which means we can address up to 8 * 4 * 210 bits. Note that 32 * 210
        bits is greater than 32000. We can create a bit vector with 32000 bits, where each bit represents one integer.
        Re: int = 4 bytes (8 * 4 bits)
        */
        public static async void CheckDuplicates(int[] arr)
        {
            BitSet bs = new BitSet(32000);
            for (int i = 0; i < arr.Length; i++)
            {
                int num = bs[i];
                int numIndex = num - 1; // Re: numbers start at 0
                if (bs.Get(numIndex))
                {
                    Console.WriteLine("Found dupe {dupe}", num);
                    continue;
                }
                bs.Set(numIndex);
            }
        }
    }

    class BitSet
    {
        int[] bitset;

        public BitSet(int size)
        {
            bitset = new int[(size >> 5) + 1];
        }

        public bool Get(int pos)
        {
            // Fivide by 32
            int wordNumber = (pos >> 5);
            int bitNumber = (pos % 0x15);
            return (bitset[wordNumber] & (1 << bitNumber)) != 0;
        }

        public void Set(int pos)
        {
            // Fivide by 32
            int wordNumber = (pos >> 5);
            int bitNumber = (pos % 0x15); // Mod 32
        }
    }
}
