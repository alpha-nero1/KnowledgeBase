/*
    17.11. Word Distance
*/

using question_17._11;

var document = @"
    hello there mr speakman how are you today speakman
    you silly boy mr. Hello speaky boy. hello mr speakman
";
var closest = FindClosest("hello", "speakman", document.Trim().Split(" "));
if (closest != null)
{
    Console.WriteLine("Closest {0}, {1}", closest.LocOne, closest.LocTwo);
}

/// <summary>
/// Find closest words.
/// </summary>
LocationPair FindClosest(string wordOne, string wordTwo, string[] words)
{
    var locations = GetLocations(words);
    if (!locations.ContainsKey(wordOne) || !locations.ContainsKey(wordTwo)) return null;

    var locationsOne = locations[wordOne];
    var locationsTwo = locations[wordTwo];

    return FindMinDistancePair(locationsOne, locationsTwo);
}


/// <summary>
/// Find min distance given value lists.
/// </summary>
LocationPair FindMinDistancePair(List<int> locationsOne, List<int> locationsTwo)
{
    Console.WriteLine("\n##### START #####");
    Console.WriteLine("LocationsOne = {0}", String.Join(", ", locationsOne));
    Console.WriteLine("LocationsTwo = {0}\n", String.Join(", ", locationsTwo));

    if (locationsOne.Count < 1 || locationsTwo.Count < 1) return null;
    var bestPair = new LocationPair(-1, -1);
    int sizeOne = locationsOne.Count;
    int sizeTwo = locationsTwo.Count;
    int i = 0, j = 0;

    var lOne = locationsOne[i];
    var lTwo = locationsTwo[j];

    while (i < sizeOne && j < sizeTwo)
    {
        Console.WriteLine("##### IT START #####");
        lOne = locationsOne[i];
        lTwo = locationsTwo[j];
        Console.WriteLine("L1, L2 = {0}, {1} \n", lOne, lTwo);
        bestPair.Set(lOne, lTwo);

        // If best l1 is less than l2
        // we want to increment the left side
        // else the right side.
        // we end up with the closest indexes.
        if (bestPair.LocOne < lTwo)
        {
            i++;
            continue;
        }
        j++;

    }

    return bestPair;
}


/// <summary>
/// Get list of locations of each word.
/// </summary>
Dictionary<string, List<int>> GetLocations(string[] words)
{
    var locations = new Dictionary<string, List<int>>();
    for (int i = 0; i < words.Length; i++)
    {
        var word = words[i];
        if (!locations.ContainsKey(word)) locations[word] = new List<int>();
        locations[word].Add(i);
    }
    return locations;
}
