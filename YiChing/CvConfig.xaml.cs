using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace YiChing;

public partial class CvConfig : ContentView
{
    private readonly IConfiguration _configuration;
    private readonly MainPage _mainPage;

    public Settings Settings { get; set; }

    public CvConfig(MainPage page, IConfiguration configuration)
    {
        InitializeComponent();
        _mainPage = page;
        _configuration = configuration;
        btnReturn.Clicked += btnReturn_Click;
        BindingContext = this;
        LoadSettings();
    }
    private void LoadSettings()
    {
        Settings = _configuration.GetRequiredSection("Settings").Get<Settings>();
    }

    private void btnReturn_Click(object? sender, EventArgs e)
    {
        _mainPage.Content = _mainPage.CVHexagram;
    }

}