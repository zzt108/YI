using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using YiChing.Services;
using YiChing.ViewModels;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        public IConfiguration Configuration { get; }
        public Version Version { get; }
        public CvHexagram CVHexagram { get; }
        public CvYarrowStalks CVYarrowStalks { get; }
        public CvConfig CVConfig { get; }

        public async Task<bool> ShowMessageBox(string title, string message, string accept = "OK", string? cancel = null)
        {
            bool result = await DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public MainPage(ILoggerFactory loggerFactory, IConfiguration config, IAlertService alertService, INavigationService navigationService)
        {
            // Null checks for critical dependencies
            ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
            ArgumentNullException.ThrowIfNull(config, nameof(config));
            ArgumentNullException.ThrowIfNull(alertService, nameof(alertService));
            ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

            InitializeComponent();
            Configuration = config;

            Version = typeof(App).Assembly.GetName().Version ?? new Version(0, 0, 0, 0);

            CVHexagram = new CvHexagram(
                loggerFactory.CreateLogger<CvHexagram>(), 
                loggerFactory, 
                Configuration, 
                alertService, 
                navigationService);
            DisplayVersionText();
            CVYarrowStalks = new(this);
            CVConfig = new CvConfig(this, Configuration);

            // Set the content of the page
            Content = CVHexagram ?? throw new InvalidOperationException("CVHexagram cannot be null");
            //Content = CVConfig;
        }

        public void DisplayVersionText()
        {
            ((HexagramViewModel)CVHexagram.BindingContext).Answer = "Version: " + Version?.ToString() + "\n\nWhat is the answer to the ultimate question of\nlife, the universe, and everything?";
        }
    }
}
