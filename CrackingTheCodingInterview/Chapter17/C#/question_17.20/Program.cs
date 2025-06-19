/*
    17.20. Contiguous Median.
*/

var lst = new List<int>();

void AddNewNumber(int randomNum)
{
    if (lst.Count == 0)
    {
        lst.Add(randomNum);
        return;
    }

    var newIndex = lst.Count - 1;

    // Insert the new random number in the correct pos inside the list.
    for (int i = 0; i < lst.Count; i++)
    {
        var ths = lst[i];
        int? nxt = i < lst.Count - 1 ? lst[i + 1] : null;
        if (nxt == null)
        {
            break;
        }
        if (randomNum > ths && randomNum < nxt)
        {
            newIndex = i;
            break;
        }
    }

    lst.Insert(newIndex + 1, randomNum);
}

double GetMedian()
{
    if (lst.Count == 0) return 0;
    if (lst.Count % 2 == 1) return lst[lst.Count / 2];
    int middleLower = lst.Count / 2 - 1;
    int middleUpper = lst.Count / 2;
    return (lst[middleLower] + lst[middleUpper]) / 2;
}

AddNewNumber(0);
AddNewNumber(1);
AddNewNumber(5);
AddNewNumber(3);
AddNewNumber(2);
AddNewNumber(10);
AddNewNumber(15);
AddNewNumber(20);

Console.WriteLine("Median is {0}", GetMedian());