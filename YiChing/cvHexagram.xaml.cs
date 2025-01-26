using HexagramNS;
using HG = HexagramNS;
using YiChing.Configuration;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

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
                WidthRequest = 40,
                VerticalTextAlignment = TextAlignment.Center
            };
            gridHexagram.Add(labelCB, gridHexagram.ColumnDefinitions.Count - 1, row + 1);
            labelCB.WidthRequest = 40;

            if (gridHexagram != null)
            {
                for (int col = 0; col < HG.Hexagram.ColCount; col++)
                {
                    CheckBox checkBox = new CheckBox();
                    CheckBoxes[row, col] = checkBox;
                    checkBox.WidthRequest = 40;
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

    private string GetFullQuestion()
    {
        if (_mainPage != null)
        {
            var settings = _mainPage?.CVConfig?.Settings;
            if (settings == null) return string.Empty;
            return $"{settings.TranslationRequest} {settings.AnswerLanguage} and provide an interpretation of the result.\n\n" +
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
        if (mainPage.CVConfig?.Settings == null)
        {
            Debug.WriteLine("Warning: Settings not initialized - using default values");
            var defaultSettings = new Settings();
            this.BindingContext = defaultSettings;
        }
        else
        {
            this.BindingContext = mainPage.CVConfig.Settings;
        }

        btnCopy.Clicked += btnCopy_Click;
        btnClear.Clicked += btnClear_Click;
        btnYarrow.Clicked += btnYarrow_Click;
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
        var hexagramEntries = jsonHandler.ReadHexagramEntriesFromJson();

        foreach (var entry in hexagramEntries)
        {
            hexagramPicker.Items.Add($"{entry.DisplayText}");
        }
    }

    public void FillCheckBoxesFromHexagramNumbers(int currentHexagramNumber, int newHexagramNumber)
    {
        var h = new HG.Hexagram(currentHexagramNumber, newHexagramNumber);
        FillCheckBoxes(h.Values);
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

    private void btnCopy_Click(object sender, EventArgs e)
    {
        EvalAndSaveHexagram();
        string full = GetFullQuestion();
        Clipboard.SetTextAsync(full);
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

    #endregion

    #region Commands

    public ICommand OpenAICommand => new Command(async () =>
    {
        var settings = _mainPage?.CVConfig?.Settings;
        if (settings?.SelectedAIUrl == null)
        {
            await Shell.Current.DisplayAlert("No URL", "Please select an AI URL from the dropdown", "OK");
            return;
        }

        try
        {
            await Launcher.OpenAsync(settings.SelectedAIUrl);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Could not open URL: {ex.Message}", "OK");
        }
    });

    #endregion
}
