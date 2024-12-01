using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using YiChing.ViewModels;
using YiChing.Services;

namespace YiChing
{
    public static class MauiProgram
    {
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
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddTransient<HexagramViewModel>();
            builder.Services.AddTransient<CvHexagram>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CvYarrowStalks>(provider => 
                new CvYarrowStalks(provider.GetRequiredService<MainPage>()));

            return builder.Build();
        }
    }
}
