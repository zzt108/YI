using HG = HexagramNS;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {

        public CvHexagram CVHexagram = new();
        public CvYarrowStalks CVYarrowStalks = new();

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
