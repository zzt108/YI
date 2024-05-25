using HG = HexagramNS;

namespace YiChing;

public partial class CvHexagram : ContentView
{
    #region Privates
    private const int HorDist = 100;
    private const int VerDist = 40;
    private const int HorStart = 20;
    private const int VerStart = 40;
    private void DrawHexagram()
    {
        // Set the initial location for the checkboxes
        //int x = HorStart;
        //int y = VerStart;

        // Add CheckBoxes to the Panel in a 6x3 arrangement
        for (int row = 0; row < HG.Hexagram.RowCount; row++)
        {
            Label labelCB = new Label
            {
                Text = $"{HG.Hexagram.RowCount - row}",
                WidthRequest = 40,
                VerticalTextAlignment = TextAlignment.Center
            };
            gridHexagram.Add(labelCB, gridHexagram.ColumnDefinitions.Count - 1, row + 1);
            labelCB.WidthRequest = 40;

            for (int col = 0; col < HG.Hexagram.ColCount; col++)
            {
                CheckBox checkBox = new CheckBox();
                CheckBoxes[row, col] = checkBox;
                checkBox.WidthRequest = 40;

                // checkBox.Location = new Point(x, y);
                gridHexagram.Add(checkBox, col + 1, row + 1);
                //x += HorDist; // Adjust the horizontal spacing between checkboxes
            }
        }
    }

    private void ResetIndeterminate()
    {
        new HG.Values().UpdateValues(
            CheckBoxes,
            (row, col, value) =>
            {

                CheckBox checkBox = CheckBoxes[row, col];
                checkBox.IsChecked = false;
                return checkBox;
            });
    }

    private string GetFullQuestion()
    {
        return $"{DateTime.Now:d}\nQuestion to I Ching:\n {rtQuestion.Text}\n"
            + $"\nI Ching answered:\n{rtAnswer.Text}\nWould you please interpret?";
    }

    #endregion

    #region Constructor
    public CvHexagram(MainPage mainPage)
    {
        InitializeComponent();
        mainPage.Title = Title;
        // Add event handlers
        btnCopy.Clicked += btnCopy_Click;
        btnEval.Clicked += btnEval_Click;
        btnClear.Clicked += btnClear_Click;
        btnYarrow.Clicked += btnYarrow_Click;
        btnPerpAI.Clicked += btnPerpAI_Click;
        btnConfig.Clicked += btnConfig_Click;

        rtQuestion.TextChanged += (sender, e) => { Question.Text = e.NewTextValue; };

        DrawHexagram();
        this.mainPage = mainPage;
    }
    #endregion


    // TODO encapsulate rtQuestion.Text into property
    public Editor Question
    {
        get
        {
            mainPage.Title = Title;
            return rtQuestion;
        }
        set
        {
            mainPage.Title = Title;
            rtQuestion = value;
        }
    }

    public string Title = "Yi Ching for AI by Gerzson";

    public MainPage mainPage;

    protected CheckBox[,] CheckBoxes = new CheckBox[HG.Hexagram.RowCount, HG.Hexagram.ColCount];

    public void FillCheckBoxes(HG.Values values)
    {
        values.UpdateValues<CheckBox>(CheckBoxes, (row, col, value) =>
        {
            CheckBox checkBox = CheckBoxes[row, col];

            checkBox.IsChecked = value;
            return checkBox;
        });
    }

    #region EventHandlers

    private void btnConfig_Click(object? sender, EventArgs e)
    {
        mainPage.Content = mainPage.CVConfig;
    }

    private void btnEval_Click(object sender, EventArgs e)
    {
        // Get the text from the RichTextBox control
        string question = rtQuestion.Text;

        var hexagram = new HG.Hexagram(new HG.Values().InitValues<CheckBox>(CheckBoxes, (checkBox, row, col) => checkBox.IsChecked));

        rtAnswer.Text = $"\nMain Hexagram {hexagram.Main}\n\n";
        if (hexagram.ChangingLines.Any())
        {
            rtAnswer.Text += "Changing lines: ";
            foreach (int line in hexagram.ChangingLines)
            {
                rtAnswer.Text += line + ", ";
            }
            rtAnswer.Text += $"\n\nChanging Hexagram {hexagram.Changed}\n";
        }
        else
            rtAnswer.Text += "\n No changing lines ";

    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
        btnEval_Click(sender, e);
        string full = GetFullQuestion();
        Clipboard.SetTextAsync(full);
    }

    private void btnYarrow_Click(object sender, EventArgs e)
    {
        ResetIndeterminate();
        mainPage.CVYarrowStalks.Question.Text = rtQuestion.Text;
        mainPage.CVYarrowStalks.InitProcess();
        
        mainPage.Content = mainPage.CVYarrowStalks;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        rtQuestion.Text = string.Empty;
        ResetIndeterminate();
    }
    private async void btnPerpAI_Click(object? sender, EventArgs e)
    {
        try
        {
            btnEval_Click(sender, e);
            string question = GetFullQuestion();
            string url = $"https://www.perplexity.ai/search?s=o&q={question}";
            Uri uri = new Uri(url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
        }
        catch (Exception ex)
        {
            // Handle potential exceptions (e.g., no browser installed)
            await mainPage.DisplayAlert("Error", "Failed to open web page: " + ex.Message, "OK");
        }
    }
    #endregion


}

