using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace YiChing.Configuration;

public partial class CvConfig : ContentView
{
    private readonly IConfiguration _configuration;
    private readonly MainPage _mainPage;

    public Settings? Settings { get; set; }
    public Settings? Defaults { get; set; }

    public CvConfig(MainPage page, IConfiguration configuration)
    {
        InitializeComponent();
        _mainPage = page;
        _configuration = configuration;
        
        // Existing event handlers
        btnReturn.Clicked += btnReturn_Click;
        btnReset.Clicked += btnReset_Click;
#pragma warning disable CS8622
        myPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
#pragma warning restore CS8622
        
        // New URL event handlers
        btnAddUrl.Clicked += AddUrl_Click;
        btnRemoveUrl.Clicked += RemoveUrl_Click;
        btnOpenUrl.Clicked += OpenUrl_Click;
        
        LoadSettings();
        BindingContext = this;
        
        if (Settings != null)
        {
            myPicker.SelectedItem = Settings.AnswerLanguage ?? "English";
        }
        else
        {
            myPicker.SelectedItem = "English";
        }
    }

    private void LoadSettings()
    {
        Settings = new Settings(Defaults);
    }

    private void btnReturn_Click(object? sender, EventArgs e)
    {
        Settings?.SaveValues();
        _mainPage.Content = _mainPage.CVHexagram;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedItem = picker.SelectedItem;
        txtAnswerLanguage.Text = selectedItem?.ToString() ?? string.Empty;

        if (Settings != null)
        {
            Settings.AnswerLanguage = selectedItem?.ToString();
        }
    }

    private void btnReset_Click(object? sender, EventArgs e)
    {
        var defaultSettings = new Settings();

        if (Settings != null)
        {
            Settings.AnswerLanguage = defaultSettings.AnswerLanguage;
            Settings.QuestionPrefix = defaultSettings.QuestionPrefix;
            Settings.AnswerPrefix = defaultSettings.AnswerPrefix;
            Settings.TranslationRequest = defaultSettings.TranslationRequest;
            Settings.StepsHeader = defaultSettings.StepsHeader;
            Settings.OutputFormatHeader = defaultSettings.OutputFormatHeader;
            Settings.NotesHeader = defaultSettings.NotesHeader;
            
            // Reset URLs
            Settings.SavedUrls = new ObservableCollection<string>(
                JsonSerializer.Deserialize<string[]>(DefaultTexts.DEFAULT_URLS) ?? Array.Empty<string>()
            );
            Settings.SelectedUrl = Settings.SavedUrls.FirstOrDefault() ?? string.Empty;

            myPicker.SelectedItem = Settings.AnswerLanguage;
            Settings.SaveValues();
        }
    }

    private void AddUrl_Click(object? sender, EventArgs e)
    {
        var newUrl = txtNewUrl.Text?.Trim();
        if (string.IsNullOrWhiteSpace(newUrl))
        {
            return;
        }

        if (!Uri.TryCreate(newUrl, UriKind.Absolute, out _))
        {
            // Show error message
            return;
        }

        if (Settings != null && !Settings.SavedUrls.Contains(newUrl))
        {
            Settings.SavedUrls.Add(newUrl);
            txtNewUrl.Text = string.Empty;
        }
    }

    private void RemoveUrl_Click(object? sender, EventArgs e)
    {
        if (Settings != null && Settings.SelectedUrl != null)
        {
            Settings.SavedUrls.Remove(Settings.SelectedUrl);
            Settings.SelectedUrl = Settings.SavedUrls.FirstOrDefault() ?? string.Empty;
        }
    }

    private void OpenUrl_Click(object? sender, EventArgs e)
    {
        if (Settings != null && !string.IsNullOrWhiteSpace(Settings.SelectedUrl))
        {
            try
            {
                var uri = new Uri(Settings.SelectedUrl);
                Launcher.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error opening URL: {ex.Message}");
            }
        }
    }
}
