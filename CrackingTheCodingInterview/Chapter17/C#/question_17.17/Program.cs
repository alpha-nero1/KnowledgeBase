/*
    17.17 Multi-Search.
*/

using question_17._17;

var res = SearchAll("missisipi", new string[] { "is", "ppi", "hi", "sis", "i", "ssippi" });

Dictionary<string, List<int>> SearchAll(string big, string[] smalls)
{
    Dictionary<string, List<int>> lookup = new Dictionary<string, List<int>>();
    Trie trie = CreateTrieFromString(big);
    foreach (var s in smalls)
    {
        Console.WriteLine("Searching for {0}", s);
        List<int> locations = trie.Search(s);

        Console.WriteLine("Locations = {0}", locations != null ? String.Join(", ", locations) : null);
    }
    return lookup;
}


void SubtractValue(List<int> locations, int delta)
{
    if (locations == null) return;
    for (int i = 0; i < locations.Count; i++)
    {
        locations[i] = (locations[i] - delta);
    }
}


Trie CreateTrieFromString(string s)
{
    Trie trie = new Trie();
    for (int i = 0; i < s.Length; i++)
    {
        string suffx = s.Substring(1);
        trie.Root.InsertString(suffx, i);
    }
    return trie;
}
