using HexagramNS;
using System;
using System.Linq;




namespace YiWin
{
    partial class frmYarrowStalks : Form
    {
        const int YarrowClickTiming = 20;
        private const int LineWidth = 16;
        private const int LineHeight = 800;
        private const int ButtonWidth = 16;
        private const int ButtonHeight = LineHeight;
        private const int Spacing = 3;

        private Button[] lines;
        private Button[] buttons;

        public int[] divisions = new int[18];
        private YarrowStalksHelper helper = new YarrowStalksHelper();
        int[] piles = { 0, 0, 0 };
        int clickCount = 0;
        public Values values = new Values();
        int hexagramRow;
        public frmYiWin frmYiWin;

        public frmYarrowStalks()
        {
            InitializeComponent();
            GenerateLinesAndButtons(helper.RemainingStalkCount, panelSticks.Controls);
            hexagramRow = values.RowCount;
        }

        private void GenerateLinesAndButtons(int stickCount, Control.ControlCollection controls)
        {
            lines = new Button[stickCount];
            buttons = new Button[stickCount - 1];
            controls.Clear();

            int x = Spacing;
            int y = Spacing;

            for (int i = 0; i < stickCount; i++)
            {
                // Create a line
                Button line = new Button
                {
                    Width = LineWidth,
                    Height = LineHeight,
                    Location = new Point(x, y),
                    BackColor = Color.YellowGreen,
                    ForeColor = Color.Red,
                };
                line.Click += Line_Click;
                lines[i] = line;
                controls.Add(line);

                // Create a button (if applicable)
                if (i < stickCount - 1)
                {
                    int buttonX = x + LineWidth + Spacing;
                    Button button = new Button
                    {
                        Width = ButtonWidth,
                        Height = ButtonHeight,
                        Location = new Point(buttonX, y + (LineHeight - ButtonHeight) / 2),
                        ForeColor = Color.Black,
                        BackColor = Color.Black,
                        FlatStyle = FlatStyle.Popup,
                        Text = string.Empty
                    };
                    button.Click += Button_Click;
                    buttons[i] = button;
                    controls.Add(button);

                    x += LineWidth + ButtonWidth + Spacing * 2;
                }
                else
                {
                    x += LineWidth + Spacing;
                }
            }

            ClientSize = new Size(x + Spacing, LineHeight + Spacing * 2);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int buttonIndex = Array.IndexOf(buttons, clickedButton);

            int linesLeft = buttonIndex + 1;
            //int linesRight = buttons.Length - buttonIndex;

            HandleClick(linesLeft);

        }

        private void HandleClick(int linesLeft)
        {

            piles[clickCount % 3] = helper.GetHand(linesLeft);

            foreach (var line in lines) { line.Visible = false; Thread.Sleep(YarrowClickTiming); }
            if (clickCount < 18)
            {
                clickCount++;
                if (clickCount % 3 == 0)
                {
                    hexagramRow--;
                    values.SetHexagramRow(hexagramRow, YarrowStalksHelper.GetHexagramLine(piles));
                    frmYiWin.FillCheckBoxes(values);
                    helper.Reset();
                }
                if (clickCount == 18)
                {
                    this.Close();
                }
            }
            this.Text = $"18/: {clickCount} - 1:{piles[0]} -  2:{piles[1]} - 3:{piles[2]}";

            GenerateLinesAndButtons(helper.RemainingStalkCount, panelSticks.Controls);

            //MessageBox.Show($"Lines to the left: {linesLeft}", "Button Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Line_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int lineIndex = Array.IndexOf(lines, clickedButton);

            int linesLeft = lineIndex;
            if (lineIndex < helper.RemainingStalkCount / 2) linesLeft++;

            // linesRight = lines.Length - linesLeft;

            HandleClick(linesLeft);
        }

    }
}
