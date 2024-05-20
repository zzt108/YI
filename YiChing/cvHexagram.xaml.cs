using HG = HexagramNS;

namespace YiChing;

public partial class CvHexagram : ContentView
{
    //private Grid grid;

    private const int HorDist = 100;
    private const int VerDist = 40;
    private const int HorStart = 20;
    private const int VerStart = 40;

    public MainPage mainPage;

    protected CheckBox[,] CheckBoxes = new CheckBox[HG.Hexagram.RowCount, HG.Hexagram.ColCount];

    public void FillCheckBoxes(HG.Values values)
    {
        values.UpdateValues<CheckBox>(CheckBoxes, (row, col, value) =>
        {
            CheckBox checkBox = CheckBoxes[row, col];

            checkBox.IsChecked = value;
            return checkBox;
        });
    }

    public CvHexagram(MainPage mainPage)
    {
        InitializeComponent();

        // Add event handlers
        btnCopy.Clicked += btnCopy_Click;
        btnEval.Clicked += btnEval_Click;
        btnClear.Clicked += btnClear_Click;
        btnYarrow.Clicked += btnYarrow_Click;

        DrawHexagram();
        this.mainPage = mainPage;
    }

    private void DrawHexagram()
    {
        // Set the initial location for the checkboxes
        //int x = HorStart;
        //int y = VerStart;

        // Add CheckBoxes to the Panel in a 6x3 arrangement
        for (int row = 0; row < HG.Hexagram.RowCount; row++)
        {
            Label labelCB = new Label
            {
                Text = $"{HG.Hexagram.RowCount - row}",
                WidthRequest = 40,
                VerticalTextAlignment = TextAlignment.Center
            };
            gridHexagram.Add(labelCB, gridHexagram.ColumnDefinitions.Count - 1, row + 1);
            labelCB.WidthRequest = 40;

            for (int col = 0; col < HG.Hexagram.ColCount; col++)
            {
                CheckBox checkBox = new CheckBox();
                CheckBoxes[row, col] = checkBox;
                checkBox.WidthRequest = 40;

                // checkBox.Location = new Point(x, y);
                gridHexagram.Add(checkBox, col + 1, row + 1);
                //x += HorDist; // Adjust the horizontal spacing between checkboxes
            }
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ResetIndeterminate();
    }

    private void ResetIndeterminate()
    {
        new HG.Values().UpdateValues(
            CheckBoxes,
            (row, col, value) =>
            {

                CheckBox checkBox = CheckBoxes[row, col];
                checkBox.IsChecked = false;
                return checkBox;
            });
    }

    private void btnYarrow_Click(object sender, EventArgs e)
    {
        ResetIndeterminate();
        mainPage.Content = mainPage.CVYarrowStalks;
        //var frm = new frmYarrowStalks();
        //frm.frmYiWin = this;
        //frm.ShowDialog();
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
        var full = $"{DateTime.Now:d}\nQuestion to I Ching:\n {rtQuestion.Text}\n"
            + $"\nI Ching answered:\n{rtAnswer.Text}\nWould you please interpret?";
        Clipboard.SetTextAsync(full);
    }

    private void btnEval_Click(object sender, EventArgs e)
    {
        // Get the text from the RichTextBox control
        string question = rtQuestion.Text;

        var hexagram = new HG.Hexagram(new HG.Values().InitValues<CheckBox>(CheckBoxes, (checkBox, row, col) => checkBox.IsChecked));

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

}

