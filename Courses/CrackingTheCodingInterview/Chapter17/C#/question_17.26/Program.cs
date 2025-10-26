/*
    17.26. Sparse similarity. The similarity of two documents (each with distinct words) is defined
    to be the size of the intersection divided by the size of the union. For example, if the documents
    consist of integers [1, 5, 3] and [1, 7, 2, 3], the similarity is 0.4 because the intersection has
    size 2 and the union has size 5.
*/

using question_17._26;

var documents = new List<Document>
{
    new Document(1, new List<int> { 1, 2, 3, 4 }),
    new Document(1, new List<int> { 8, 2, 9, 8 }),
    new Document(1, new List<int> { 3, 6, 7, 4 }),
    new Document(1, new List<int> { 1, 2, 8, 0 }),
};

var similar = GetSimilarDocuments(documents);

foreach (var docPair in similar)
{
    Console.WriteLine(docPair);
}

#region Brute Force

List<DocPair> GetSimilarDocuments(List<Document> documents)
{
    var pairMap = new HashSet<DocPair>();

    for (int i = 0; i < documents.Count - 1; i++)
    {
        for (int j = i + 1; j < documents.Count; j++)
        {
            var docFirst = documents[i];
            var docSecond = documents[j];
            double similarity = ComputeSimilarity(docFirst, docSecond);
            if (similarity > 0)
            {
                pairMap.Add(new DocPair(docFirst.GetId(), docSecond.GetId(), similarity));
            }
        }
    }

    return pairMap.ToList();
}

double ComputeSimilarity(Document docOne, Document docTwo)
{
    int commonElements = 0;
    HashSet<int> setOne = new HashSet<int>(docOne.GetWords());

    foreach (var word in docTwo.GetWords())
    {
        // Up the counter for the intersection.
        if (setOne.Contains(word)) commonElements += 1;
    }

    // Get count of all uncommon elements
    int totalWords = docOne.GetSize() + docTwo.GetSize();
    double uncommonElements = totalWords - commonElements;

    //
    return commonElements / uncommonElements;
}

#endregion;
