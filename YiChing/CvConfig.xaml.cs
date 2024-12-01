using Microsoft.Extensions.Configuration;
using YiChing.Services;
using YiChing.Models;

namespace YiChing;

public partial class CvConfig : ContentPage
{
    private IConfiguration? _configuration;
    private MainPage? _mainPage;
    private IAlertService? _alertService;

    public Settings? Settings { get; set; }
    public Settings? Defaults { get; set; }

    // Parameterless constructor required for Shell navigation
    public CvConfig()
    {
        InitializeComponent();
    }

    // Dependency injection constructor
    public CvConfig(MainPage? page, IConfiguration? configuration, IAlertService? alertService = null)
    {
        // Null checks for critical dependencies
        ArgumentNullException.ThrowIfNull(page, nameof(page));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        InitializeComponent();
        _mainPage = page;
        _configuration = configuration;
        _alertService = alertService;

        LoadSettings();
        BindingContext = this;

        // Null-safe picker item selection
        if (myPicker != null && Settings?.AnswerLanguage != null)
        {
            myPicker.SelectedItem = Settings.AnswerLanguage;
        }

        // Null-safe event binding with additional logging
        if (btnReturn != null)
        {
            btnReturn.Clicked += OnReturnClicked;
            System.Diagnostics.Debug.WriteLine("CvConfig Constructor: btnReturn event handler ADDED");
        }
        else 
        {
            System.Diagnostics.Debug.WriteLine("CvConfig Constructor: btnReturn is null, cannot add event handler");
        }

        // Verbose logging for event binding
        System.Diagnostics.Debug.WriteLine($"CvConfig Constructor: myPicker is {(myPicker != null ? "NOT null" : "null")}");
        
        // Null-safe event binding with additional logging
        if (myPicker != null)
        {
            myPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            System.Diagnostics.Debug.WriteLine("CvConfig Constructor: myPicker event handler added");
        }
        else 
        {
            System.Diagnostics.Debug.WriteLine("CvConfig Constructor: myPicker is null, cannot add event handler");
        }

        // Null-safe event binding with additional logging
        if (btnSave != null)
        {
            btnSave.Clicked += OnSaveClicked;
            System.Diagnostics.Debug.WriteLine("CvConfig Constructor: btnSave event handler ADDED");
        }
        else 
        {
            System.Diagnostics.Debug.WriteLine("CvConfig Constructor: btnSave is null, cannot add event handler");
        }

        // Final constructor logging
        System.Diagnostics.Debug.WriteLine("CvConfig Constructor: COMPLETED");
    }

    // Method to set dependencies after shell navigation
    public void SetDependencies(MainPage? page, IConfiguration? configuration, IAlertService? alertService = null)
    {
        // Null checks for critical dependencies
        ArgumentNullException.ThrowIfNull(page, nameof(page));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        _mainPage = page;
        _configuration = configuration;
        _alertService = alertService;

        LoadSettings();
        BindingContext = this;

        // Null-safe picker item selection
        if (myPicker != null && Settings?.AnswerLanguage != null)
        {
            myPicker.SelectedItem = Settings.AnswerLanguage;
        }
    }

    private async void OnReturnClicked(object sender, EventArgs e)
    {
        try 
        {
            // Log the event
            System.Diagnostics.Debug.WriteLine("OnReturnClicked: Method ENTERED");

            // Null-safe save of settings
            Settings?.SaveValues();

            // Explicitly check for Shell and navigation
            if (Shell.Current == null)
            {
                System.Diagnostics.Debug.WriteLine("CRITICAL: Shell.Current is NULL");
                await DisplayAlert("Navigation Error", "Shell is not initialized", "OK");
                return;
            }

            // Log current navigation context
            System.Diagnostics.Debug.WriteLine($"Current Shell: {Shell.Current}");
            System.Diagnostics.Debug.WriteLine($"Current Page: {Shell.Current.CurrentPage?.GetType().Name}");

            // Attempt navigation using the correct route name
            await Shell.Current.GoToAsync("///MainPage");
            
            System.Diagnostics.Debug.WriteLine("Navigation to MainPage successful");
        }
        catch (Exception ex)
        {
            // Log and display any navigation errors
            System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            
            await DisplayAlert("Navigation Error", 
                $"Could not return to Main page: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try 
        {
            // Save settings
            Settings?.SaveValues();
            
            // Show success message
            await DisplayAlert("Success", "Settings saved successfully", "OK");
        }
        catch (Exception ex)
        {
            // Log and display any errors
            System.Diagnostics.Debug.WriteLine($"Settings save error: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            
            await DisplayAlert("Error", 
                $"Could not save settings: {ex.Message}", "OK");
        }
    }

    private void LoadSettings()
    {
        // Try to load defaults from configuration, fallback to null
        try 
        {
            Defaults = _configuration?.GetSection("Settings")?.Get<Settings>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
            Defaults = null;
        }

        // Always create a new Settings instance, using Defaults if available
        Settings = new Settings(Defaults ?? new Settings());
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Null-safe handling of picker selection
        if (sender is Picker picker && picker.SelectedItem is string selectedItem)
        {
            // Update settings
            if (Settings != null)
            {
                Settings.AnswerLanguage = selectedItem;
            }
        }
    }
}