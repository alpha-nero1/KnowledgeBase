/*
    16.6. Smallest Difference
    Given two arrays of integers, compute the pair of values (one value in each
    array) with the smallest (non-negative) difference. Return the difference.
*/

using System;

namespace question_16._6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Consider the bellow arrays,
            // with cursors a and b starting at 0 we begin.
            // we first compare the difference between 1 and 4, this is less than in max value
            // so it is the new smallest.
            // now the value at cursor a was smaller so moving it forward would only bring the value potentially closer.
            // now a = 2 and b = 4
            // the difference is now only 2, we record that.
            // now 2 is still smaller so we move the a cursor yet again.
            // now a = 11 and b = 4, okay nothing exciting, the cursor for b is now moved.
            // now a = 11 and b = 12, diff is 1, money shot!
            // we now find that we increment the cursors arbitrarily and a new min is not found
            // so the result of 1 2 is returned...
            int smallest = FindSmallestDifference(
                new int[] { 1, 2, 11, 15 },
                new int[] { 4, 12, 19, 23, 127, 235 }
            );
            Console.WriteLine("Found smallest {0}", smallest);
        }

        static int FindSmallestDifference(int[] arrOne, int[] arrTwo)
        {
            // Start with sorted arrays...
            Array.Sort(arrOne);
            Array.Sort(arrTwo);

            int a = 0;
            int b = 0;

            // Start with the highest value because we will continually shrink it down as we go along...
            int min = int.MaxValue;
            while (a < arrOne.Length && b < arrTwo.Length)
            {
                if (Math.Abs(arrOne[a] - arrTwo[b]) < min)
                {
                    min = Math.Abs(arrOne[a] - arrTwo[b]);
                }

                // Increment the cursors...
                if (arrOne[a] < arrTwo[b]) a += 1;
                else b += 1;
            }
            return min;
        }
    }
}
