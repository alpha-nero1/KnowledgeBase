namespace question_17._17;

public class Trie
{
    public TrieNode Root { get; private set; } = new TrieNode();

    public Trie() {}
    public Trie(string word)
    {
        Console.WriteLine("New Trie");
        Root.InsertString(word);
    }

    public List<int> Search(string word)
    {
        return Root.Search(word, 0, new List<int>());
    }

    public override string ToString()
    {
        return Root.ToString();
    }
}

public class TrieNode
{
    private Dictionary<char, TrieNode> _children = new Dictionary<char, TrieNode>();
    private List<int> _indexes = new List<int>();
    private char _char;

    public TrieNode() {}

    public TrieNode(char character)
    {
        _char = character;
    }

    public void InsertString(string remainingString, int index = 0)
    {
        Console.WriteLine("Remaining = {0}", remainingString);
        _indexes.Add(index);
        if (String.IsNullOrWhiteSpace(remainingString))
        {
            _children['\0'] = null;
            return;
        };

        // Get the next char for what is remaining.
        var nextChar = remainingString[0];
        var nextChild = new TrieNode();

        if (_children.ContainsKey(nextChar))
        {
            // If we already have the next char then
            nextChild = _children[nextChar];
        }

        _children[nextChar] = nextChild;

        var remaining = remainingString.Substring(1);
        nextChild.InsertString(remaining, index + 1);
    }

    public List<int> Search(string s, int i, List<int> indecies)
    {
        if (String.IsNullOrWhiteSpace(s)) return indecies;

        char first = s[0];

        if (_children.ContainsKey(first))
        {
            indecies.Add(i);
            return _children[first].Search(s.Substring(1), i + 1, indecies);
        }

        if (_children.Count > 0)
        {
            var frst = _children.Keys.First();
            return _children[frst].Search(s.Substring(1), i + 1, indecies);
        }

        return indecies;
    }

    public bool Terminates()
    {
        return _children.ContainsKey('\0');
    }

    public TrieNode GetChild(char c)
    {
        if (_children.ContainsKey(c)) return _children[c];
        return null;
    }

    public override string ToString()
    {
        var str = "[";
        str += "\n";

        foreach (var chr in _children)
        {
            str += $"[{chr.Key}, {chr.Value?.ToString()}],";
        }

        return str + "]";
    }
}
