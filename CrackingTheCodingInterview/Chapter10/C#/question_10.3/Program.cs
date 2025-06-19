using System;

namespace question_10._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{RotationSearch(new int[] { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 }, 0, 11, 5)}");
        }

        // Searches for x in a rotated array.
        // It is similar to a binary search. BUT
        // it tries it's best to choose which side to look in as
        // it can't know where x would be given the rotation. 
        public static int RotationSearch(int[] arr, int left, int right, int x)
        {
            int mid = (left + right) / 2;
            if (x == arr[mid]) return mid; // Found element!.
            if (right < left) return -1;

            // Either the left or the right half must be normally ordered.
            // We need to first find out which side. then use the normally ordered
            // half to figure out which side to search for x.
            if (arr[left] < arr[mid])
            {
                Console.WriteLine($"left {arr[left]} is smaller than mid {arr[mid]}");
                // Left is normally ordered.
                if (x >= arr[left] && x < arr[mid]) return RotationSearch(arr, left, mid - 1, x); // Search left.
                return RotationSearch(arr, mid + 1, right, x); // Search right.
            }
            else if (arr[mid] < arr[left])
            {
                Console.WriteLine($"left {arr[left]} is larger than mid {arr[mid]}");
                // Right is normally ordered.
                if (x > arr[mid] && x <= arr[right]) return RotationSearch(arr, mid + 1, right, x); // Search right.
                return RotationSearch(arr, left, mid - 1, x); // Search left.
            }
            else if (arr[left] == arr[mid])
            {
                // Left or right hald is all repeats...
                // If right is different then search it.
                if (arr[mid] != arr[right]) return RotationSearch(arr, mid + 1, right, x);
                int searchLeftRes = RotationSearch(arr, left, mid - 1, x);
                // If we found nothing in the left of repeats it must be in the right.
                if (searchLeftRes == -1) return RotationSearch(arr, mid + 1, right, x);
                return searchLeftRes;
            }
            return -1;
        }
    }
}
