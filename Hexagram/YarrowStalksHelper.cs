using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagramNS;

/// <summary>
/// Helper class for generating hexagram line values based on the count of piles.
/// </summary>
public class YarrowStalksHelper
{
    // Constants
    private const int TotalStalks = 50; // Total number of stals in a Yarrow
    private const int ObserverStalk = 1; // Number of observer stals
    private const int RemainingStalkCount = TotalStalks - ObserverStalk; // Number of remaining stals

    /// <summary>
    /// Generates a list of pile counts for the hexagram.
    /// </summary>
    /// <returns>A list of integer counts for each pile.</returns>
    private List<int> GetCountedPiles()
    {
        List<int> countedPiles = new List<int>();

        int remainingStalkCount = RemainingStalkCount;
        int totalCount;

        // Iterate over the piles
        for (int i = 0; i < 3; i++)
        {
            List<int> piles = new List<int>();
            int remainder = remainingStalkCount;

            // Divide the remaining piles into 4s and the remainder
            while (remainder >= 4)
            {
                piles.Add(4);
                remainder -= 4;
            }

            // Add the remainder if it's greater than 0
            if (remainder > 0)
            {
                piles.Add(remainder);
            }

            // Calculate the total count of piles
            totalCount = GetTotalCount(piles);
            countedPiles.Add(totalCount);

            // Calculate the remaining stalk count for the next iteration
            remainingStalkCount = totalCount % 4 == 0 ? 8 : 5;
        }

        return countedPiles;
    }

    /// <summary>
    /// Calculates the total count of piles, considering the special cases of 4 and 5.
    /// </summary>
    /// <param name="piles">A list of integer counts for each pile.</param>
    /// <returns>The total count of piles.</returns>
    private int GetTotalCount(List<int> piles)
    {
        int totalCount = piles.Count;

        // Iterate over each pile
        foreach (int pile in piles)
        {
            // Add 3 for piles of 4 or 5, and 2 for other piles
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
    public int[] GetHexagramLineValues()
    {
        List<int> countedPiles = GetCountedPiles();
        int[] hexagramLineValues = new int[6];

        // Iterate over the counted piles
        for (int i = 0; i < countedPiles.Count; i++)
        {
            // Set the value to 2 if the count is divisible by 4, and 3 otherwise
            int value = countedPiles[i] % 4 == 0 ? 2 : 3;
            hexagramLineValues[i] = value;
        }

        return hexagramLineValues;
    }
}