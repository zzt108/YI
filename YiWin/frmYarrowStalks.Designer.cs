namespace YiWin
{
    partial class frmYarrowStalks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelSticks = new Panel();
            panelinfo = new Panel();
            labelClicksLeft = new Label();
            panelinfo.SuspendLayout();
            SuspendLayout();
            // 
            // panelSticks
            // 
            panelSticks.BackColor = Color.Black;
            panelSticks.Dock = DockStyle.Fill;
            panelSticks.Location = new Point(10, 10);
            panelSticks.Name = "panelSticks";
            panelSticks.Size = new Size(712, 292);
            panelSticks.TabIndex = 0;
            // 
            // panelinfo
            // 
            panelinfo.Controls.Add(labelClicksLeft);
            panelinfo.Dock = DockStyle.Right;
            panelinfo.Font = new Font("Segoe UI", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panelinfo.Location = new Point(676, 10);
            panelinfo.Name = "panelinfo";
            panelinfo.Size = new Size(46, 292);
            panelinfo.TabIndex = 1;
            panelinfo.Visible = false;
            // 
            // labelClicksLeft
            // 
            labelClicksLeft.AutoSize = true;
            labelClicksLeft.Dock = DockStyle.Top;
            labelClicksLeft.Location = new Point(0, 0);
            labelClicksLeft.Name = "labelClicksLeft";
            labelClicksLeft.Size = new Size(47, 20);
            labelClicksLeft.TabIndex = 0;
            labelClicksLeft.Text = "18/18";
            // 
            // frmYarrowStalks
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 312);
            Controls.Add(panelinfo);
            Controls.Add(panelSticks);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmYarrowStalks";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Yarrow Stalks";
            panelinfo.ResumeLayout(false);
            panelinfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSticks;
        private Panel panelinfo;
        private Label labelClicksLeft;
    }
}
