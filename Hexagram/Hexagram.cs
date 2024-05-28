using Newtonsoft.Json;
using System.Diagnostics;

namespace HexagramNS;

/// <summary>
/// Represents a Hexagram, which is a 6x3 grid of coins.
/// </summary>
public class Hexagram
{
    // Constants for the number of rows and columns in the Hexagram
    public const int RowCount = 6;
    public const int ColCount = 3;

    private Values _values { get; set; }
    private int[] lastTrigrams = new int[] { 0, 0, 0, 0 };

    public Hexagram(Values values)
    {
        _values = values;
    }

    // TODO add int Hexagram number and changed hexagram number
    // Variable to represent the hexagram number
    public int Main
    {
        get
        {
            GetTrigrams();
            return Hexagram.hexagramLookup[lastTrigrams[0]][lastTrigrams[1]];
        }
    }

    // Variable to represent the changed hexagram number
    public int Changed
    {
        get
        {
            GetTrigrams();
            return Hexagram.hexagramLookup[lastTrigrams[2]][lastTrigrams[3]];
        }
    }
    public List<int> ChangingLines { get; } = [];

    /*

|   |111 | 001| 010| 100| 0  | 110|101 | 011|
|111| 1  | 34 | 5  | 26 | 11 | 9  | 14 | 43 |
|001| 25 | 51 | 3  | 27 | 24 | 42 | 21 | 17 |
|010| 6  | 40 | 29 | 4  | 7  | 59 | 64 | 47 |
|100| 33 | 62 | 39 | 52 | 15 | 53 | 56 | 31 |
|000| 12 | 16 | 8  | 23 | 2  | 20 | 35 | 45 |
|110| 44 | 32 | 48 | 18 | 46 | 57 | 50 | 28 |
|101| 13 | 55 | 63 | 22 | 36 | 37 | 30 | 49 |
|011| 10 | 54 | 60 | 41 | 19 | 61 | 38 | 58 |

    111 000 = 11
    */
    private static readonly Dictionary<int, Dictionary<int, int>> hexagramLookup = new()
{
{ 111, new Dictionary<int, int> { { 111,  1 }, { 1, 34 }, { 10, 5  }, { 100, 26 }, { 0, 11 }, { 110, 9  }, { 11, 43 }, { 101, 14 } } },
{ 001, new Dictionary<int, int> { { 111, 25 }, { 1, 51 }, { 10, 3  }, { 100, 27 }, { 0, 24 }, { 110, 42 }, { 11, 17 }, { 101, 21 } } },
{ 010, new Dictionary<int, int> { { 111,  6 }, { 1, 40 }, { 10, 29 }, { 100, 4  }, { 0, 7  }, { 110, 59 }, { 11, 47 }, { 101, 64 } } },
{ 100, new Dictionary<int, int> { { 111, 33 }, { 1, 62 }, { 10, 39 }, { 100, 52 }, { 0, 15 }, { 110, 53 }, { 11, 31 }, { 101, 56 } } },
{ 000, new Dictionary<int, int> { { 111, 12 }, { 1, 16 }, { 10, 8  }, { 100, 23 }, { 0, 2  }, { 110, 20 }, { 11, 45 }, { 101, 35 } } },
{ 110, new Dictionary<int, int> { { 111, 44 }, { 1, 32 }, { 10, 48 }, { 100, 18 }, { 0, 46 }, { 110, 57 }, { 11, 28 }, { 101, 50 } } },
{ 101, new Dictionary<int, int> { { 111, 13 }, { 1, 55 }, { 10, 63 }, { 100, 22 }, { 0, 36 }, { 110, 37 }, { 11, 49 }, { 101, 30 } } },
{ 011, new Dictionary<int, int> { { 111, 10 }, { 1, 54 }, { 10, 60 }, { 100, 41 }, { 0, 19 }, { 110, 61 }, { 11, 58 }, { 101, 38 } } }
};

    private void GetTrigrams()
    {
        if (!_values.Changed) { return; }

        var rowStr = string.Empty;
        ChangingLines.Clear();

        string[] trigrams = new string[2];
        string[] changedTrigrams = new string[2];

        //for (int row = RowCount - 1; row >= 0; row--)
        for (int row = 0; row < RowCount; row++)
        {
            
            UpdateTrigrams(row, trigrams, changedTrigrams);
        }

        lastTrigrams = new int[] { int.Parse(trigrams[0]), int.Parse(trigrams[1]), int.Parse(changedTrigrams[0]), int.Parse(changedTrigrams[1]) };
    }

    void UpdateTrigrams(int row, string[] trigrams, string[] changedTrigrams)
    {
        var line = GetLine(row);
        var trigramIndex = row > 2 ? 0 : 1;
        switch (line)
        {
            case 6:
                trigrams[trigramIndex] += "0";
                changedTrigrams[trigramIndex] += "1";
                ChangingLines.Add(RowCount - row);
                break;
            case 7:
                changedTrigrams[trigramIndex] += "0";
                trigrams[trigramIndex] += "0";
                break;
            case 8:
                changedTrigrams[trigramIndex] += "1";
                trigrams[trigramIndex] += "1";
                break;
            case 9:
                changedTrigrams[trigramIndex] += "0";
                trigrams[trigramIndex] += "1";
                ChangingLines.Add(RowCount - row);
                break;
        }
    }

    int GetLine(int row)
    {
        var line = 0;
        for (int col = 0; col < ColCount; col++)
        {
            var coinValue = _values.GetValue(row, col);
            if (coinValue)
            {
                line += 3;
            }
            else
            {
                line += 2;
            }
        }
        return line;
    }
}

public class QAList : List<QA>
{
    public QAList(IEnumerable<QA> collection) : base(collection)
    {
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static List<Hexagram>? FromJson(string json)
    {
        return JsonConvert.DeserializeObject<List<Hexagram>>(json);
    }
}

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class QA
{
    public DateTime Datum;
    public string Question;
    public string Answer;
    public Hexagram hexagram;

    private string GetDebuggerDisplay() => ToString();
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

}
