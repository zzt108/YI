namespace YiWin
{
    partial class frmYiWin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gbCoins = new GroupBox();
            gbQuestion = new GroupBox();
            rtQuestion = new RichTextBox();
            groupBox1 = new GroupBox();
            button1 = new Button();
            btnCopy = new Button();
            btnEval = new Button();
            groupBox2 = new GroupBox();
            rtAnswer = new RichTextBox();
            button2 = new Button();
            gbQuestion.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // gbCoins
            // 
            gbCoins.Location = new Point(6, 102);
            gbCoins.Margin = new Padding(2, 1, 2, 1);
            gbCoins.Name = "gbCoins";
            gbCoins.Padding = new Padding(2, 1, 2, 1);
            gbCoins.Size = new Size(174, 162);
            gbCoins.TabIndex = 1;
            gbCoins.TabStop = false;
            gbCoins.Text = "Coins";
            // 
            // gbQuestion
            // 
            gbQuestion.Controls.Add(rtQuestion);
            gbQuestion.Location = new Point(6, 6);
            gbQuestion.Margin = new Padding(2, 1, 2, 1);
            gbQuestion.Name = "gbQuestion";
            gbQuestion.Padding = new Padding(2, 1, 2, 1);
            gbQuestion.Size = new Size(278, 94);
            gbQuestion.TabIndex = 2;
            gbQuestion.TabStop = false;
            gbQuestion.Text = "Question";
            // 
            // rtQuestion
            // 
            rtQuestion.Dock = DockStyle.Fill;
            rtQuestion.Location = new Point(2, 17);
            rtQuestion.Margin = new Padding(2, 1, 2, 1);
            rtQuestion.Name = "rtQuestion";
            rtQuestion.Size = new Size(274, 76);
            rtQuestion.TabIndex = 0;
            rtQuestion.Text = "";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(btnCopy);
            groupBox1.Controls.Add(btnEval);
            groupBox1.Location = new Point(184, 102);
            groupBox1.Margin = new Padding(2, 1, 2, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 1, 2, 1);
            groupBox1.Size = new Size(100, 162);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Action";
            // 
            // button1
            // 
            button1.Location = new Point(18, 75);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(67, 23);
            button1.TabIndex = 2;
            button1.Text = "Clear throw";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(18, 48);
            btnCopy.Margin = new Padding(2, 1, 2, 1);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(67, 25);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "&Copy";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // btnEval
            // 
            btnEval.Location = new Point(18, 18);
            btnEval.Margin = new Padding(2, 1, 2, 1);
            btnEval.Name = "btnEval";
            btnEval.Size = new Size(67, 28);
            btnEval.TabIndex = 0;
            btnEval.Text = "&Eval";
            btnEval.UseVisualStyleBackColor = true;
            btnEval.Click += btnEval_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(rtAnswer);
            groupBox2.Location = new Point(293, 6);
            groupBox2.Margin = new Padding(2, 1, 2, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 1, 2, 1);
            groupBox2.Size = new Size(411, 259);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Results";
            // 
            // rtAnswer
            // 
            rtAnswer.Dock = DockStyle.Fill;
            rtAnswer.Location = new Point(2, 17);
            rtAnswer.Margin = new Padding(2, 1, 2, 1);
            rtAnswer.Name = "rtAnswer";
            rtAnswer.Size = new Size(407, 241);
            rtAnswer.TabIndex = 0;
            rtAnswer.Text = "";
            // 
            // button2
            // 
            button2.Location = new Point(18, 100);
            button2.Margin = new Padding(2, 1, 2, 1);
            button2.Name = "button2";
            button2.Size = new Size(67, 23);
            button2.TabIndex = 3;
            button2.Text = "Yarrow Stalks";
            button2.TextAlign = ContentAlignment.BottomCenter;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // frmYiWin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 270);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(gbQuestion);
            Controls.Add(gbCoins);
            Margin = new Padding(2, 1, 2, 1);
            Name = "frmYiWin";
            Text = "Yi Ching";
            gbQuestion.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbCoins;
        private GroupBox gbQuestion;
        private RichTextBox rtQuestion;
        private GroupBox groupBox1;
        private Button btnCopy;
        private Button btnEval;
        private GroupBox groupBox2;
        private RichTextBox rtAnswer;
        private Button button1;
        private Button button2;
    }
}
