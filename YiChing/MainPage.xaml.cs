using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        private CvHexagram? _cvHexagram;
        private CvYarrowStalks? _cvYarrowStalks;
        private CvConfig? _cvConfig;

        public CvHexagram CVHexagram 
        { 
            get 
            {
                _cvHexagram ??= new(this);
                return _cvHexagram;
            }
        }
        
        public CvYarrowStalks CVYarrowStalks 
        { 
            get 
            {
                _cvYarrowStalks ??= new(this);
                return _cvYarrowStalks;
            }
        }
        
        public CvConfig CVConfig 
        { 
            get 
            {
                _cvConfig ??= new CvConfig(this, configuration);
                return _cvConfig;
            }
        }

        public Version version;
        private readonly IConfiguration configuration;

        public async Task<bool> ShowMessageBox(string title, string message, string accept = "OK", string? cancel = null)
        {
            bool result = await DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public MainPage(IConfiguration config)
        {
            InitializeComponent();
            configuration = config;
            version = typeof(App).Assembly.GetName().Version ?? new Version(1, 0);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    Content = CVHexagram;
                    DisplayVersionText();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error setting content: {ex}");
                }
            });
        }

        public void DisplayVersionText()
        {
            if (version != null && CVHexagram?.Answer != null)
            {
                CVHexagram.Answer.Text = "Version: " + version.ToString() + "\n\nWhat is the answer to the ultimate question of\nlife, the universe, and everything?";
            }
        }
    }
}
