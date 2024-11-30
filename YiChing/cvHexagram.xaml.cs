using AndroidX.Lifecycle;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Web;
using YiChing.ViewModels;
using HG = HexagramNS;

namespace YiChing;

public partial class CvHexagram : ContentView
{
    #region Privates

    private ILoggerFactory loggerFactory = LoggerFactory.Create(static builder => builder.AddDebug());
    private ILogger<JsonHandler> logger;
    private IConfiguration configuration = new ConfigurationBuilder().Build();
    private readonly HexagramViewModel _viewModel;

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
        return $"{DateTime.Now:yyyy-MM-dd}\nQuestion to I Ching:\n {rtQuestion.Text}\n"
            + $"\nI Ching answered:\n{rtAnswer.Text}\nWould you please interpret?\n\nPlease translate to {_viewModel.Settings.AnswerLanguage}.";
    }

    #endregion

    #region Constructor
    public CvHexagram(HexagramViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;

        loggerFactory = LoggerFactory.Create(static builder => builder.AddDebug());
        logger = loggerFactory.CreateLogger<JsonHandler>();
        configuration = new ConfigurationBuilder().Build();

        // Add event handlers
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        btnCopy.Clicked += btnCopy_Click;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        btnClear.Clicked += btnClear_Click;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        btnYarrow.Clicked += btnYarrow_Click;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        btnConfig.Clicked += btnConfig_Click;

        rtQuestion.TextChanged += (sender, e) => { Question.Text = e.NewTextValue; };

        DrawHexagram();
    }
    #endregion

    private void LoadHexagrams()
    {
        var jsonHandler = new JsonHandler(logger, configuration);
        // var jsonHandler = new JsonHandler();

        // Assuming you have a method to read the JSON data
        var hexagramEntries = jsonHandler.ReadHexagramEntriesFromJson(); // Implement this method based on your JSON structure

        foreach (var entry in hexagramEntries)
        {
            hexagramPicker.Items.Add($"{entry.DisplayText}"); // Customize the display text as needed
        }
    }

    private void OnHexagramSelected(object sender, EventArgs e)
    {
        if (hexagramPicker.SelectedItem != null)
        {

            var selectedHexagram = hexagramPicker.SelectedItem.ToString();
            // Extract corresponding hexagram details, assuming id can be parsed from selectedItem
            // var hexagramId = int.Parse(new string(selectedHexagram.Where(char.IsDigit).ToArray()));
            var jsonHandler = new JsonHandler(logger, configuration);
            var hexagramDetails = jsonHandler.GetHexagramDetails(selectedHexagram);

            rtAnswer.Text = hexagramDetails?.Answer; // Display hexagram description
            rtQuestion.Text = hexagramDetails?.Question;
        }
    }

    // TODO encapsulate rtQuestion.Text into property
    public Editor Question
    {
        get
        {
            return rtQuestion;
        }
        set
        {
            rtQuestion = value;
        }
    }

    public Editor Answer { get => rtAnswer; set => rtAnswer = value; }

    public string Title = "Yi Ching for AI by Gerzson";

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

    private void EvalAndSaveHexagram()
    {
        string question = rtQuestion.Text;
        var hexagram = new HG.Hexagram(new HG.Values().InitValues<CheckBox>(CheckBoxes, (checkBox, row, col) => checkBox.IsChecked));

        // Example logic to retrieve the main hexagram.
        int mainHexagram = hexagram.Main;

        rtAnswer.Text = $"\nMain Hexagram {mainHexagram}\n\n";

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
        {
            rtAnswer.Text += "\n No changing lines ";
        }

        // Save the question and hexagram result
        var jsonHandler = new JsonHandler(logger, configuration);
        jsonHandler.SaveEntry(new HexagramEntry(question, rtAnswer.Text));

        // Refresh the hexagram picker with the updated entries
        RefreshHexagramPicker();
    }

    private void RefreshHexagramPicker()
    {
        // Clear existing items
        hexagramPicker.Items.Clear();
        // Reload hexagrams from JSON
        LoadHexagrams();
    }
    #region EventHandlers

    private void btnConfig_Click(object? sender, EventArgs e)
    {
        _viewModel.NavigateToConfig();
    }

    private void btnEval_Click(object? sender, EventArgs e)
    {
        EvalAndSaveHexagram();
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
        EvalAndSaveHexagram();
        string full = GetFullQuestion();
        Clipboard.SetTextAsync(full);
    }

    private void btnYarrow_Click(object sender, EventArgs e)
    {
        // ResetIndeterminate();
        _viewModel.NavigateToYarrowStalks(rtQuestion.Text);
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        //var task = _mainPage.DisplayAlert("Are you sure?", "Clear question and answer?", "Yes", "No");
        //task.Wait();
        //if (!task.Result)
        //    return;
        rtQuestion.Text = string.Empty;
        ResetIndeterminate();
    }
    private async void btnPerpAI_Click(object? sender, EventArgs e)
    {
        try
        {
            EvalAndSaveHexagram();
            string question = HttpUtility.UrlEncode(GetFullQuestion());
            string url = $"https://www.perplexity.ai/search?s=o&q={question}";
            Uri uri = new Uri(url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
        }
        catch (Exception ex)
        {
            // Handle potential exceptions (e.g., no browser installed)
            await _viewModel.DisplayAlert("Error", "Failed to open web page: " + ex.Message, "OK");
        }
    }
    #endregion
}
