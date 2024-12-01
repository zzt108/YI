using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Windows;
using YiChing.Models;
using YiChing.Services;

namespace YiChing.ViewModels
{
    public partial class HexagramViewModel : ObservableObject
    {
        private readonly IJsonHandler _jsonHandler;
        private readonly ILogger<HexagramViewModel> _logger;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private Editor rtQuestion;

        [ObservableProperty]
        private Editor rtAnswer;

        [ObservableProperty]
        private List<HexagramEntry> hexagramEntries;

        [ObservableProperty]
        private HexagramEntry selectedHexagram;

        private string _question;
        private string _answer;

        public string Question 
        { 
            get => _question; 
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
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        private void OnQuestionChanged()
        {
            if (RtQuestion != null && RtQuestion.Text != Question)
            {
                RtQuestion.Text = Question;
            }
        }

        partial void OnRtQuestionChanged(Editor value)
        {
            if (value != null)
            {
                value.TextChanged += (s, e) => Question = e.NewTextValue;
            }
        }

        partial void OnRtAnswerChanged(Editor value)
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
            IAlertService alertService)
        {
            _jsonHandler = jsonHandler;
            _logger = logger;
            _alertService = alertService;
            Settings = new AppSettings(); // Initialize with default settings
            LoadHexagrams();
        }

        partial void OnSelectedHexagramChanged(HexagramEntry value)
        {
            if (value != null)
            {
                RtQuestion.Text = value.Question;
                RtAnswer.Text = value.Answer;
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
            RtQuestion.Text = string.Empty;
            RtAnswer.Text = string.Empty;
            SelectedHexagram = null;
        }

        [RelayCommand]
        private void YarrowStalks()
        {
            // TODO: Implement yarrow stalks calculation
            _logger.LogInformation("Yarrow stalks calculation requested");
        }

        [RelayCommand]
        private void ShowConfiguration()
        {
            // TODO: Show configuration dialog
            _logger.LogInformation("Configuration requested");
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
            $"{DateTime.Now:yyyy-MM-dd}\nQuestion to I Ching:\n {RtQuestion.Text}\n" +
            $"\nI Ching answered:\n{RtAnswer.Text}";
    }
}