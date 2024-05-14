namespace YiWin
{
    public partial class frmYiWin : Form
    {
        private const int HorDist = 100;
        private const int VerDist = 40;
        private const int HorStart = 20;
        private const int VerStart = 40;

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
        */
        private static readonly Dictionary<int, Dictionary<int, int>> hexagramLookup = new()
{
    { 111, new Dictionary<int, int> { { 111,  1 }, { 1, 34 }, { 10, 5  }, { 100, 26 }, { 0, 11 }, { 110, 9  }, { 11, 43 }, { 101, 14 } } },
    { 001, new Dictionary<int, int> { { 111, 25 }, { 1, 51 }, { 10, 3  }, { 100, 27 }, { 0, 24 }, { 110, 42 }, { 11, 17 }, { 101, 21 } } },
    { 010, new Dictionary<int, int> { { 111,  6 }, { 1, 40 }, { 10, 29 }, { 100, 4  }, { 0, 7  }, { 110, 59 }, { 11, 47 }, { 101, 64 } } },
    { 100, new Dictionary<int, int> { { 111, 33 }, { 1, 62 }, { 10, 39 }, { 100, 52 }, { 0, 15 }, { 110, 53 }, { 11, 31 }, { 101, 56 } } },
    { 000, new Dictionary<int, int> { { 111, 12 }, { 1, 16 }, { 10, 8  }, { 100, 23 }, { 0, 2  }, { 110, 20 }, { 11, 45 }, { 101, 35 } } },
    { 110, new Dictionary<int, int> { { 111, 44 }, { 1, 32 }, { 10, 48 }, { 100, 18 }, { 0, 46 }, { 110, 57 }, { 11, 28 }, { 101, 50 } } },
    { 101, new Dictionary<int, int> { { 111, 13 }, { 1, 55 }, { 10, 63 }, { 100, 22 }, { 0, 36 }, { 110, 37 }, { 11, 49 }, { 101, 30 } } },
    { 011, new Dictionary<int, int> { { 111, 10 }, { 1, 54 }, { 10, 60 }, { 100, 41 }, { 0, 19 }, { 110, 61 }, { 11, 58 }, { 101, 38 } } }
};
        private static Dictionary<string, int> hexagramLookup2 = new Dictionary<string, int>()
    {
        {"111111", 1}, {"111110", 2}, {"111101", 3}, {"111100", 4},
        {"111011", 5}, {"111010", 6}, {"111001", 7}, {"111000", 8},
        {"110111", 9}, {"110110", 10}, {"110101", 11}, {"110100", 12},
        {"110011", 13}, {"110010", 14}, {"110001", 15}, {"110000", 16},
        {"101111", 17}, {"101110", 18}, {"101101", 19}, {"101100", 20},
        {"101011", 21}, {"101010", 22}, {"101001", 23}, {"101000", 24},
        {"100111", 25}, {"100110", 26}, {"100101", 27}, {"100100", 28},
        {"100011", 29}, {"100010", 30}, {"100001", 31}, {"100000", 32},
        {"011111", 33}, {"011110", 34}, {"011101", 35}, {"011100", 36},
        {"011011", 37}, {"011010", 38}, {"011001", 39}, {"011000", 40},
        {"010111", 41}, {"010110", 42}, {"010101", 43}, {"010100", 44},
        {"010011", 45}, {"010010", 46}, {"010001", 47}, {"010000", 48},
        {"001111", 49}, {"001110", 50}, {"001101", 51}, {"001100", 52},
        {"001011", 53}, {"001010", 54}, {"001001", 55}, {"001000", 56},
        {"000111", 57}, {"000110", 58}, {"000101", 59}, {"000100", 60},
        {"000011", 61}, {"000010", 62}, {"000001", 63}, {"000000", 64}
    };

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
            // Get the text from the RichTextBox control
            string question = rtQuestion.Text;
        }

        private void rtQuestion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
