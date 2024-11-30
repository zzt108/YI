using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using YiChing.ViewModels;

namespace YiChing
{
    public static class MauiProgram
    {
        // public static IServiceProvider Services { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services
            builder.Services.AddSingleton<IJsonHandler, JsonHandler>();
            builder.Services.AddTransient<HexagramViewModel>();
            builder.Services.AddTransient<CvHexagram>();

            // Add logging
            builder.Services.AddLogging(logging =>
            {
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Debug);
            });

            return builder.Build();
        }
    }
}
