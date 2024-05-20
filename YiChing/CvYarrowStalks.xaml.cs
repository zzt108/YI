using HexagramNS;

namespace YiChing;

public partial class CvYarrowStalks : ContentView
{

    const int YarrowClickTiming = 2;
    const int StickCount = 49;
    private int LineWidth = 8;
    private int LineHeight = 400;
    private int ButtonWidth = 4;
    private int ButtonHeight = 400;
    private int Spacing = 3;

    private Button[] lines;
    private Button[] buttons;

    private Grid yarrowGrid = new Grid();

    public int[] divisions = new int[18];
    private YarrowStalksHelper helper = new YarrowStalksHelper();
    int[] piles = { 0, 0, 0 };
    int clickCount = 0;
    public Values values = new Values();
    int hexagramRow;
    public MainPage mainPage;

    public CvYarrowStalks(MainPage mainPage)
    {
        this.mainPage = mainPage;
        InitializeComponent();
        lines = new Button[StickCount];
        buttons = new Button[StickCount - 1];
        hexagramRow = values.RowCount;
        this.mainPage = mainPage;
        //yarrowGrid = gridMain;
        Content = yarrowGrid;
        GenerateLinesAndButtons(helper.RemainingStalkCount, yarrowGrid);
    }

    private void GenerateLinesAndButtons(int stickCount, Grid controls)
    {
        // Generate the lines and buttons
        LineHeight = 400; // (int)(controls.Height - (Spacing * 2));
        LineWidth = 10; //(int)((controls.Width - (Spacing * 2)) / stickCount);

        ButtonHeight = 400; // (int)(controls.Height - (Spacing * 2));
        ButtonWidth = 10; //(int)((controls.Width - (Spacing * 2)) / stickCount);

        controls.Children.Clear();
        controls.RowDefinitions.Clear();
        controls.ColumnDefinitions.Clear();
        controls.ClearLogicalChildren();
        controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

        int x = Spacing;
        int y = Spacing;

        for (int i = 0; i < stickCount; i++)
        {
            // Create a line
            Button line = new Button
            {
                MinimumWidthRequest = LineWidth,
                WidthRequest = LineWidth,
                HeightRequest = LineHeight,
                BackgroundColor = Colors.ForestGreen,
            };
            line.Clicked += Line_Click;
            lines[i] = line;
            line.Text = i.ToString();
            controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            controls.SetColumn(line, controls.ColumnDefinitions.Count - 1);
            controls.Children.Add(line);

            // Create a button (if applicable)
            if (i < stickCount - 1)
            {
                int buttonX = x + LineWidth + Spacing;
                Button button = new Button
                {
                    MinimumWidthRequest = ButtonWidth,
                    WidthRequest = ButtonWidth,
                    HeightRequest = ButtonHeight,
                    BackgroundColor = Colors.AliceBlue,
                    Text = string.Empty
                };
                button.Clicked += Button_Click;
                buttons[i] = button;
                controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                controls.SetColumn(button, controls.ColumnDefinitions.Count - 1);
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
        GenerateLinesAndButtons(helper.RemainingStalkCount, yarrowGrid);

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