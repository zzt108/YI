# YI
## Data structure
![Data structure](
https://www.plantuml.com/plantuml/png/JOun2y8m48Nt-nMt5OGuEZX8nmuT5DJzQ8yOI2wGNAGY_dUDsDhr--xTUsrMIbg2X-ReIVGI_7Q80M3mb3DsF95D59w0w4JnIhuml6RhiiRqg39hScBnL3YhYxASd7dIbU-OHauV2z3qJXYDYKi9xl56TyOT7g3cS6FMJlxOO4zY2rc6-cMcyLi7lrcLB7c0bcKimRy1
)
## Dotnet EF

- Open the package manager prompt
- To install the dotnet-ef tool, run the following command:
- For .NET 7
  - PM> dotnet tool install --global dotnet-ef
- add Migration

PM> dotnet ef migrations add Initial --project DataLayer --context YiDbContext
- undo Migration
To undo this action, use 'ef migrations remove' (no 'Initial')

PM> dotnet ef migrations remove --project DataLayer --context yidbcontext

Your startup project 'DataLayer' doesn't reference Microsoft.EntityFrameworkCore.Design. 
--> Do not just add nuget package but add using Microsoft.EntityFrameworkCore.Design; 

# Plans
- ? Randomized throws
- Open AI web page with YiWin form summary ?
- User reaction based coin throws
  - gif
  - based on user response or position
- Stalk based hexagrams
- Continuous saving of data, restore last state when started
- Do UI in MAUI/Android

## open Perplexity.ai with a question in the URL.

### Use the following URL format:
https://www.perplexity.ai/?s=O&q=your_question_here
Replace "your_question_here" with your actual question. This will open Perplexity.ai and perform a search for your question

### You can also add Perplexity.ai as a custom search engine in your browser with the following URL:
https://www.perplexity.ai/search?q=%s
Then when you type your question in the address bar and hit enter, it will open Perplexity.ai and search for your question

### Some additional tips:
You can add more parameters to the URL like &focus=[internet,scholar,writing,wolfram,youtube,reddit] to specify the search focus
Setting copilot=true will enable pro mode for the search
The OpenAI API can also be used to access Perplexity models programmatically

## Yarow stalks

Here's the C# code to implement the instructions you provided:

```csharp
using System;
using System.Collections.Generic;

public class YarrowStalksHelper
{
    private const int TotalStalks = 50;
    private const int ObserverStalk = 1;
    private const int RemainingStalkCount = TotalStalks - ObserverStalk;

    private List<int> GetCountedPiles()
    {
        List<int> countedPiles = new List<int>();

        int remainingStalkCount = RemainingStalkCount;
        int totalCount;

        for (int i = 0; i < 3; i++)
        {
            List<int> piles = new List<int>();
            int remainder = remainingStalkCount;

            while (remainder >= 4)
            {
                piles.Add(4);
                remainder -= 4;
            }

            if (remainder > 0)
            {
                piles.Add(remainder);
            }

            totalCount = GetTotalCount(piles);
            countedPiles.Add(totalCount);

            remainingStalkCount = totalCount % 4 == 0 ? 8 : 5;
        }

        return countedPiles;
    }

    private int GetTotalCount(List<int> piles)
    {
        int totalCount = piles.Count;

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

    public int[] GetHexagramLineValues()
    {
        List<int> countedPiles = GetCountedPiles();
        int[] hexagramLineValues = new int[6];

        for (int i = 0; i < countedPiles.Count; i++)
        {
            int value = countedPiles[i] % 4 == 0 ? 2 : 3;
            hexagramLineValues[i] = value;
        }

        return hexagramLineValues;
    }
}
```

Here's how the code works:

1. The `GetCountedPiles` method follows the instructions to divide the remaining 49 yarrow stalks into piles of 4 or less, and count the total for each pile.
2. The `GetTotalCount` method calculates the total count for a given set of piles, following the rules of counting 3 for piles of 4 or 5, and 2 for piles of 8 or 9.
3. The `GetCountedPiles` method repeats the process three times, each time using the remainder from the previous step as the starting point.
4. The `GetHexagramLineValues` method takes the counted piles and converts them into hexagram line values (2 for even numbers, 3 for odd numbers).
5. The resulting hexagram line values are returned as an array of 6 integers.

You can use this code by creating an instance of the `YarrowStalksHelper` class and calling the `GetHexagramLineValues` method:

```csharp
YarrowStalksHelper helper = new YarrowStalksHelper();
int[] hexagramLineValues = helper.GetHexagramLineValues();
```

The `hexagramLineValues` array will contain the six values representing the hexagram lines, following the instructions you provided.