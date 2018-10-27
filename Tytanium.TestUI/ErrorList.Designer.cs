namespace Tytanium
{
    partial class ErrorList
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
            this.ErrorListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ErrorListBox
            // 
            this.ErrorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorListBox.Location = new System.Drawing.Point(0, 0);
            this.ErrorListBox.Name = "ErrorListBox";
            this.ErrorListBox.Size = new System.Drawing.Size(684, 261);
            this.ErrorListBox.TabIndex = 0;
            // 
            // ErrorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 261);
            this.Controls.Add(this.ErrorListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ErrorList";
            this.Text = "ErrorList";
            this.Load += new System.EventHandler(this.ErrorList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ErrorListBox;
    }
}