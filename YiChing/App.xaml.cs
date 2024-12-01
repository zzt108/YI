using Microsoft.Extensions.Configuration;

namespace YiChing
{
    public partial class App : Application
    {
        public App(MainPage page)
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
