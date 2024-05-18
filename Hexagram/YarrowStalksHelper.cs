using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Test")]

namespace HexagramNS;
/// <summary>
/// Helper class for generating hexagram line values based on the count of piles.
/// </summary>
public class YarrowStalksHelper
{
    // Constants
    private const int TotalStalks = 50; // Total number of stals in a Yarrow
    private const int ObserverStalk = 1; // Number of observer stals
    public int RemainingStalkCount = TotalStalks-ObserverStalk; // Number of remaining stals

    public void Reset() { RemainingStalkCount = TotalStalks - ObserverStalk; }

    // TODO Divide remaining stalks into right and left piles
    private int[] DividePiles(int position)
    {
        int rightPiles = RemainingStalkCount - position;
        int leftPiles = position;
        return new int[] { leftPiles, rightPiles };
    }

    public int GetHand(int position)
    {
        var divide = DividePiles(position);

        int hand = 1;
        divide[1] -= 1;

        int leftRemainder = divide[0] % 4;
        int rightRemainder = divide[1] % 4;
        hand += leftRemainder == 0 ? 4 : leftRemainder;

        hand += rightRemainder == 0 ? 4 : rightRemainder;

        Console.WriteLine(hand);
        Debug.Assert(new[] { 4, 5, 8, 9 }.Contains(hand));
        RemainingStalkCount -= hand;
        return hand;
    }

    internal int GetLine(int[] position)
    {
        RemainingStalkCount = TotalStalks - ObserverStalk;

        int[] piles = { 0, 0, 0 };

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"---Pos:{position[i]}: Remaining stalks: {RemainingStalkCount}");
            var hand = GetHand(position[i]);
            piles[i] = hand;
        }

        int totalCount = 0;
        foreach (int pile in piles)
        {
            if (pile == 4 || pile == 5)
            {
                totalCount += 3;
            }
            else
            {
                totalCount += 2;
            }
        }

        return totalCount;

    }

    /// <summary>
    /// Generates an array of hexagram line values based on the counted piles.
    /// </summary>
    /// <returns>An array of integer values for each hexagram line.</returns>
    public int[] GetHexagramLineValues(int[] positions)
    {
        int[] hexagramLineValues = new int[6];

        // Iterate over the counted piles
        for (int i = 0; i < 6; i++)
        {
            int value = GetLine([positions[i], positions[i], positions[i]]);

            // Console.WriteLine($"Line {i}: {value}");

            hexagramLineValues[i] = value;
        }

        return hexagramLineValues;
    }
}