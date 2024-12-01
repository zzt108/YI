using YiChing;

namespace YiChing;

public partial class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        InitializeComponent();
        RegisterRoutes();
        
        // Set the first page of the shell
        CurrentItem = new ShellContent
        {
            Content = mainPage
        };
    }

    private void RegisterRoutes()
    {
        try 
        {
            // Register routes with full path
            Routing.RegisterRoute("CvYarrowStalks", typeof(CvYarrowStalks));
            Routing.RegisterRoute("CvHexagram", typeof(CvHexagram));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("CvConfig", typeof(CvConfig));

            // Add debug logging for routes
            System.Diagnostics.Debug.WriteLine("Registered Routes:");
            System.Diagnostics.Debug.WriteLine("- CvYarrowStalks");
            System.Diagnostics.Debug.WriteLine("- CvHexagram");
            System.Diagnostics.Debug.WriteLine("- MainPage");
            System.Diagnostics.Debug.WriteLine("- CvConfig");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Route registration error: {ex.Message}");
        }
    }
}
