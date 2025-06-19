/*
 17.4 Missing Number.
*/

using System.Collections;

int FindMissingInt(List<int> nums)
{
    return FindMissing(nums.Select(num => new BitArray(new [] { num })), 0);
}

int FindMissing(IEnumerable<BitArray> nums, int column = 0)
{
    Console.WriteLine("Column ** {0}", column);
    // End of int value, we have calculated what is missing...
    if (column >= int.MaxValue.ToString().Length) return -1;
    var onesList = new List<BitArray>();
    var zerosList = new List<BitArray>();

    foreach (var num in nums)
    {
        bool bit = num.Get(column);
        if (!bit)
        {
            zerosList.Add(num);
            continue;
        }
        onesList.Add(num);
    }

    Console.WriteLine("Zeros List {0}", zerosList.Print());
    Console.WriteLine("Ones List {0}", onesList.Print());

    if (zerosList.Count() <= onesList.Count())
    {
        int value = FindMissing(zerosList, column + 1);
        Console.WriteLine("Found Missing in Zeros {0}", value);
        return (value << 1) | 0;
    }
    int val = FindMissing(onesList, column + 1);
    Console.WriteLine("Found Missing in Ones {0}", val);
    return (val << 1) | 1;
}

Console.WriteLine("Found missing: {0}", FindMissingInt(new List<int> { 1, 2, 3, 4, 5, 7, 8, 9, 10 }));

public static class ArrayExtensions
{
   public static string Print(this List<BitArray> arr)
    {
        return String.Join(", ", arr.Select(v => v.ToInt()));
    }

    public static int ToInt(this BitArray binary)
    {
        var result = new int[1];
        binary.CopyTo(result, 0);
        return result[0];
    }
}
