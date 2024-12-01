using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Web;
using YiChing.ViewModels;
using YiChing.Services;
using HG = HexagramNS;

namespace YiChing;

public partial class CvHexagram : ContentPage
{
    #region Privates

    private readonly ILogger<CvHexagram> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IConfiguration configuration;
    private readonly IAlertService _alertService;
    private readonly INavigationService _navigationService;
    private readonly HexagramViewModel _viewModel;
    private HG.Values _values;
    private CheckBox[,] CheckBoxes = new CheckBox[HG.Hexagram.RowCount, HG.Hexagram.ColCount];

    #endregion

    public CvHexagram(
        ILogger<CvHexagram> logger, 
        ILoggerFactory loggerFactory, 
        IConfiguration configuration, 
        IAlertService alertService, 
        INavigationService navigationService,
        MainPage mainPage)
    {
        _logger = logger;
        _loggerFactory = loggerFactory;
        this.configuration = configuration;
        this._alertService = alertService;
        this._navigationService = navigationService;

        // Initialize Values
        _values = new HG.Values();

        InitializeComponent();

        // Initialize ViewModel
        _viewModel = new HexagramViewModel(
            new JsonHandler(_loggerFactory.CreateLogger<JsonHandler>(), configuration), 
            _loggerFactory.CreateLogger<HexagramViewModel>(),
            _alertService,
            navigationService,
            mainPage,
            configuration);
        BindingContext = _viewModel;

        // Dynamically create CheckBoxes
        for (int row = 0; row < HG.Hexagram.RowCount; row++)
        {
            for (int col = 0; col < HG.Hexagram.ColCount; col++)
            {
                var checkBox = new CheckBox
                {
                    IsChecked = false
                };
                gridHexagram.Add(checkBox, col + 1, row + 1);
                _values.SetValue(row, col, false);
                CheckBoxes[row, col] = checkBox;
            }
        }
    }

    private void DrawHexagram()
    {
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
                CheckBox checkBox = CheckBoxes[row, col];
                checkBox.WidthRequest = 40;

                gridHexagram.Add(checkBox, col + 1, row + 1);
            }
        }
    }

    private void ResetIndeterminate()
    {
        for (int row = 0; row < HG.Hexagram.RowCount; row++)
        {
            for (int col = 0; col < HG.Hexagram.ColCount; col++)
            {
                CheckBox checkBox = CheckBoxes[row, col];
                checkBox.IsChecked = false;
                _values.SetValue(row, col, false);
            }
        }
    }

    private string GetFullQuestion()
    {
        // Null checks for _viewModel and its properties
        if (_viewModel == null)
        {
            _logger?.LogWarning("ViewModel is null in GetFullQuestion");
            return string.Empty;
        }

        // Initialize the values based on CheckBox states
        for (int row = 0; row < HG.Hexagram.RowCount; row++)
        {
            for (int col = 0; col < HG.Hexagram.ColCount; col++)
            {
                _values.SetValue(row, col, CheckBoxes[row, col].IsChecked);
            }
        }

        string question = _viewModel.RtQuestion?.Text ?? string.Empty;
        var hexagram = new HG.Hexagram(_values);

        // Example logic to retrieve the main hexagram.
        int mainHexagram = hexagram.Main;

        // Null checks for RtAnswer
        if (_viewModel.RtAnswer == null)
        {
            _logger?.LogWarning("RtAnswer is null in GetFullQuestion");
            return string.Empty;
        }

        _viewModel.RtAnswer.Text = $"\nMain Hexagram {mainHexagram}\n\n";

        if (hexagram.ChangingLines?.Any() == true)
        {
            _viewModel.RtAnswer.Text += "Changing lines: ";
            foreach (int line in hexagram.ChangingLines)
            {
                _viewModel.RtAnswer.Text += line + ", ";
            }
            _viewModel.RtAnswer.Text += $"\n\nChanging Hexagram {hexagram.Changed}\n";
        }
        else
        {
            _viewModel.RtAnswer.Text += "\n No changing lines ";
        }

        // Null checks for JsonHandler dependencies
        var jsonHandler = new JsonHandler(
            _loggerFactory?.CreateLogger<JsonHandler>() ?? NullLogger<JsonHandler>.Instance, 
            configuration ?? new ConfigurationBuilder().Build()
        );

        // Null-safe saving of entry
        try 
        {
            jsonHandler.SaveEntry(new HexagramEntry(question, _viewModel.RtAnswer.Text));
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error saving hexagram entry");
        }

        // Null-safe refresh of hexagram picker
        try 
        {
            RefreshHexagramPicker();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error refreshing hexagram picker");
        }

        return $"{DateTime.Now:yyyy-MM-dd}\nQuestion to I Ching:\n {question}\n"
            + $"\nI Ching answered:\n{_viewModel.RtAnswer?.Text ?? string.Empty}\nWould you please interpret?\n\nPlease translate to {_viewModel.Settings?.AnswerLanguage ?? "default language"}.";
    }

    private void EvalAndSaveHexagram()
    {
        // Null checks for _viewModel and its properties
        if (_viewModel?.RtQuestion == null)
        {
            // Optionally log or handle the case where RtQuestion is null
            return;
        }

        string question = _viewModel.RtQuestion.Text;
        var hexagram = new HG.Hexagram(_values);

        // Example logic to retrieve the main hexagram.
        int mainHexagram = hexagram.Main;

        // Null checks for RtAnswer
        if (_viewModel?.RtAnswer == null)
        {
            // Optionally log or handle the case where RtAnswer is null
            return;
        }

        _viewModel.RtAnswer.Text = $"\nMain Hexagram {mainHexagram}\n\n";

        if (hexagram.ChangingLines.Any())
        {
            _viewModel.RtAnswer.Text += "Changing lines: ";
            foreach (int line in hexagram.ChangingLines)
            {
                _viewModel.RtAnswer.Text += line + ", ";
            }
            _viewModel.RtAnswer.Text += $"\n\nChanging Hexagram {hexagram.Changed}\n";
        }
        else
        {
            _viewModel.RtAnswer.Text += "\n No changing lines ";
        }

        // Null checks for JsonHandler dependencies
        var jsonHandler = new JsonHandler(
            _loggerFactory?.CreateLogger<JsonHandler>() ?? NullLogger<JsonHandler>.Instance, 
            configuration ?? new ConfigurationBuilder().Build()
        );

        // Null-safe saving of entry
        try 
        {
            jsonHandler.SaveEntry(new HexagramEntry(question, _viewModel.RtAnswer.Text));
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error saving hexagram entry");
        }

        // Null-safe refresh of hexagram picker
        try 
        {
            RefreshHexagramPicker();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error refreshing hexagram picker");
        }
    }

    private void RefreshHexagramPicker()
    {
        // Instead of clearing a non-existent hexagramPicker, 
        // reload hexagram entries in the ViewModel
        _viewModel.HexagramEntries = LoadHexagrams();
    }

    private List<HexagramEntry> LoadHexagrams()
    {
        var jsonHandler = new JsonHandler(_loggerFactory.CreateLogger<JsonHandler>(), configuration);
        return jsonHandler.ReadHexagramEntriesFromJson();
    }

    #region EventHandlers

    // private void btnConfig_Click(object? sender, EventArgs e)
    // {
    //     _viewModel.NavigateToConfig();
    // }

    // private void btnEval_Click(object? sender, EventArgs e)
    // {
    //     EvalAndSaveHexagram();
    // }

    // private void btnCopy_Click(object sender, EventArgs e)
    // {
    //     EvalAndSaveHexagram();
    //     string full = GetFullQuestion();
    //     Clipboard.SetTextAsync(full);
    // }

    private async void btnYarrow_Click(object sender, EventArgs e)
    {
        try
        {
            // Add a retry mechanism with a small delay
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    await _navigationService.NavigateToAsync("CvYarrowStalks");
                    _logger.LogInformation("Navigating to Yarrow Stalks page");
                    return; // Exit if navigation is successful
                }
                catch (InvalidOperationException)
                {
                    // Wait a short time before retrying
                    await Task.Delay(100);
                }
            }

            // If all attempts fail, show an alert
            await _alertService.DisplayAlert("Error", "Could not open Yarrow Stalks page", "OK");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error navigating to Yarrow Stalks page");
            await _alertService.DisplayAlert("Error", "An unexpected error occurred", "OK");
            throw;
        }
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
            // Use the new ShowAlert method from ViewModel
            await _viewModel.ShowAlert("Error", "Failed to open web page: " + ex.Message, "OK");
        }
    }

    public new string Title { get; set; } = "Yi Ching for AI by Gerzson";

    public HexagramViewModel ViewModel => _viewModel;

    public void FillCheckBoxes(HG.Values values)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            System.Diagnostics.Debug.WriteLine("FillCheckBoxes called on Main Thread");
            System.Console.WriteLine("FillCheckBoxes called on Main Thread");

            for (int row = 0; row < HG.Hexagram.RowCount; row++)
            {
                for (int col = 0; col < HG.Hexagram.ColCount; col++)
                {
                    CheckBox checkBox = CheckBoxes[row, col];
                    bool value = values.GetValue(row, col);

                    // More diagnostic logging
                    System.Diagnostics.Debug.WriteLine($"Setting CheckBox - Row: {row}, Col: {col}, Value: {value}");
                    System.Console.WriteLine($"Setting CheckBox - Row: {row}, Col: {col}, Value: {value}");

                    checkBox.IsChecked = value;
                }
            }
        });
    }
}

#endregion