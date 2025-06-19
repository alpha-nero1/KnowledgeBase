/*
    17.14. Smallest K.
    Note that "rank" in this context means "smallest values in range of".
    This solution uses a selection rank algorithm.
*/

using System;

int[] SmallestK(int[] arr, int k)
{
    Console.WriteLine("##### START #####");
    Console.WriteLine("Array {0}", String.Join(", ", arr));
    if (k <= 0 || k > arr.Length) throw new ArgumentOutOfRangeException();

    // Get the value that values must be less than.
    int threshold = GetRank(arr, k - 1);

    Console.WriteLine("\n##### EVALUATION #####");
    Console.WriteLine("Threshold = {0}", threshold);
    int[] smallest = new int[k];
    int count = 0;
    foreach (var a in arr)
    {
        if (a <= threshold)
        {
            Console.WriteLine("Count & A {0}, {1}", count, a);
            smallest[count] = a;
            count += 1;
        }
    }
    return smallest;
}

/// <summary>
/// Recursive entry function.
/// </summary>
int GetRank(int[] arr, int rank)
{
    return Rank(arr, 0, arr.Length - 1, rank);
}

/// <summary>
/// Get element with rank between left and right indices.
/// </summary>
int Rank(int[] arr, int leftIndex, int rightIndex, int rank)
{
    Console.WriteLine("##### IT START #####");

    // Get a random index in the range.
    int randomIndex = GetRandomIntInRange(leftIndex, rightIndex);
    Console.WriteLine("Left = {0}, Right = {1}, Rank = {2}", leftIndex, rightIndex, rank);
    Console.WriteLine("Random Index = {0}", randomIndex);

    // The pivot is the value that represents the random index.
    int pivot = arr[randomIndex];
    Console.WriteLine("Pivot = {0}", pivot);

    // Get the left side...
    int leftEnd = Partition(arr, leftIndex, rightIndex, pivot);
    Console.WriteLine("Left End = {0}", leftEnd);
    int leftSize = leftEnd - leftIndex + 1;
    Console.WriteLine("Left Size = {0}\n", leftSize);

    // If the size of the left == rank then return the largest element
    if ((leftSize - 1) == rank) return Max(arr, leftIndex, leftEnd);
    // If size of the left partition is still greater than the rank then run rank again only using the left partition.
    if (rank < leftSize) return Rank(arr, leftIndex, leftEnd, rank);
    // Else if the left partition is smaller than rank, we must look in the right side for our answer.
    return Rank(arr, leftEnd + 1, rightIndex, rank - leftSize);
}

/// <summary>
/// Partition array around pivot such that all elements less than pivot come before all
/// elements after pivot.
/// E.g. [1, 2, 3, 4, 5, 6, (pivot = 7), 8, 9, 10]
/// This algorithm is similar to your cross linear search however it does a swap of sides of the pivot
/// </summary>
int Partition(int[] arr, int leftIndex, int rightIndex, int pivot)
{
    while (leftIndex <= rightIndex)
    {
        // If the left value is greater than the pivot, swap the left and right
        // and reduce the right index.
        if (arr[leftIndex] > pivot)
        {
            Swap(arr, leftIndex, rightIndex);
            rightIndex -= 1;
            continue;
        }
        // Else if opposite increase left index and swap to index on the other side.
        if (arr[rightIndex] <= pivot)
        {
            Swap(arr, leftIndex, rightIndex);
            leftIndex += 1;
            continue;
        }
        leftIndex += 1;
        rightIndex -= 1;
    }

    return leftIndex - 1;
}

/// <summary>
/// Get a ranom integer between min and max.
/// </summary>
int GetRandomIntInRange(int min, int max)
{
    Random rand = new Random();
    return rand.Next(max + 1 - min) + min;
}

/// <summary>
/// Swap indices i and j.
/// </summary>
void Swap(int[] arr, int i, int j)
{
    int temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
}

/// <summary>
/// Find what is the largest element in the range!
/// </summary>
int Max(int[] arr, int left, int right)
{
    Console.WriteLine("Left & Right = {0}, {1}", left, right);
    int max = int.MinValue;
    for (int i = left; i <= right; i++)
    {
        max = Math.Max(arr[i], max);
    }
    return max;
}

var smallest = SmallestK(new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 3);

foreach (var num in smallest)
{
    Console.WriteLine("Smallest {0}", num);
}