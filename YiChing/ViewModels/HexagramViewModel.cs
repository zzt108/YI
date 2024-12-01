using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using YiChing.Models;
using YiChing.Services;

namespace YiChing.ViewModels
{
    public partial class HexagramViewModel : ObservableObject
    {
        private readonly IJsonHandler _jsonHandler;
        private readonly ILogger<HexagramViewModel> _logger;
        private readonly IAlertService _alertService;
        private readonly INavigationService _navigationService;
        private readonly MainPage _mainPage;
        private readonly IConfiguration _configuration;

        [ObservableProperty]
        private Editor? _rtQuestion;

        [ObservableProperty]
        private Editor? _rtAnswer;

        [ObservableProperty]
        private List<HexagramEntry>? _hexagramEntries;

        [ObservableProperty]
        private HexagramEntry? _selectedHexagram;

        private string? _question;
        private string? _answer;

        public string Question 
        { 
            get => _question ?? string.Empty; 
            set
            {
                if (SetProperty(ref _question, value))
                {
                    OnQuestionChanged();
                }
            }
        }

        public string Answer
        {
            get => _answer ?? string.Empty;
            set => SetProperty(ref _answer, value);
        }

        private void OnQuestionChanged()
        {
            if (RtQuestion != null && RtQuestion.Text != Question)
            {
                RtQuestion.Text = Question;
            }
        }

        partial void OnRtQuestionChanged(Editor? value)
        {
            if (value != null)
            {
                value.TextChanged += (s, e) => Question = e.NewTextValue;
            }
        }

        partial void OnRtAnswerChanged(Editor? value)
        {
            if (value != null)
            {
                value.TextChanged += (s, e) => Answer = e.NewTextValue;
            }
        }

        public AppSettings Settings { get; private set; }

        public HexagramViewModel(
            IJsonHandler jsonHandler, 
            ILogger<HexagramViewModel> logger,
            IAlertService alertService,
            INavigationService navigationService,
            MainPage mainPage,
            IConfiguration configuration)
        {
            _jsonHandler = jsonHandler;
            _logger = logger;
            _alertService = alertService;
            _navigationService = navigationService;
            _mainPage = mainPage;
            _configuration = configuration;
            Settings = new AppSettings(); // Initialize with default settings
            _hexagramEntries = new List<HexagramEntry>();
            _question = string.Empty;
            _answer = string.Empty;
            LoadHexagrams();
        }

        partial void OnSelectedHexagramChanged(HexagramEntry? value)
        {
            if (value != null)
            {
                Question = value.Question;
                Answer = value.Answer;
            }
        }

        [RelayCommand]
        private void LoadHexagrams()
        {
            try
            {
                HexagramEntries = _jsonHandler.ReadHexagramEntriesFromJson();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading hexagrams");
            }
        }

        [RelayCommand]
        private void Clear()
        {
            if (RtQuestion != null)
            {
                RtQuestion.Text = string.Empty;
            }
            if (RtAnswer != null)
            {
                RtAnswer.Text = string.Empty;
            }
            SelectedHexagram = null;
        }

        [RelayCommand]
        private async Task YarrowStalks()
        {
            try
            {
                await _navigationService.NavigateToAsync("CvYarrowStalks");
                _logger.LogInformation("Navigating to Yarrow Stalks page");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error navigating to Yarrow Stalks page");
                await _alertService.DisplayAlert("Error", "Could not open Yarrow Stalks page", "OK");
                throw;
            }
        }

        [RelayCommand]
        private async Task ShowConfiguration()
        {
            try 
            {
                // Attempt navigation to Configuration page
                var configPage = Shell.Current.CurrentPage.Navigation.NavigationStack
                    .OfType<CvConfig>()
                    .FirstOrDefault();

                if (configPage == null)
                {
                    // If no existing config page, create a new one and set dependencies
                    configPage = new CvConfig();
                    configPage.SetDependencies(_mainPage, _configuration);
                }

                await Shell.Current.GoToAsync("CvConfig");
                
                _logger.LogInformation("Successfully navigated to Configuration page");
                System.Diagnostics.Debug.WriteLine("Successfully navigated to Configuration page");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error navigating to Configuration page: {ex.Message}";
                _logger.LogError(ex, errorMsg);
                System.Diagnostics.Debug.WriteLine(errorMsg);
                
                await _alertService.DisplayAlert("Navigation Error", 
                    $"Could not open Configuration page: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task CopyToClipboard()
        {
            try
            {
                await Clipboard.SetTextAsync(GetFullQuestion());
                _logger.LogInformation("Text copied to clipboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error copying to clipboard");
            }
        }

        public async Task ShowAlert(string title, string message, string cancel)
        {
            await _alertService.DisplayAlert(title, message, cancel);
        }

        private string GetFullQuestion() =>
            $"{DateTime.Now:yyyy-MM-dd}\nQuestion to I Ching:\n {RtQuestion?.Text ?? string.Empty}\n" +
            $"\nI Ching answered:\n{RtAnswer?.Text ?? string.Empty}";
    }
}