using HexagramNS;
namespace YiWin
{
    public partial class frmYiWin : Form
    {
        private const int HorDist = 100;
        private const int VerDist = 40;
        private const int HorStart = 20;
        private const int VerStart = 40;

        protected CheckBox[,] CheckBoxes = new CheckBox[HexagramNS.Hexagram.RowCount, HexagramNS.Hexagram.ColCount];

        public void FillCheckBoxes(Values values)
        {
            values.InitValues<CheckBox>(CheckBoxes, (checkBox, row, col) => checkBox.Checked);
        }

        public frmYiWin()
        {
            InitializeComponent();

            // Set the initial location for the checkboxes
            int x = HorStart;
            int y = VerStart;

            // Add CheckBoxes to the Panel in a 6x3 arrangement
            for (int row = 0; row < HexagramNS.Hexagram.RowCount; row++)
            {
                for (int col = 0; col < HexagramNS.Hexagram.ColCount; col++)
                {
                    CheckBox checkBox = new CheckBox();
                    CheckBoxes[row, col] = checkBox;
                    if (col == HexagramNS.Hexagram.ColCount - 1)
                    {
                        checkBox.Width = 100;
                        checkBox.Text = $"{HexagramNS.Hexagram.RowCount - row}";
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var full = $"{DateTime.Now:d}\nQuestion to I Ching:\n {rtQuestion.Text}\n"
                + $"\nI Ching answered:\n{rtAnswer.Text}\nWould you please interpret?";
            Clipboard.SetText(full);
        }

        private void btnEval_Click(object sender, EventArgs e)
        {
            // Get the text from the RichTextBox control
            string question = rtQuestion.Text;

            var hexagram = new Hexagram(new Values().InitValues<CheckBox>(CheckBoxes, (checkBox, row, col) => checkBox.Checked));

            rtAnswer.Text = $"\nMain Hexagram {hexagram.Main}\n\n";
            if (hexagram.ChangingLines.Any())
            {
                rtAnswer.Text += "Changing lines: ";
                foreach (int line in hexagram.ChangingLines)
                {
                    rtAnswer.Text += line + ", ";
                }
                rtAnswer.Text += $"\n\nChanging Hexagram {hexagram.Changed}\n";
            }
            else
                rtAnswer.Text += "\n No changing lines ";



        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in CheckBoxes)
            {
                item.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new frmYarrowStalks();
            frm.frmYiWin = this;
            frm.ShowDialog();
        }
    }
}
