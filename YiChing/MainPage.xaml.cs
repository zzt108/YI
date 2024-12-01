using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using YiChing.Services;
using YiChing.ViewModels;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        public CvHexagram CVHexagram;
        public CvYarrowStalks CVYarrowStalks;
        public CvConfig CVConfig;

        public Version version = null!;
        
        private IConfiguration configuration;

        public async Task<bool> ShowMessageBox(string title, string message, string accept = "OK", string? cancel = null)
        {
            bool result = await DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public MainPage(IConfiguration config, ILoggerFactory loggerFactory, IAlertService alertService, IJsonHandler jsonHandler)
        {
            // Null checks for critical dependencies
            ArgumentNullException.ThrowIfNull(config, nameof(config));
            ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
            ArgumentNullException.ThrowIfNull(alertService, nameof(alertService));
            ArgumentNullException.ThrowIfNull(jsonHandler, nameof(jsonHandler));

            InitializeComponent();
            configuration = config;

            version = typeof(App).Assembly.GetName().Version ?? new Version(0, 0, 0, 0);

            var logger = loggerFactory.CreateLogger<HexagramViewModel>();
            CVHexagram = new CvHexagram(loggerFactory, configuration, alertService);
            DisplayVersionText();
            CVYarrowStalks = new(this);
            CVConfig = new CvConfig(this, config);

            // Set the content of the page
            Content = CVHexagram ?? throw new InvalidOperationException("CVHexagram cannot be null");
            //Content = CVConfig;
        }

        public void DisplayVersionText()
        {
            ((HexagramViewModel)CVHexagram.BindingContext).Answer = "Version: " + version?.ToString() + "\n\nWhat is the answer to the ultimate question of\nlife, the universe, and everything?";
        }
    }
}
