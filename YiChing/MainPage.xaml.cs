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

        public MainPage(
            ILogger<MainPage> logger,
            IConfiguration configuration,
            IAlertService alertService,
            INavigationService navigationService,
            ILoggerFactory loggerFactory)
        {
            InitializeComponent();

            // Null checks for critical dependencies
            ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
            ArgumentNullException.ThrowIfNull(alertService, nameof(alertService));
            ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

            Configuration = configuration;

            Version = typeof(App).Assembly.GetName().Version ?? new Version(0, 0, 0, 0);

            // Create Hexagram page with this MainPage as a parameter
            CVHexagram = new CvHexagram(
                loggerFactory.CreateLogger<CvHexagram>(),
                loggerFactory,
                configuration,
                alertService,
                navigationService,
                this);  // Pass this MainPage instance

            DisplayVersionText();
            CVYarrowStalks = new(this);
            CVConfig = new CvConfig(this, configuration, alertService);

            // Log navigation details
            System.Diagnostics.Debug.WriteLine($"MainPage Loaded");
            System.Diagnostics.Debug.WriteLine($"MainPage Shell: {Shell.Current}");
            System.Diagnostics.Debug.WriteLine($"MainPage Current Page: {Shell.Current?.CurrentPage?.GetType().Name}");

            // Ensure routes are registered
            try 
            {
                Routing.RegisterRoute("CvHexagram", typeof(CvHexagram));
                Routing.RegisterRoute("CvConfig", typeof(CvConfig));
                Routing.RegisterRoute("MainPage", typeof(MainPage));
                Routing.RegisterRoute("CvYarrowStalks", typeof(CvYarrowStalks));

                System.Diagnostics.Debug.WriteLine("Routes registered in MainPage constructor");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Route registration error in MainPage: {ex.Message}");
            }

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

        // Add this method to navigate to Configuration page
        public async Task NavigateToConfigPage()
        {
            try 
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("CvConfig");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Shell.Current is null, cannot navigate to Configuration");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation to Configuration error: {ex.Message}");
            }
        }
    }
}
