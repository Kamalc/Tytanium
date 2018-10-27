namespace Tytanium
{
    partial class TestUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestUI));
            this.CodeBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineNoBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CodeBox1
            // 
            this.CodeBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CodeBox1.Location = new System.Drawing.Point(45, 30);
            this.CodeBox1.Name = "CodeBox1";
            this.CodeBox1.Size = new System.Drawing.Size(674, 349);
            this.CodeBox1.TabIndex = 0;
            this.CodeBox1.Text = "";
            this.CodeBox1.VScroll += new System.EventHandler(this.CodeBox1_VScroll);
            this.CodeBox1.TextChanged += new System.EventHandler(this.CodeBox1_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(734, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.scanToolStripMenuItem.Text = "Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.scanToolStripMenuItem_Click);
            // 
            // LineNoBox
            // 
            this.LineNoBox.BackColor = System.Drawing.Color.White;
            this.LineNoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineNoBox.Location = new System.Drawing.Point(12, 30);
            this.LineNoBox.Name = "LineNoBox";
            this.LineNoBox.ReadOnly = true;
            this.LineNoBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LineNoBox.Size = new System.Drawing.Size(27, 349);
            this.LineNoBox.TabIndex = 2;
            this.LineNoBox.Text = "";
            // 
            // TestUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 390);
            this.Controls.Add(this.LineNoBox);
            this.Controls.Add(this.CodeBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "TestUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test UI - Project Tytanium";
            this.Load += new System.EventHandler(this.TestUI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox CodeBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.RichTextBox LineNoBox;
    }
}

