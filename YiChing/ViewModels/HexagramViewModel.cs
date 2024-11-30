using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace YiChing.ViewModels
{
    public partial class HexagramViewModel : ObservableObject
    {
        private readonly IJsonHandler _jsonHandler;
        private readonly ILogger<HexagramViewModel> _logger;

        [ObservableProperty]
        private string question;

        [ObservableProperty]
        private string answer;

        [ObservableProperty]
        private List<HexagramEntry> hexagramEntries;

        [ObservableProperty]
        private HexagramEntry selectedHexagram;

        public HexagramViewModel(IJsonHandler jsonHandler, ILogger<HexagramViewModel> logger)
        {
            _jsonHandler = jsonHandler;
            _logger = logger;
            LoadHexagrams();
        }

        partial void OnSelectedHexagramChanged(HexagramEntry value)
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
            Question = string.Empty;
            Answer = string.Empty;
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
                await Application.Current.Clipboard.SetTextAsync(GetFullQuestion());
                _logger.LogInformation("Text copied to clipboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error copying to clipboard");
            }
        }

        private string GetFullQuestion() =>
            $"{DateTime.Now:yyyy-MM-dd}\nQuestion to I Ching:\n {Question}\n" +
            $"\nI Ching answered:\n{Answer}";
    }
}