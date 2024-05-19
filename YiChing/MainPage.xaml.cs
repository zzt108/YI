using HexagramNS;

namespace YiChing
{
    public partial class MainPage : ContentPage
    {
        //private Grid grid;

        private const int HorDist = 100;
        private const int VerDist = 40;
        private const int HorStart = 20;
        private const int VerStart = 40;

        protected CheckBox[,] CheckBoxes = new CheckBox[Hexagram.RowCount, Hexagram.ColCount];

        public void FillCheckBoxes(Values values)
        {
            values.UpdateValues<CheckBox>(CheckBoxes, (row, col, value) =>
            {
                CheckBox checkBox = CheckBoxes[row, col];

                checkBox.IsChecked = value;
                return checkBox;
            });
        }

        public MainPage()
        {
            InitializeComponent();


            // Set the content of the page
//            Content = grid;

            // Add event handlers
            btnCopy.Clicked += btnCopy_Click;
            btnEval.Clicked += btnEval_Click;

        }



        private void btnCopy_Click(object sender, EventArgs e)
        {
            var full = $"Question to I Ching:\n {rtQuestion.Text}\n I Ching answered:\n{rtAnswer.Text}\nWould you please interpret?";
            Clipboard.SetTextAsync(full);
        }

        private void btnEval_Click(object sender, EventArgs e)
        {
            // Get the text from the RichTextBox control
            string question = rtQuestion.Text;

            // Set the answer in the RichTextBox control
            rtAnswer.Text = question + " " + "I Ching";
        }

    }

}
