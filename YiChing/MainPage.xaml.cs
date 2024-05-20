using HG = HexagramNS;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        public CvHexagram CVHexagram;
        public CvYarrowStalks CVYarrowStalks;

        public async Task<bool> ShowMessageBox(string title, string message, string accept = "OK", string? cancel = null )
        {
            bool result = await DisplayAlert(title, message, accept, cancel);
            return result;
        }

        public MainPage()
        {
            InitializeComponent();
            //BackgroundColor = Colors.DarkBlue;
            
            CVHexagram = new(this);
            CVYarrowStalks = new(this);

            // Set the content of the page
            Content = CVHexagram;
        }
    }
}
