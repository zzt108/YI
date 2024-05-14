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
            btnCopy = new Button();
            btnEval = new Button();
            groupBox2 = new GroupBox();
            rtAnswer = new RichTextBox();
            button1 = new Button();
            gbQuestion.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // gbCoins
            // 
            gbCoins.Location = new Point(12, 218);
            gbCoins.Name = "gbCoins";
            gbCoins.Size = new Size(324, 346);
            gbCoins.TabIndex = 1;
            gbCoins.TabStop = false;
            gbCoins.Text = "Coins";
            // 
            // gbQuestion
            // 
            gbQuestion.Controls.Add(rtQuestion);
            gbQuestion.Location = new Point(12, 12);
            gbQuestion.Name = "gbQuestion";
            gbQuestion.Size = new Size(516, 200);
            gbQuestion.TabIndex = 2;
            gbQuestion.TabStop = false;
            gbQuestion.Text = "Question";
            // 
            // rtQuestion
            // 
            rtQuestion.Dock = DockStyle.Fill;
            rtQuestion.Location = new Point(3, 35);
            rtQuestion.Name = "rtQuestion";
            rtQuestion.Size = new Size(510, 162);
            rtQuestion.TabIndex = 0;
            rtQuestion.Text = "";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(btnCopy);
            groupBox1.Controls.Add(btnEval);
            groupBox1.Location = new Point(342, 218);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(186, 346);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Action";
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(33, 134);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(124, 90);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "&Copy";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // btnEval
            // 
            btnEval.Location = new Point(33, 38);
            btnEval.Name = "btnEval";
            btnEval.Size = new Size(124, 90);
            btnEval.TabIndex = 0;
            btnEval.Text = "&Eval";
            btnEval.UseVisualStyleBackColor = true;
            btnEval.Click += btnEval_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(rtAnswer);
            groupBox2.Location = new Point(545, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(764, 553);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Results";
            // 
            // rtAnswer
            // 
            rtAnswer.Dock = DockStyle.Fill;
            rtAnswer.Location = new Point(3, 35);
            rtAnswer.Name = "rtAnswer";
            rtAnswer.Size = new Size(758, 515);
            rtAnswer.TabIndex = 0;
            rtAnswer.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(33, 230);
            button1.Name = "button1";
            button1.Size = new Size(124, 90);
            button1.TabIndex = 2;
            button1.Text = "Clear throw";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // frmYiWin
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1321, 577);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(gbQuestion);
            Controls.Add(gbCoins);
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
    }
}
