namespace question_17._26;

public class DocPair
{
    public int DocFirst { get; private set; }
    public int DocSecond { get; private set; }
    public double Similarity { get; private set; }

    public DocPair(int docFirst, int docSecond, double similarity)
    {
        DocFirst = docFirst;
        DocSecond = docSecond;
        Similarity = similarity;
    }

    /// <summary>
    /// Overriding the Equals function does more than just assist in the ==
    /// operation, it also can determine uniqueness in dictionaries!
    /// </summary>
    public bool Equals(DocPair o)
    {
        if (o is not DocPair) return false;

        DocPair other = (DocPair) o;
        return (
            other.DocFirst == DocFirst
            && other.DocSecond == DocSecond
        );
    }

    public int Hash()
    {
        return (DocFirst * 31) ^ DocSecond;
    }

    public override string ToString()
    {
        return $"DocFirst = {DocFirst}, DocSecond = {DocSecond}, Similarity = {Similarity}";
    }
}