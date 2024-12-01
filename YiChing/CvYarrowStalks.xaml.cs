using HexagramNS;

namespace YiChing;

public partial class CvYarrowStalks : ContentView
{

    const int YarrowClickTiming = 4;
    const int StickCount = 49;

    private int LineWidth = 4;
    private int LineHeight = 400;
    private int ButtonWidth = 2;
    private int ButtonHeight = 400;

    private readonly Button[] lines = []; // instantiated dynamically later
    private readonly Button[] buttons = []; // instantiated dynamically later
    //private readonly Grid yarrowGrid = new Grid();
    private readonly int[] piles = [0, 0, 0];
    private readonly YarrowStalksHelper helper = new();
    private readonly MainPage mainPage;
    private readonly Values values = new();

    private int clickCount;
    private int hexagramRow;

    public Editor Question
    {
        get
        {
            // var text = (string)rtQuestion.GetValue(Editor.TextProperty);
            return rtQuestion;
        }
        set
        {
            rtQuestion = value;
        }
    }

    public CvYarrowStalks(MainPage mainPage)
    {
        this.mainPage = mainPage;
        InitializeComponent();
        btnReturn.Clicked += btnReturn_Click;
        lines = new Button[StickCount];
        buttons = new Button[StickCount - 1];
    }

    public void InitProcess()
    {
        clickCount = 0;
        hexagramRow = values.RowCount;
        GenerateLinesAndButtons(helper.RemainingStalkCount, gridYarrow);
    }

    private void btnReturn_Click(object? sender, EventArgs e)
    {
        ReturnToHexagramPage();
    }

    private void GenerateLinesAndButtons(int stickCount, Grid controls)
    {

#if true

#else
        var width = (controls).Width;
        var height = (controls).Height;

        LineHeight = (int)(controls.Height - (Spacing * 2));
        LineWidth = (int)((width - (Spacing * 2)) / stickCount);

        ButtonHeight = (int)(controls.Height - (Spacing * 2));
        ButtonWidth = (int)((width - (Spacing * 2)) / stickCount);
#endif
        controls.Children.Clear();
        controls.RowDefinitions.Clear();
        controls.ColumnDefinitions.Clear();
        controls.ClearLogicalChildren();

        controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

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
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            line.Clicked += Line_Click;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            lines[i] = line;
            line.Text = i.ToString();
            controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            controls.SetColumn(line, controls.ColumnDefinitions.Count - 1);
            controls.Children.Add(line);

            // Create a button (if applicable)
            if (i < stickCount - 1)
            {
                Button button = new Button
                {
                    MinimumWidthRequest = ButtonWidth,
                    WidthRequest = ButtonWidth,
                    HeightRequest = ButtonHeight,
                    BackgroundColor = Colors.AliceBlue,
                    Text = string.Empty
                };
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                button.Clicked += Button_Click;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                buttons[i] = button;
                controls.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                controls.SetColumn(button, controls.ColumnDefinitions.Count - 1);
                controls.Children.Add(button);
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
        const int MaxDivisionCount = 18;

        piles[clickCount % 3] = helper.GetHand(linesLeft);

        if (clickCount < MaxDivisionCount)
        {
            clickCount++;
            mainPage.Title = $"{clickCount}/{MaxDivisionCount}";
            if (clickCount % 3 == 0)
            {
                hexagramRow--;
                values.SetHexagramRow(hexagramRow, YarrowStalksHelper.GetHexagramLine(piles));
                mainPage.CVHexagram.FillCheckBoxes(values);
                helper.Reset();
            }
            if (clickCount == MaxDivisionCount)
            {
                ReturnToHexagramPage();
            }
        }

        //gridYarrow.IsEnabled = false;
        gridYarrow.IsVisible = false;
        Thread.Sleep(YarrowClickTiming * 150);

        //foreach (var line in lines) { line.BackgroundColor = Colors.Gray; Thread.Sleep(YarrowClickTiming/2); }
        //foreach (var line in buttons) { line.BackgroundColor = Colors.Gray; Thread.Sleep(YarrowClickTiming/2); }
        //foreach (var line in lines) { line.HeightRequest = 0; Thread.Sleep(YarrowClickTiming / 2); }
        //foreach (var line in buttons) { line.HeightRequest = 50; Thread.Sleep(YarrowClickTiming / 2); }
        gridYarrow.IsEnabled = true;
        gridYarrow.IsVisible = true;

        //var b = mainPage.ShowMessageBox($"Lines to the left: {linesLeft}", "Button Click");
        GenerateLinesAndButtons(helper.RemainingStalkCount, gridYarrow);
    }

    private void ReturnToHexagramPage()
    {
        mainPage.CVHexagram.ViewModel.Question = rtQuestion.Text;
        mainPage.Content = mainPage.CVHexagram;
    }

    private void Line_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineIndex = Array.IndexOf(lines, clickedButton);

        int linesLeft = lineIndex;
        if (lineIndex < helper.RemainingStalkCount / 2) linesLeft++;

        HandleClick(linesLeft);
    }
}