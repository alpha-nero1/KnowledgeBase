using System;

namespace question_10._11
{
    class Program
    {
        static void Main(string[] args)
        {
            SortValleyPeak(new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 });
        }

        public static async void SortValleyPeak(int[] arr)
        {
            for (int i = 1; i < arr.Length; i += 2)
            {
                // Iterate from one and skip an index on each it.
                int biggestIndex = Program.MaxIndex(arr, i);
                if (i != biggestIndex)
                {
                    int temp = arr[i];
                    arr[i] = arr[biggestIndex];
                    arr[biggestIndex] = temp;
                }
            }
            foreach (var i in arr)
            {
                Console.WriteLine("Element: {0}", i);
            }
        }

        public static int MaxIndex(int[] arr, int i)
        {
            int before = i - 1;
            int after = i + 1;
            int len = arr.Length;
            int beforeValue = before >= 0 && before < arr.Length ? arr[before] : int.MinValue;
            int iValue = i >= 0 && i < arr.Length ? arr[i] : int.MinValue;
            int afterValue = after >= 0 && after < arr.Length ? arr[after] : int.MinValue;

            // Compare the before value with this and the one after.
            // Solution:
            int max = Math.Max(beforeValue, Math.Max(iValue, afterValue));
            if (max == beforeValue) return before;
            if (max == iValue) return i;
            return after;
        }
    }
}
