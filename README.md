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

### Yarrow Stalks UI

Here's a C# WinForms code snippet that creates a new form with 49 vertical, narrow lines and 48 narrow buttons among the lines in one row. When a button is clicked, it displays the number of lines to the left and right of that button:

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

public class LinesAndButtonsForm : Form
{
    private const int LineWidth = 2;
    private const int LineHeight = 100;
    private const int ButtonWidth = 40;
    private const int ButtonHeight = 20;
    private const int Spacing = 2;

    private Panel[] lines;
    private Button[] buttons;

    public LinesAndButtonsForm()
    {
        InitializeComponents();
        GenerateLinesAndButtons();
    }

    private void InitializeComponents()
    {
        Text = "Lines and Buttons Form";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
    }

    private void GenerateLinesAndButtons()
    {
        lines = new Panel[49];
        buttons = new Button[48];

        int x = Spacing;
        int y = Spacing;

        for (int i = 0; i < 49; i++)
        {
            // Create a line
            Panel line = new Panel
            {
                Width = LineWidth,
                Height = LineHeight,
                Location = new Point(x, y),
                BackColor = Color.Black
            };
            lines[i] = line;
            Controls.Add(line);

            // Create a button (if applicable)
            if (i < 48)
            {
                int buttonX = x + LineWidth + Spacing;
                Button button = new Button
                {
                    Width = ButtonWidth,
                    Height = ButtonHeight,
                    Location = new Point(buttonX, y + (LineHeight - ButtonHeight) / 2),
                    Text = (i + 1).ToString()
                };
                button.Click += Button_Click;
                buttons[i] = button;
                Controls.Add(button);

                x += LineWidth + ButtonWidth + Spacing * 2;
            }
            else
            {
                x += LineWidth + Spacing;
            }
        }

        ClientSize = new Size(x + Spacing, LineHeight + Spacing * 2);
    }

    private void Button_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int buttonIndex = Array.IndexOf(buttons, clickedButton);

        int linesLeft = buttonIndex;
        int linesRight = 47 - buttonIndex;

        MessageBox.Show($"Lines to the left: {linesLeft}\nLines to the right: {linesRight}", "Button Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new LinesAndButtonsForm());
    }
}
```

Here's how the code works:

1. The `LinesAndButtonsForm` class inherits from `Form`.
2. In the constructor, the `InitializeComponents` method is called to set up the form properties, and then the `GenerateLinesAndButtons` method is called to create the lines and buttons.
3. The `InitializeComponents` method sets the form's title, position, border style, and disables the maximize button.
4. The `GenerateLinesAndButtons` method creates two arrays: `lines` to store the `Panel` instances representing the lines, and `buttons` to store the `Button` instances.
5. A loop iterates from 0 to 48, creating a new `Panel` instance for each line and a `Button` instance for each button (except for the last line).
6. Each line and button is added to their respective arrays (`lines` and `buttons`).
7. Each button's `Click` event is subscribed to the `Button_Click` event handler.
8. The `Button_Click` event handler is called when a button is clicked.
9. Inside the `Button_Click` event handler, the clicked button is identified using the `sender` object.
10. The index of the clicked button in the `buttons` array is obtained using `Array.IndexOf`.
11. The number of lines to the left and right of the clicked button is calculated based on the button's index.
12. A message box is displayed showing the number of lines to the left and right of the clicked button.
13. In the `Main` method, the `Application.Run` method is called with an instance of the `LinesAndButtonsForm` class to start the application and display the form.

When you run this code, a new form will open with 49 vertical, narrow lines and 48 narrow buttons among the lines in one row. When you click on a button, a message box will appear displaying the number of lines to the left and right of that button.

Note: Remember to replace the `using` statements at the top with the appropriate namespaces for your project.