using HexagramNS;
using HG = HexagramNS;
using YiChing.Configuration;
using System.Text;

namespace YiChing;

public partial class CvHexagram : ContentView
{
    #region Privates

    MainPage _mainPage;
    private void DrawHexagram()
    {
        for (int row = 0; row < HG.Hexagram.RowCount; row++)
        {
            Label labelCB = new Label
            {
                Text = $"{HG.Hexagram.RowCount - row}",
                WidthRequest = 20,
                HeightRequest = 20,
                VerticalTextAlignment = TextAlignment.Center
            };
            gridHexagram.Add(labelCB, gridHexagram.ColumnDefinitions.Count - 1, row + 1);
            // labelCB.WidthRequest = 20;

            if (gridHexagram != null)
            {
                for (int col = 0; col < HG.Hexagram.ColCount; col++)
                {
                    CheckBox checkBox = new CheckBox();
                    CheckBoxes[row, col] = checkBox;
                    checkBox.WidthRequest = 20;
                    checkBox.HeightRequest = 20;
                    gridHexagram.Add(checkBox, col + 1, row + 1);
                }
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

    private string GetTranslationRequestText() => _mainPage?.CVConfig?.Settings?.AnswerLanguage != "English" ? $"{_mainPage?.CVConfig?.Settings?.TranslationRequest} {_mainPage.CVConfig.Settings.AnswerLanguage}\n\n" : string.Empty;

    private string GetSystemText()
    {
        if (_mainPage?.CVConfig?.Settings == null) return string.Empty;

        var settings = _mainPage.CVConfig.Settings;
        return
            GetTranslationRequestText() +
            $"{settings.StepsHeader}\n\n" +
            $"{settings.OutputFormatHeader}\n\n" +
            $"{settings.NotesHeader}";
    }

    private string GetAnswerText()
    {
        if (_mainPage?.CVConfig?.Settings == null) return string.Empty;

        var settings = _mainPage.CVConfig.Settings;
        return
            $"Date: {DateTime.Now:yyyy-MM-dd}\n" +
            $"{settings.QuestionPrefix} {rtQuestion.Text}\n\n" + 
            rtAnswer.Text;
    }

    private string GetFullQuestion()
    {
        if (_mainPage != null)
        {
            var settings = _mainPage?.CVConfig?.Settings;
            if (settings == null) return string.Empty;
            return GetTranslationRequestText() +
                   $"Date: {DateTime.Now:yyyy-MM-dd}\n" +
                   $"{settings.QuestionPrefix} {rtQuestion.Text}\n\n" +
                   $"{settings.AnswerPrefix}\n\n" +
                   $"{rtAnswer.Text}\n\n" +
                   $"{settings.StepsHeader}\n\n" +
                   $"{settings.OutputFormatHeader}\n\n" +
                   $"{settings.NotesHeader}";
        }
        return string.Empty;
    }

    #endregion

    #region Constructor
    public CvHexagram(MainPage mainPage)
    {
        InitializeComponent();
        mainPage.Title = Title;
        
        // Set binding context to access settings
        this.BindingContext = mainPage.CVConfig?.Settings;

        btnCopy.Clicked += btnCopyAll_Click;
        btnCopyAnswer.Clicked += BtnCopyAnswer_Clicked;
        btnCopySystem.Clicked += BtnCopySystem_Clicked;

        btnClear.Clicked += btnClear_Click;
        btnYarrow.Clicked += btnYarrow_Click;
        btnConfig.Clicked += btnConfig_Click;
        btnOpenAI.Clicked += OnOpenAIClicked;

        rtQuestion.TextChanged += (sender, e) => { Question.Text = e.NewTextValue; };

        DrawHexagram();
        this._mainPage = mainPage;
        LoadHexagrams();
        
        // Initialize URL picker
        if (mainPage.CVConfig?.Settings != null)
        {
            aiUrlPicker.ItemsSource = mainPage.CVConfig.Settings.SavedUrls;
            aiUrlPicker.SelectedItem = mainPage.CVConfig.Settings.SelectedUrl;
        }
    }
    #endregion

    private void LoadHexagrams()
    {
        var jsonHandler = new JsonHandler();
        var hexagramEntries = jsonHandler.ReadHexagramEntriesFromJson();

        foreach (var entry in hexagramEntries)
        {
            hexagramPicker.Items.Add($"{entry.DisplayText}");
        }
    }

    public void FillCheckBoxesFromHexagramNumbers(int currentHexagramNumber, int newHexagramNumber)
    {
        try
        {
            var h = new HG.Hexagram(currentHexagramNumber, newHexagramNumber);
            FillCheckBoxes(h.Values);            
        }
        catch (ArgumentException e)
        {
            _mainPage.DisplayAlert("Error", $"Error: Invalid hexagram numbers: {e.Message}", "OK");
        }
        
    }

    private void OnHexagramSelected(object sender, EventArgs e)
    {
        if (hexagramPicker.SelectedItem != null)
        {
            var selectedHexagram = hexagramPicker.SelectedItem?.ToString();
            if (selectedHexagram == null) return;
            var jsonHandler = new JsonHandler();
            var hexagramDetails = jsonHandler.GetHexagramDetails(selectedHexagram!);

            if (hexagramDetails != null)
            {
                rtAnswer.Text = hexagramDetails.Answer;
                rtQuestion.Text = hexagramDetails.Question;
                FillCheckBoxesFromHexagramNumbers(hexagramDetails.CurrentHexagram, hexagramDetails.NewHexagram);
            }
        }
    }

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

    public CheckBox[,] CheckBoxes = new CheckBox[HG.Hexagram.RowCount, HG.Hexagram.ColCount];

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

        int mainHexagram = hexagram.Current;
        var hexagramName = HexagramNameProvider.GetHexagramName(mainHexagram, HexagramNameProvider.Language.English);

        StringBuilder answerText = new StringBuilder();
        answerText.AppendLine($"Main Hexagram {mainHexagram}: {hexagramName}");
        answerText.AppendLine();

        if (hexagram.ChangingLines.Any())
        {
            answerText.Append("Changing lines: ");
            var changingLines = hexagram.ChangingLines;
            changingLines.Reverse();
            foreach (int line in changingLines)
            {
                answerText.Append(line + ", ");
            }

            var hexagramChangedName = HexagramNameProvider.GetHexagramName(hexagram.New,
                HexagramNameProvider.Language.English);
            answerText.AppendLine(); // Ensure newline before next line
            answerText.AppendLine($"Changing Hexagram {hexagram.New}: {hexagramChangedName}");
        }
        else
        {
            answerText.AppendLine("No changing lines");
        }

        rtAnswer.Text = answerText.ToString(); // Set the complete answer text

        var jsonHandler = new JsonHandler();
        jsonHandler.SaveEntry(new HexagramEntry(
            question,
            rtAnswer.Text,
            hexagram.Current,
            hexagram.New
        ));

        RefreshHexagramPicker();
    }

    private void RefreshHexagramPicker()
    {
        hexagramPicker.Items.Clear();
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

    private void btnCopyAll_Click(object sender, EventArgs e)
    {
        EvalAndSaveHexagram(); // Make sure answer is up-to-date
        string full = GetFullQuestion();
        Clipboard.SetTextAsync(full);
    }

    private void BtnCopyAnswer_Clicked(object sender, EventArgs e)
    {
        EvalAndSaveHexagram(); // Ensure rtAnswer.Text is current
        string answerText = GetAnswerText();
        Clipboard.SetTextAsync(answerText);
    }

    private void BtnCopySystem_Clicked(object sender, EventArgs e)
    {
        string systemText = GetSystemText();
        Clipboard.SetTextAsync(systemText);
    }

    private void btnYarrow_Click(object sender, EventArgs e)
    {
        _mainPage.CVYarrowStalks.Question.Text = rtQuestion.Text;
        _mainPage.CVYarrowStalks.InitProcess();
        _mainPage.Content = _mainPage.CVYarrowStalks;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        _mainPage.DisplayVersionText();
        rtQuestion.Text = string.Empty;
        ResetIndeterminate();
    }

    private async void OnOpenAIClicked(object sender, System.EventArgs e)
    {
        var selectedUrl = aiUrlPicker.SelectedItem as string;
        if (!string.IsNullOrEmpty(selectedUrl))
        {
            try
            {
                await Launcher.OpenAsync(selectedUrl);
            }
            catch (Exception ex)
            {
                await _mainPage.DisplayAlert("Error", $"Could not open URL: {ex.Message}", "OK");
            }
        }
        else
        {
            await _mainPage.DisplayAlert("No URL", "Please select an AI URL from the dropdown", "OK");
        }
    }

    #endregion
}
