using Microsoft.Extensions.Configuration;
using YiChing.Configuration;
using System.Diagnostics;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        private CvHexagram? _cvHexagram;
        private CvYarrowStalks? _cvYarrowStalks;
        private CvConfig? _cvConfig;
        private readonly Settings _settings;

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
                _cvConfig ??= new CvConfig(this, _configuration);
                return _cvConfig;
            }
        }

        public Version version;
        private readonly IConfiguration _configuration;

        public MainPage(IConfiguration config)
        {
            InitializeComponent();
            _configuration = config;
            _settings = new Settings();
            Debug.WriteLine($"Settings initialized with {_settings.SavedUrls.Count} URLs");
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
                    Debug.WriteLine($"Error setting content: {ex}");
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
