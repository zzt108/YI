using System.Windows.Forms;

namespace YiWin
{
    public partial class frmYiWin : Form
    {
        private const int HorDist = 100;
        private const int VerDist = 40;
        private const int HorStart = 20;
        private const int VerStart = 40;

        protected CheckBox[,] checkBoxes = new CheckBox[6, 3];

        public frmYiWin()
        {
            InitializeComponent();

            // Create an array of CheckBox controls

            // Set the initial location for the checkboxes
            int x = HorStart;
            int y = VerStart;

            // Add CheckBoxes to the Panel in a 6x3 arrangement
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBoxes[row, col] = checkBox;
                    if (col == 2)
                    {
                        checkBox.Width = 100;
                        checkBox.Text = $"{row + 1}";
                    }
                    else
                    {
                        checkBox.Width = 40;
                        checkBox.Text = $"";
                    }

                    checkBox.Location = new Point(x, y);
                    gbCoins.Controls.Add(checkBox);

                    x += HorDist; // Adjust the horizontal spacing between checkboxes
                }

                x = HorStart; // Reset the x position for the next row
                y += VerDist; // Adjust the vertical spacing between rows
            }
        }

        private void btnEval_Click(object sender, EventArgs e)
        {
        }

        private void rtQuestion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
