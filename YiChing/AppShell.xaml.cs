using YiChing;

namespace YiChing;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute("CvYarrowStalks", typeof(CvYarrowStalks));
        Routing.RegisterRoute("CvHexagram", typeof(CvHexagram));
        Routing.RegisterRoute("MainPage", typeof(MainPage));
    }
}
