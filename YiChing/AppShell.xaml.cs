using YiChing;

namespace YiChing;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("CvYarrowStalks", typeof(CvYarrowStalks));
    }
}
