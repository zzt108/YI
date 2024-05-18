using HexagramNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;



namespace YiWin
{
    partial class YarrowStalks : Form
    {
        private const int LineWidth = 8;
        private const int LineHeight = 400;
        private const int ButtonWidth = 8;
        private const int ButtonHeight = LineHeight;
        private const int Spacing = 3;

        private Button[] lines;
        private Button[] buttons;

        public int[] divisions = new int[18];
        private YarrowStalksHelper helper = new YarrowStalksHelper();

        public YarrowStalks()
        {
            InitializeComponent();
            GenerateLinesAndButtons(helper.RemainingStalkCount, panelSticks.Controls);
        }

        private void GenerateLinesAndButtons(int stickCount, Control.ControlCollection controls)
        {
            lines = new Button[stickCount];
            buttons = new Button[stickCount-1];

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
                if (i < stickCount-1)
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

            ClientSize = new Size(x + 2*Spacing+panelinfo.Width, LineHeight + Spacing * 2);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int buttonIndex = Array.IndexOf(buttons, clickedButton);

            int linesLeft = buttonIndex+1;
            int linesRight = buttons.Length - buttonIndex;

            helper.GetHand(linesLeft);

            MessageBox.Show($"Lines to the left: {linesLeft}\nLines to the right: {linesRight}", "Button Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Line_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int lineIndex = Array.IndexOf(lines, clickedButton);

            int linesLeft = lineIndex;
            if (lineIndex < helper.RemainingStalkCount/2) linesLeft++;

            int linesRight = lines.Length - linesLeft;

            helper.GetHand(linesLeft);

            MessageBox.Show($"Lines to the left: {linesLeft}\nLines to the right: {linesRight}", "Button Click", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
