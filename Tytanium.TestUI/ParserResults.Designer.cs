namespace Tytanium
{
    partial class ParserResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParserResults));
            this.ParserTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ParserTreeView
            // 
            this.ParserTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParserTreeView.Location = new System.Drawing.Point(0, 0);
            this.ParserTreeView.Name = "ParserTreeView";
            this.ParserTreeView.Size = new System.Drawing.Size(1264, 681);
            this.ParserTreeView.TabIndex = 1;
            // 
            // ParserResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.ParserTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ParserResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParserResults";
            this.Load += new System.EventHandler(this.TokenTable_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ParserTreeView;
    }
}