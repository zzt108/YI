using HexagramNS;

namespace YiChing;

public partial class CvYarrowStalks : ContentView
{

    const int YarrowClickTiming = 2;
    private const int LineWidth = 4;
    private const int LineHeight = 800;
    private const int ButtonWidth = 4;
    private const int ButtonHeight = LineHeight;
    private const int Spacing = 3;

    private Button[] lines;
    private Button[] buttons;

    public int[] divisions = new int[18];
    private YarrowStalksHelper helper = new YarrowStalksHelper();
    int[] piles = { 0, 0, 0 };
    int clickCount = 0;
    public Values values = new Values();
    int hexagramRow;
    public MainPage mainPage;


    private Grid gridSticks = new ();

    public CvYarrowStalks()
    {
        InitializeComponent();
        Content = gridSticks;
        GenerateLinesAndButtons(helper.RemainingStalkCount, gridSticks);
        hexagramRow = values.RowCount;
    }

    private void GenerateLinesAndButtons(int stickCount, Grid controls)
    {
        lines = new Button[stickCount];
        buttons = new Button[stickCount - 1];
        controls.Clear();

        int x = Spacing;
        int y = Spacing;

        for (int i = 0; i < stickCount; i++)
        {
            // Create a line
            Button line = new Button
            {
                WidthRequest = LineWidth,
                HeightRequest = LineHeight,
                BorderColor = Colors.ForestGreen,
            };
            line.Clicked += Line_Click;
            lines[i] = line;
            line.Text = i.ToString();
            controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            controls.SetColumn(line, i);
            controls.Children.Add(line);

            // Create a button (if applicable)
            if (i < stickCount - 1)
            {
                int buttonX = x + LineWidth + Spacing;
                Button button = new Button
                {
                    WidthRequest = ButtonWidth,
                    HeightRequest = ButtonHeight,
                    BorderColor = Colors.AliceBlue,
                    Text = string.Empty
                };
                button.Clicked += Button_Click;
                buttons[i] = button;
                controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                controls.SetColumn(button, i+1);
                controls.Children.Add(button);

                //x += LineWidth + ButtonWidth + Spacing * 2;
            }
            else
            {
                //x += LineWidth + Spacing;
            }
        }

        //ClientSize = new Size(x + Spacing, LineHeight + Spacing * 2);
    }

    private void Button_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int buttonIndex = Array.IndexOf(buttons, clickedButton);

        int linesLeft = buttonIndex + 1;
        //int linesRight = buttons.Length - buttonIndex;

        HandleClick(linesLeft);

    }

    private void HandleClick(int linesLeft)
    {

        piles[clickCount % 3] = helper.GetHand(linesLeft);

        foreach (var line in lines) { line.IsVisible = false; Thread.Sleep(YarrowClickTiming); }
        if (clickCount < 18)
        {
            clickCount++;
            if (clickCount % 3 == 0)
            {
                hexagramRow--;
                values.SetHexagramRow(hexagramRow, YarrowStalksHelper.GetHexagramLine(piles));
                mainPage.CVHexagram.FillCheckBoxes(values);
                helper.Reset();
            }
            if (clickCount == 18)
            {
                mainPage.Content = mainPage.CVHexagram;
            }
        }
        mainPage.Title = $"18/: {clickCount} - 1:{piles[0]} -  2:{piles[1]} - 3:{piles[2]}";

        var b = mainPage.ShowMessageBox($"Lines to the left: {linesLeft}", "Button Click");
        GenerateLinesAndButtons(helper.RemainingStalkCount, gridSticks);

    }

    private void Line_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineIndex = Array.IndexOf(lines, clickedButton);

        int linesLeft = lineIndex;
        if (lineIndex < helper.RemainingStalkCount / 2) linesLeft++;

        // linesRight = lines.Length - linesLeft;

        HandleClick(linesLeft);
    }


}