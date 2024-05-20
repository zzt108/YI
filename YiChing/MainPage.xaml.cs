using HG = HexagramNS;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        public CvHexagram CVHexagram = new();
        public CvYarrowStalks CVYarrowStalks = new();

        public async Task<bool> ShowMessageBox(string title, string message, string accept = "OK", string? cancel = null )
        {
            bool result = await DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public MainPage()
        {
            InitializeComponent();
            CVHexagram.mainPage = this;
            CVYarrowStalks.mainPage = this;

            // Set the content of the page
            Content = CVHexagram;
        }
    }
}
