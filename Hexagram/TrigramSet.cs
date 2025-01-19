namespace HexagramNS;

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

public class TrigramSet
{
    public TrigramSet()
    {
        this.TopBinaryString = "xxx";
        this.BottomBinaryString = "xxx";
    }

    public TrigramSet(int keyTop, int keyBottom):this()
    {
        Top = keyTop;
        Bottom = keyBottom;
    }

    public int Top
    {
        get
        {
            return int.Parse(TopBinaryString);
        }
        set
        {
            TopBinaryString = Convert.ToString(value, 10).PadLeft(3, '0');
        }
    }
    public int Bottom
    {
        get
        {
            return int.Parse(BottomBinaryString);
        }
        set
        {
            BottomBinaryString = Convert.ToString(value, 10).PadLeft(3, '0');
        }
    }

    public string TopBinaryString;
    public string BottomBinaryString;

    public override string ToString()
    {
        // Concatenate the binary strings to form the 6-digit hexagram representation
        return TopBinaryString + BottomBinaryString;
    }

    public void SetLine(int index, bool isYang)
    {
        var trigramUpper = index > 2;
        var trigramPos = index % 3;

        if (trigramUpper)
        {
            SetValue(TopBinaryString, trigramPos, isYang);
        }
        else
        {
            SetValue(BottomBinaryString, trigramPos, isYang);
        }
    }

    private void SetValue(string binaryString, int trigramPos, bool isYang)
    {
        if (isYang)
        {
            binaryString = binaryString.Remove(trigramPos, 1).Insert(trigramPos, "1");
        }
        else
        {
            binaryString = binaryString.Remove(trigramPos, 1).Insert(trigramPos, "0");
        }
        if (trigramPos == 0)
        {
            TopBinaryString = binaryString;
        }
        else
        {
            BottomBinaryString = binaryString;
        }
    }
}
