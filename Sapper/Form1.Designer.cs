namespace Sapper
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            clockPanel = new Panel();
            clockLabel = new Label();
            panel2 = new Panel();
            minesLabel = new Label();
            clockPanel.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(12, 65);
            panel1.Name = "panel1";
            panel1.Size = new Size(355, 404);
            panel1.TabIndex = 0;
            panel1.Resize += panel1_Resize;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top;
            button1.Location = new Point(123, 24);
            button1.Name = "button1";
            button1.Size = new Size(132, 23);
            button1.TabIndex = 1;
            button1.Text = "Новая игра";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // clockPanel
            // 
            clockPanel.Controls.Add(clockLabel);
            clockPanel.Location = new Point(12, 12);
            clockPanel.Name = "clockPanel";
            clockPanel.Size = new Size(111, 47);
            clockPanel.TabIndex = 2;
            // 
            // clockLabel
            // 
            clockLabel.AutoSize = true;
            clockLabel.Location = new Point(32, 16);
            clockLabel.Name = "clockLabel";
            clockLabel.Size = new Size(40, 15);
            clockLabel.TabIndex = 4;
            clockLabel.Text = "00 : 00";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel2.Controls.Add(minesLabel);
            panel2.Location = new Point(256, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(111, 47);
            panel2.TabIndex = 3;
            // 
            // minesLabel
            // 
            minesLabel.AutoSize = true;
            minesLabel.Location = new Point(36, 20);
            minesLabel.Name = "minesLabel";
            minesLabel.Size = new Size(38, 15);
            minesLabel.TabIndex = 5;
            minesLabel.Text = "label1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 481);
            Controls.Add(panel2);
            Controls.Add(clockPanel);
            Controls.Add(button1);
            Controls.Add(panel1);
            MinimumSize = new Size(395, 520);
            Name = "MainForm";
            Text = "Сапёр";
            clockPanel.ResumeLayout(false);
            clockPanel.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private System.Windows.Forms.Timer timer1;
        private Panel clockPanel;
        private Panel panel2;
        private Label clockLabel;
        private Label minesLabel;
    }
}