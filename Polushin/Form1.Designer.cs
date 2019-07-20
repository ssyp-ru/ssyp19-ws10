namespace FakeLogo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GoButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.input = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.ClearLog = new System.Windows.Forms.Button();
            this.ClearMapButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(243, 12);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(34, 32);
            this.GoButton.TabIndex = 0;
            this.GoButton.Text = "GO";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.pictureBox1.Location = new System.Drawing.Point(0, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(734, 509);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // input
            // 
            this.input.BackColor = System.Drawing.SystemColors.GrayText;
            this.input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.input.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input.ForeColor = System.Drawing.Color.DarkMagenta;
            this.input.Location = new System.Drawing.Point(12, 12);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(265, 256);
            this.input.TabIndex = 2;
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.Color.Black;
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Font = new System.Drawing.Font("NSimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.ForeColor = System.Drawing.Color.Lime;
            this.output.Location = new System.Drawing.Point(12, 274);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(265, 247);
            this.output.TabIndex = 3;
            // 
            // ClearLog
            // 
            this.ClearLog.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearLog.ForeColor = System.Drawing.Color.Lime;
            this.ClearLog.Location = new System.Drawing.Point(226, 488);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(51, 33);
            this.ClearLog.TabIndex = 4;
            this.ClearLog.Text = "Clear";
            this.ClearLog.UseVisualStyleBackColor = false;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // ClearMapButton
            // 
            this.ClearMapButton.Location = new System.Drawing.Point(243, 50);
            this.ClearMapButton.Name = "ClearMapButton";
            this.ClearMapButton.Size = new System.Drawing.Size(42, 32);
            this.ClearMapButton.TabIndex = 5;
            this.ClearMapButton.Text = "Clear";
            this.ClearMapButton.UseVisualStyleBackColor = true;
            this.ClearMapButton.Click += new System.EventHandler(this.ClearMapButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(285, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 516);
            this.panel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1029, 533);
            this.Controls.Add(this.ClearMapButton);
            this.Controls.Add(this.ClearLog);
            this.Controls.Add(this.output);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.input);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "FakeLogo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Button ClearLog;
        private System.Windows.Forms.Button ClearMapButton;
        private System.Windows.Forms.Panel panel1;
    }
}

