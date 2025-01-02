using HexagramNS;
using System.Web;
using HG = HexagramNS;

namespace YiChing;

public partial class CvHexagram : ContentView
{
    #region Privates

    MainPage _mainPage;
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
        if (_mainPage != null) // Check if _mainPage is not null
        {
            var settings = _mainPage.CVConfig.Settings;
            return $"{settings.TranslationRequest} {settings.AnswerLanguage} and provide an interpretation of the result.\n\n" +
                   $"Date: {DateTime.Now:yyyy-MM-dd}\n" +
                   $"{settings.QuestionPrefix} {rtQuestion.Text}\n\n" +
                   $"{settings.AnswerPrefix}\n\n" +
                   $"{rtAnswer.Text}\n\n" +
                   $"{settings.StepsHeader}\n\n" +
                   $"{settings.OutputFormatHeader}\n\n" +
                   $"{settings.NotesHeader}";
        }
        return string.Empty; // Return an empty string if _mainPage is null
    }

    #endregion

    #region Constructor
    public CvHexagram(MainPage mainPage)
    {
        InitializeComponent();
        mainPage.Title = Title;
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
        this._mainPage = mainPage;
        LoadHexagrams();
    }
    #endregion

    private void LoadHexagrams()
    {
        var jsonHandler = new JsonHandler();

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
            var jsonHandler = new JsonHandler();
            var hexagramDetails = jsonHandler.GetHexagramDetails(selectedHexagram);

            if (hexagramDetails != null)
            {
                rtAnswer.Text = hexagramDetails.Answer;
                rtQuestion.Text = hexagramDetails.Question;

                FillCheckBoxesFromHexagramNumbers(hexagramDetails.CurrentHexagram, hexagramDetails.NewHexagram);
            }
        }
    }

    // TODO encapsulate rtQuestion.Text into property
    public Editor Question
    {
        get
        {
            _mainPage.Title = Title;
            return rtQuestion;
        }
        set
        {
            _mainPage.Title = Title;
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

    private string HexagramToBinary(int hexagramNumber)
    {
        // Find the trigrams that correspond to the hexagram number using the reverse lookup
        var trigrams = FindTrigramsForHexagram(hexagramNumber);

        // Convert the trigrams to a 6-digit binary string
        string binaryString = TrigramsToBinaryString(trigrams);

        return binaryString;
    }

    private (int, int) FindTrigramsForHexagram(int hexagramNumber)
    {
        foreach (var upperTrigram in Hexagram.hexagramLookup)
        {
            foreach (var lowerTrigram in upperTrigram.Value)
            {
                if (lowerTrigram.Value == hexagramNumber)
                {
                    return (upperTrigram.Key, lowerTrigram.Key);
                }
            }
        }

        throw new ArgumentException($"Hexagram number {hexagramNumber} not found in lookup table.");
    }

    private string TrigramsToBinaryString((int, int) trigrams)
    {
        // Convert each trigram to its 3-digit binary representation
        string upperBinary = Convert.ToString(trigrams.Item1, 2).PadLeft(3, '0');
        string lowerBinary = Convert.ToString(trigrams.Item2, 2).PadLeft(3, '0');

        // Concatenate the binary strings to form the 6-digit hexagram representation
        return upperBinary + lowerBinary;
    }

    public void FillCheckBoxesFromHexagramNumbers(int currentHexagramNumber, int newHexagramNumber)
    {
        // 1. Hexagram Numbers to Binary Strings
        string currentBinary = HexagramToBinary(currentHexagramNumber);
        string newBinary = HexagramToBinary(newHexagramNumber);

        // 2. Binary Strings to Lines and Checkbox States
        for (int row = 0; row < Hexagram.RowCount; row++)
        {
            // Determine if the line is changing by comparing current and new binary
            bool isChanging = currentBinary[Hexagram.RowCount - 1 - row] != newBinary[Hexagram.RowCount - 1 - row];

            // Get the line value from the current binary string (0 or 1)
            char lineValue = currentBinary[Hexagram.RowCount - 1 - row]; // Read from right to left

            // Set checkbox states based on the line value and whether it's changing
            for (int col = 0; col < Hexagram.ColCount; col++)
            {
                if (lineValue == '1') // Yang line
                {
                    CheckBoxes[row, col].IsChecked = !isChanging; // Yang, not changing (9): Checked, Checked, Checked
                }
                else // Yin line
                {
                    CheckBoxes[row, col].IsChecked = isChanging; // Yin, not changing (6): Unchecked, Unchecked, Unchecked
                }
            }
        }
    }

    private void EvalAndSaveHexagram()
    {
        string question = rtQuestion.Text;
        var hexagram = new HG.Hexagram(new HG.Values().InitValues<CheckBox>(CheckBoxes, (checkBox, row, col) => checkBox.IsChecked));

        // Example logic to retrieve the main hexagram.
        int mainHexagram = hexagram.Current;
        var hexagramName = HexagramNameProvider.GetHexagramName(mainHexagram, HexagramNameProvider.Language.English);

        rtAnswer.Text = $"\nMain Hexagram {mainHexagram}: {hexagramName}\n\n";

        if (hexagram.ChangingLines.Any())
        {
            rtAnswer.Text += "Changing lines: ";
            var changingLines = hexagram.ChangingLines;
            changingLines.Reverse();
            foreach (int line in changingLines)
            {
                rtAnswer.Text += line + ", ";
            }
            var hexagramChangedName = HexagramNameProvider.GetHexagramName(hexagram.New, HexagramNameProvider.Language.English);

            rtAnswer.Text += $"\n\nChanging Hexagram {hexagram.New}: {hexagramChangedName}\n";
        }
        else
        {
            rtAnswer.Text += "\n No changing lines ";
        }

        // Save the question and hexagram result
        var jsonHandler = new JsonHandler();
        jsonHandler.SaveEntry(new HexagramEntry(
            question,
            rtAnswer.Text,
            hexagram.Current,
            hexagram.New
        ));

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
        _mainPage.Content = _mainPage.CVConfig;
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
        _mainPage.CVYarrowStalks.Question.Text = rtQuestion.Text;
        _mainPage.CVYarrowStalks.InitProcess();

        _mainPage.Content = _mainPage.CVYarrowStalks;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        //var task = _mainPage.DisplayAlert("Are you sure?", "Clear question and answer?", "Yes", "No");
        //task.Wait();
        //if (!task.Result)
        //    return;
        _mainPage.DisplayVersionText();
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
            await _mainPage.DisplayAlert("Error", "Failed to open web page: " + ex.Message, "OK");
        }
    }
    #endregion
}
