﻿﻿﻿namespace HexagramNS;

/*
1. Six Lines Stacked:
A hexagram consists of six lines, stacked one on top of the other.
The lines are read from the bottom up, meaning the first line is at the bottom, and the sixth line is at the top. This order is crucial for understanding the hexagram's development and meaning.

2. Trigrams:
The six lines of a hexagram can be further divided into two trigrams (三卦 - sān guà):
Lower Trigram (內卦 - nèi guà): Composed of the bottom three lines. It is often associated with the internal situation, personal feelings, or inner world.
Upper Trigram (外卦 - wài guà): Composed of the top three lines. It is often associated with the external situation, relationships with others, or the outer world.
There are eight possible trigrams, each representing a fundamental concept or aspect of reality:

☰ Qian (乾): Heaven, creative, strong (three solid lines)
☷ Kun (坤): Earth, receptive, yielding (three broken lines)
☳ Zhen (震): Thunder, arousing, initiating (solid line, broken line, broken line)
☶ Gen (艮): Mountain, stillness, stopping (broken line, broken line, solid line)
☴ Xun (巽): Wind/Wood, gentle, penetrating (broken line, solid line, solid line)
☵ Kan (坎): Water, abyss, dangerous (solid line, broken line, solid line)
☲ Li (離): Fire, clinging, radiant (broken line, solid line, broken line)
☱ Dui (兌): Lake, joyful, open (solid line, solid line, broken line)

3. Formation of Hexagrams:
By combining any two trigrams, you create one of the 64 possible hexagrams.
Each of the 64 hexagrams has a unique name, associated meaning, and often a story or interpretation connected to its particular combination of lines and trigrams.
The positions of the solid and broken lines within the hexagram are important. They determine not just the overall meaning of the hexagram but also nuances in its interpretation.
*/

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

    // Backing field for ChangingLines
    private List<int> _changingLines = new List<int>();

    public Hexagram(Values values)
    {
        _values = values;
    }

    // TODO add int Hexagram number and changed hexagram number
    // Variable to represent the hexagram number
    public int Current
    {
        get
        {
            GetTrigrams();
            return Hexagram.hexagramLookup[lastTrigrams[0]][lastTrigrams[1]];
        }
    }

    // Variable to represent the changed hexagram number
    public int New
    {
        get
        {
            GetTrigrams();
            return Hexagram.hexagramLookup[lastTrigrams[2]][lastTrigrams[3]];
        }
    }

    public List<int> ChangingLines
    {
        get
        {
            GetTrigrams();
            return new List<int>(_changingLines);
        }
    }

    /*
This is the main documentation for Hexagram values. Never change this.
|   |111 | 0  | 010|101 | 001| 100| 110| 011|
|111| 1  | 11 | 5  | 14 | 34 | 26 | 9  | 43 |
|000| 12 | 2  | 8  | 35 | 16 | 23 | 20 | 45 |
|010| 6  | 7  | 29 | 64 | 40 | 4  | 59 | 47 |
|101| 13 | 36 | 63 | 30 | 55 | 22 | 37 | 49 |
|001| 25 | 24 | 3  | 21 | 51 | 27 | 42 | 17 |
|100| 33 | 15 | 39 | 56 | 62 | 52 | 53 | 31 |
|110| 44 | 46 | 48 | 50 | 32 | 18 | 57 | 28 |
|011| 10 | 19 | 60 | 38 | 54 | 41 | 61 | 58 |

    111 000 = 11

|   |111 | 001| 010| 100| 0  | 110|101 | 011|
|111| 1  | 34 | 5  | 26 | 11 | 9  | 14 | 43 |
|001| 25 | 51 | 3  | 27 | 24 | 42 | 21 | 17 |
|010| 6  | 40 | 29 | 4  | 7  | 59 | 64 | 47 |
|100| 33 | 62 | 39 | 52 | 15 | 53 | 56 | 31 |
|000| 12 | 16 | 8  | 23 | 2  | 20 | 35 | 45 |
|110| 44 | 32 | 48 | 18 | 46 | 57 | 50 | 28 |
|101| 13 | 55 | 63 | 22 | 36 | 37 | 30 | 49 |
|011| 10 | 54 | 60 | 41 | 19 | 61 | 38 | 58 |
*/

    // this is the representation of the above hexagram values.
    public static readonly Dictionary<int, Dictionary<int, int>> hexagramLookup = new()
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
        _changingLines.Clear();

        string[] trigrams = new string[2];
        string[] changedTrigrams = new string[2];

        for (int row = 0; row < RowCount; row++)
        {
            UpdateTrigrams(row, trigrams, changedTrigrams);
        }

        // Trigrams returned as strings like "101" the hexagramLookup dictionary uses these as decimal int keys for looking up trigram numbers
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
                
                break;
            case 7:
                var noChange0 = "0";
                changedTrigrams[trigramIndex] += noChange0;
                trigrams[trigramIndex] += noChange0;
                break;
            case 8:
                var noChange1 = "1";
                changedTrigrams[trigramIndex] += noChange1;
                trigrams[trigramIndex] += noChange1;
                break;
            case 9:
                changedTrigrams[trigramIndex] += "0";
                trigrams[trigramIndex] += "1";
                break;
        }
        if (IsChangingLine(row))
        {
            _changingLines.Add(RowCount - row); // Módosítva: RowCount - row helyett row + 1
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

    private bool IsChangingLine(int row)
    {
        bool firstValue = _values.GetValue(row, 0);
        for (int col = 1; col < ColCount; col++)
        {
            if (_values.GetValue(row, col) != firstValue)
            {
                return false;
            }
        }
        return true;
    }
}
