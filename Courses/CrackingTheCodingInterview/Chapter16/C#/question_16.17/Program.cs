using System;

namespace question_16._17
{
    class Program
    {
        static void Main(string[] args)
        {
            var seq = ContiguosSequence(new int[] { 2, -8, 3, -2, 4, -10 });
            Console.WriteLine("ContiguosSequence result: {0}", seq);
        }

        // Returns a result of type (sum, (start, end)).
        public static Tuple<int, Tuple<int, int>> ContiguosSequence(int[] arr) {
            // Initialise variables.
            int sum = 0;
            int start = 0;
            int end = 0;

            // Loop each index spot.
            for (int i = 0; i < arr.Length; i++) {
                int isum = arr[i];
                int istart = i;

                // Inner loop to calculate highest sequence.
                for (int j = i + 1; j < arr.Length; j++) {
                    isum += arr[j];

                    // If, by chance, the running sum here is greater than the total sum recorded before,
                    // it is the best sequence, record where this happened!! ~ Ale :)
                    if (isum > sum) {
                        sum = isum;
                        start = istart;
                        end = j;
                    }
                }
            }

            return new Tuple<int, Tuple<int, int>>(sum, new Tuple<int, int>(start, end));
        }
    }
}
