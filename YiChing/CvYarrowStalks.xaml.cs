using HexagramNS;

namespace YiChing;

public partial class CvYarrowStalks : ContentPage
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
    private readonly YarrowStalksHelper helper;
    private readonly MainPage mainPage;
    private readonly Values values;

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
        helper = new YarrowStalksHelper();
        values = new Values();
        InitProcess();  // Call InitProcess immediately after initialization
    }

    public void InitProcess()
    {
        clickCount = 0;
        hexagramRow = values.RowCount;
        GenerateLinesAndButtons(helper.RemainingStalkCount, gridYarrow);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        InitProcess();
    }

    private async void btnReturn_Click(object? sender, EventArgs e)
    {
        mainPage.CVHexagram.ViewModel.Question = rtQuestion.Text;
        await Shell.Current.GoToAsync("/CvHexagram");
    }

    private void GenerateLinesAndButtons(int stickCount, Grid controls)
    {
        var width = controls.Width;
        var height = controls.Height;

        // Use default values if width/height not yet set
        if (width <= 0) width = 400;
        if (height <= 0) height = 400;

        LineHeight = (int)(height - 20);  // 20 for spacing
        LineWidth = Math.Max(4, (int)((width - 20) / stickCount));

        ButtonHeight = LineHeight;
        ButtonWidth = Math.Max(2, (int)((width - 20) / stickCount));

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

    private async void HandleClick(int linesLeft)
    {
        const int MaxDivisionCount = 18;

        piles[clickCount % 3] = helper.GetHand(linesLeft);

        if (clickCount < MaxDivisionCount)
        {
            clickCount++;
            
            // Update progress label
            lblProgress.Text = $"Yarrow Stalks: {clickCount}/18 clicks";
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

        gridYarrow.IsVisible = false;
        await Task.Delay(YarrowClickTiming * 150);
        gridYarrow.IsEnabled = true;
        gridYarrow.IsVisible = true;

        GenerateLinesAndButtons(helper.RemainingStalkCount, gridYarrow);
    }

    private async void ReturnToHexagramPage()
    {
        mainPage.CVHexagram.ViewModel.Question = rtQuestion.Text;
        await Shell.Current.GoToAsync("/CvHexagram");
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