using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace YiChing;

public partial class CvConfig : ContentView
{
    private readonly IConfiguration _configuration;
    public Settings Settings { get; set; }

    public CvConfig(IConfiguration configuration)
    {
        InitializeComponent();

        _configuration = configuration;
        BindingContext = this;
        LoadSettings();
    }
    private void LoadSettings()
    {
        Settings = _configuration.GetRequiredSection("Settings").Get<Settings>();
    }
}