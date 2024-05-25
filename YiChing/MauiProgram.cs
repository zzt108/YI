using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace YiChing
{
    public static class MauiProgram
    {
        // public static IServiceProvider Services { get; private set; }

        public static MauiApp CreateMauiApp()
        {

        var builder = MauiApp.CreateBuilder();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            builder.Configuration.AddConfiguration(config);
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<MainPage>();

            MauiApp mauiApp = builder.Build();
            // Use DI, or set Services here
            // Services = mauiApp.Services;
            return mauiApp;
        }
    }
}
