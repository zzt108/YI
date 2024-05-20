using HG = HexagramNS;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        public CvHexagram CVHexagram;
        public CvYarrowStalks CVYarrowStalks;
        public Version version;

        public async Task<bool> ShowMessageBox(string title, string message, string accept = "OK", string? cancel = null )
        {
            bool result = await DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public MainPage()
        {
            InitializeComponent();
            //BackgroundColor = Colors.DarkBlue;
            version = typeof(App).Assembly.GetName().Version;

            CVHexagram = new(this);
            CVHexagram.Question.Text = "Version: " + version.ToString() + "\nWhat is the answer to the ultimate question of life, the universe, and everything?";
            CVYarrowStalks = new(this);

            // Set the content of the page
            Content = CVHexagram;
        }
    }
}
