using Microsoft.Extensions.Configuration;
using HG = HexagramNS;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        public CvHexagram CVHexagram;
        public CvYarrowStalks CVYarrowStalks;
        public CvConfig CVConfig;

        public Version version;
        
        private IConfiguration configuration;

        public async Task<bool> ShowMessageBox(string title, string message, string accept = "OK", string? cancel = null )
        {
            bool result = await DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public MainPage(IConfiguration config)
        {
            InitializeComponent();
            configuration = config;

            version = typeof(App).Assembly.GetName().Version;

            CVHexagram = new(this);
            CVHexagram.Answer.Text = "Version: " + version.ToString() + "\n\nWhat is the answer to the ultimate question of\nlife, the universe, and everything?";
            CVYarrowStalks = new(this);
            CVConfig = new CvConfig(this, config);

            // Set the content of the page
            Content = CVHexagram;
            //Content = CVConfig;
        }
    }
}
