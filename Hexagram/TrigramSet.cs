namespace HexagramNS;
using System;
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
        var trigramUpper = index < 3;
        var trigramPos = index % 3;

        if (trigramUpper)
        {
            SetValue(ref TopBinaryString, trigramPos, isYang);
        }
        else
        {
            SetValue(ref BottomBinaryString, trigramPos, isYang);
        }
    }

    private void SetValue(ref string binaryString, int trigramPos, bool isYang)
    {
        if (isYang)
        {
            binaryString = binaryString.Remove(trigramPos, 1).Insert(trigramPos, "1");
        }
        else
        {
            binaryString = binaryString.Remove(trigramPos, 1).Insert(trigramPos, "0");
        }
    }
}
