namespace YiWin
{
    public partial class frmYiWin : Form
    {
        private const int HorDist = 100;
        private const int VerDist = 40;
        private const int HorStart = 20;
        private const int VerStart = 40;
        private const int RowCount = 6;
        private const int ColCount = 3;

        /*
         Here is a table with the numerical values in each cell of the provided image:

|   |111 | 001| 010| 100| 0  | 110|101 | 011|
|111| 1  | 34 | 5  | 26 | 11 | 9  | 14 | 43 |
|001| 25 | 51 | 3  | 27 | 24 | 42 | 21 | 17 |
|010| 6  | 40 | 29 | 4  | 7  | 59 | 64 | 47 |
|100| 33 | 62 | 39 | 52 | 15 | 53 | 56 | 31 |
|000| 12 | 16 | 8  | 23 | 2  | 20 | 35 | 45 |
|110| 44 | 32 | 48 | 18 | 46 | 57 | 50 | 28 |
|101| 13 | 55 | 63 | 22 | 36 | 37 | 30 | 49 |
|011| 10 | 54 | 60 | 41 | 19 | 61 | 38 | 58 |

        111 000 = 11
        */
        private static readonly Dictionary<int, Dictionary<int, int>> hexagramLookup = new()
{
    { 111, new Dictionary<int, int> { { 111,  1 }, { 1, 34 }, { 10, 5  }, { 100, 26 }, { 0, 11 }, { 110, 9  }, { 11, 43 }, { 101, 14 } } },
    { 001, new Dictionary<int, int> { { 111, 25 }, { 1, 51 }, { 10, 3 }, { 100, 27 }, { 0, 24 }, { 110, 42 }, { 11, 17 }, { 101, 21 } } },
    { 010, new Dictionary<int, int> { { 111,  6 }, { 1, 40 }, { 10, 29 }, { 100, 4  }, { 0, 7  }, { 110, 59 }, { 11, 47 }, { 101, 64 } } },
    { 100, new Dictionary<int, int> { { 111, 33 }, { 1, 62 }, { 10, 39 }, { 100, 52 }, { 0, 15 }, { 110, 53 }, { 11, 31 }, { 101, 56 } } },
    { 000, new Dictionary<int, int> { { 111, 12 }, { 1, 16 }, { 10, 8  }, { 100, 23 }, { 0, 2  }, { 110, 20 }, { 11, 45 }, { 101, 35 } } },
    { 110, new Dictionary<int, int> { { 111, 44 }, { 1, 32 }, { 10, 48 }, { 100, 18 }, { 0, 46 }, { 110, 57 }, { 11, 28 }, { 101, 50 } } },
    { 101, new Dictionary<int, int> { { 111, 13 }, { 1, 55 }, { 10, 63 }, { 100, 22 }, { 0, 36 }, { 110, 37 }, { 11, 49 }, { 101, 30 } } },
    { 011, new Dictionary<int, int> { { 111, 10 }, { 1, 54 }, { 10, 60 }, { 100, 41 }, { 0, 19 }, { 110, 61 }, { 11, 58 }, { 101, 38 } } }
};
        protected CheckBox[,] CheckBoxes = new CheckBox[RowCount, ColCount];

        public frmYiWin()
        {
            InitializeComponent();

            // Set the initial location for the checkboxes
            int x = HorStart;
            int y = VerStart;

            // Add CheckBoxes to the Panel in a 6x3 arrangement
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    CheckBox checkBox = new CheckBox();
                    CheckBoxes[row, col] = checkBox;
                    if (col == ColCount - 1)
                    {
                        checkBox.Width = 100;
                        checkBox.Text = $"{RowCount - row}";
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
            var changingLines = new List<int>();
            var tg = GetTrigrams();
            var hexagram = hexagramLookup[tg[0]][tg[1]];
            var changedHexagram = hexagramLookup[tg[2]][tg[3]];
            rtAnswer.Text = $"Main Hexagram {hexagram}\n";
            if (changingLines.Count > 0)
            {
                rtAnswer.Text += $"Changing Hexagram {changedHexagram}\n";
                rtAnswer.Text += "Changing lines: ";
                foreach (int line in changingLines)
                {
                    rtAnswer.Text += line + ", ";
                }
            }
            else
                rtAnswer.Text += " No changing lines";


            int[] GetTrigrams()
            {
                var rowStr = string.Empty;
                string[] trigrams = ["", ""];
                string[] changedTrigrams = ["", ""];
                var pos = 0;

                for (int row = RowCount - 1; row >= 0; row--)
                {
                    pos = row > 2 ? 0 : 1;
                    var line = GetLine(row);
                    switch (line)
                    {
                        case 6:
                            trigrams[pos] += "0";
                            changedTrigrams[pos] += "1";
                            changingLines.Add(RowCount - row); break;
                        case 7:
                            changedTrigrams[pos] += "0";
                            trigrams[pos] += "0";
                            break;
                        case 8:
                            changedTrigrams[pos] += "1";
                            trigrams[pos] += "1";
                            break;
                        case 9:
                            changedTrigrams[pos] += "0";
                            trigrams[pos] += "1";
                            changingLines.Add(RowCount - row); break;
                    }
                }
                return [int.Parse(trigrams[0]), int.Parse(trigrams[1]), int.Parse(changedTrigrams[0]), int.Parse(changedTrigrams[1])];

                int GetLine(int row)
                {
                    var line = 0;
                    for (int col = 0; col < ColCount; col++)
                    {
                        var checkbox = CheckBoxes[row, col];
                        if (checkbox.Checked)
                        {
                            line += 3;
                        }
                        else
                        {
                            line += 2;
                        }
                    }
                    return line;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in CheckBoxes)
            {
                item.Checked = false;
            }
        }
    }
}
