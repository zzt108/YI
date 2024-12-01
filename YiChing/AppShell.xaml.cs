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
        Routing.RegisterRoute("CvYarrowStalks", typeof(CvYarrowStalks));
        Routing.RegisterRoute("CvHexagram", typeof(CvHexagram));
        Routing.RegisterRoute("MainPage", typeof(MainPage));
    }
}
