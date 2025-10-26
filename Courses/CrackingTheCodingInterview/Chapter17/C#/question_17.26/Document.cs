namespace question_17._26;

public class Document
{
    private List<int> _words = new List<int>();
    private int _docId = 0;

    public Document(int id, List<int> words)
    {
        _docId = id;
        _words = words;
    }

    public List<int> GetWords() => _words;
    public int GetId() => _docId;
    public int GetSize() => _words.Count;
}