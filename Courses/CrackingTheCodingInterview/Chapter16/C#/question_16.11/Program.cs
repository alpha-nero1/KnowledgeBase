/*
    16.11. Diving board.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace question_16._11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lengths = GetAllPossibleLengths(10, 5, 8);
            Console.WriteLine("Res: ");
            Console.WriteLine(String.Join(", ", lengths));
        }

        static List<int> GetAllPossibleLengths(int k, int shorter, int longer)
        {
            HashSet<int> lengths = new HashSet<int>();
            // We loop the different ways, k.
            for (int nShorter = 0; nShorter <= k; nShorter++)
            {
                // The number of longer planks is now 1 less.
                int nLonger = k - nShorter;
                // This length is the n of sorter * shorter length + number of longer * longer length.
                // This is pretty nifty!
                int length = (nShorter * shorter) + (nLonger * longer);
                lengths.Add(length);
            }
            return lengths.ToList<int>();
        }
    }
}
