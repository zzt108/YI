using Microsoft.Extensions.Configuration;

namespace YiChing;

public partial class CvConfig : ContentPage
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
        btnReturn.Clicked += btnReturn_Click;
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        myPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        LoadSettings();
        BindingContext = this;
        myPicker.SelectedItem = Settings?.AnswerLanguage;
    }

    private async void btnReturn_Click(object? sender, EventArgs e)
    {
        Settings?.SaveValues();
        await Shell.Current.GoToAsync("/CvHexagram");
    }

    private void LoadSettings()
    {
        //Defaults = _configuration?.GetRequiredSection("Settings")?.Get<Settings>();
        Settings = new Settings(Defaults);
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedItem = picker.SelectedItem;
        txtAnswerLanguage.Text = selectedItem?.ToString() ?? string.Empty;
        
        // Add null check for Settings
        if (Settings != null)
        {
            Settings.AnswerLanguage = selectedItem?.ToString() ?? string.Empty;
        }
        //// Perform actions based on the selected item
        //if (selectedItem != null)
        //{
        //    _mainPage.DisplayAlert("Selection", $"You selected: {selectedItem}", "OK");
        //}
    }

}