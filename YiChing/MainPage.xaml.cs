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

            // Navigate to Hexagram page after a short delay to ensure Shell is fully initialized
            Loaded += async (s, e) => 
            {
                try 
                {
                    await Task.Delay(500); // Small delay to ensure Shell is ready
                    if (Shell.Current != null)
                    {
                        await Shell.Current.GoToAsync("CvHexagram");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Shell.Current is null, cannot navigate");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
                }
            };
        }

        public void DisplayVersionText()
        {
            if (CVHexagram?.BindingContext is HexagramViewModel viewModel)
            {
                viewModel.Answer = "Version: " + Version?.ToString() + "\n\nWhat is the answer to the ultimate question of\nlife, the universe, and everything?";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Could not set version text: CVHexagram or BindingContext is null");
            }
        }
    }
}
