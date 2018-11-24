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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ParserTreeView = new System.Windows.Forms.TreeView();
            this.ParserTreeImage = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParserTreeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ParserTreeView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ParserTreeImage, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(534, 461);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ParserTreeView
            // 
            this.ParserTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParserTreeView.Location = new System.Drawing.Point(3, 3);
            this.ParserTreeView.Name = "ParserTreeView";
            this.ParserTreeView.Size = new System.Drawing.Size(261, 455);
            this.ParserTreeView.TabIndex = 0;
            // 
            // ParserTreeImage
            // 
            this.ParserTreeImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParserTreeImage.Location = new System.Drawing.Point(270, 3);
            this.ParserTreeImage.Name = "ParserTreeImage";
            this.ParserTreeImage.Size = new System.Drawing.Size(261, 455);
            this.ParserTreeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ParserTreeImage.TabIndex = 1;
            this.ParserTreeImage.TabStop = false;
            // 
            // ParserResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ParserResults";
            this.Text = "ParserResults";
            this.Load += new System.EventHandler(this.TokenTable_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ParserTreeImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView ParserTreeView;
        private System.Windows.Forms.PictureBox ParserTreeImage;
    }
}