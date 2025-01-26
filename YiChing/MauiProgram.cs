using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using YiChing.Configuration;

namespace YiChing
{
    public static class MauiProgram
    {
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
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    // Explicit handler registrations for Windows
                    handlers.AddHandler(typeof(ContentView), typeof(Microsoft.Maui.Handlers.ContentViewHandler));
                    handlers.AddHandler(typeof(Microsoft.Maui.Controls.Button), typeof(Microsoft.Maui.Handlers.ButtonHandler));
                    handlers.AddHandler(typeof(Microsoft.Maui.Controls.Entry), typeof(Microsoft.Maui.Handlers.EntryHandler));
                    handlers.AddHandler(typeof(Microsoft.Maui.Controls.Editor), typeof(Microsoft.Maui.Handlers.EditorHandler));
                    handlers.AddHandler(typeof(Microsoft.Maui.Controls.Label), typeof(Microsoft.Maui.Handlers.LabelHandler));
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Register services
            builder.Services.AddSingleton<Settings>();
            builder.Services.AddTransient<MainPage>(sp => 
                new MainPage(sp.GetRequiredService<IConfiguration>()));
            builder.Services.AddTransient<CvConfig>();

            return builder.Build();
        }
    }
}
