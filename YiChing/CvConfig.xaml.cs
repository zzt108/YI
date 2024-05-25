using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace YiChing;

public partial class CvConfig : ContentView
{
    private readonly IConfiguration _configuration;
    private readonly MainPage _mainPage;

    public Settings Settings { get; set; }
    public Settings Defaults { get; set; }

    public CvConfig(MainPage page, IConfiguration configuration)
    {
        InitializeComponent();
        _mainPage = page;
        _configuration = configuration;
        btnReturn.Clicked += btnReturn_Click;
        myPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
        LoadSettings();
        BindingContext = this;
        myPicker.SelectedItem = Settings.AnswerLanguage;
    }
    private void LoadSettings()
    {
        //Defaults = _configuration?.GetRequiredSection("Settings")?.Get<Settings>();
        Settings = new Settings(Defaults);
    }

    private void btnReturn_Click(object? sender, EventArgs e)
    {
        Settings.SaveValues();
        _mainPage.Content = _mainPage.CVHexagram;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedItem = picker.SelectedItem;
        txtAnswerLanguage.Text = selectedItem?.ToString();
        Settings.AnswerLanguage = selectedItem?.ToString();

        //// Perform actions based on the selected item
        //if (selectedItem != null)
        //{
        //    _mainPage.DisplayAlert("Selection", $"You selected: {selectedItem}", "OK");
        //}
    }

}