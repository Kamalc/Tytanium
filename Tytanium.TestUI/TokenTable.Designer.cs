namespace Tytanium
{
    partial class TokenTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenTable));
            this.TokenView = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiteralKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecondryClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TokenView)).BeginInit();
            this.SuspendLayout();
            // 
            // TokenView
            // 
            this.TokenView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TokenView.BackgroundColor = System.Drawing.Color.White;
            this.TokenView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TokenView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TokenView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.LiteralKey,
            this.ClassKey,
            this.SecondryClass});
            this.TokenView.Location = new System.Drawing.Point(3, 2);
            this.TokenView.MultiSelect = false;
            this.TokenView.Name = "TokenView";
            this.TokenView.ReadOnly = true;
            this.TokenView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.TokenView.RowHeadersVisible = false;
            this.TokenView.RowHeadersWidth = 10;
            this.TokenView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TokenView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TokenView.Size = new System.Drawing.Size(529, 457);
            this.TokenView.TabIndex = 0;
            // 
            // Index
            // 
            this.Index.HeaderText = "#";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 30;
            // 
            // LiteralKey
            // 
            this.LiteralKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LiteralKey.HeaderText = "Literal";
            this.LiteralKey.Name = "LiteralKey";
            this.LiteralKey.ReadOnly = true;
            this.LiteralKey.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LiteralKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LiteralKey.Width = 120;
            // 
            // ClassKey
            // 
            this.ClassKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ClassKey.HeaderText = "Primary Class";
            this.ClassKey.Name = "ClassKey";
            this.ClassKey.ReadOnly = true;
            this.ClassKey.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ClassKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SecondryClass
            // 
            this.SecondryClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SecondryClass.HeaderText = "Secondry Class";
            this.SecondryClass.Name = "SecondryClass";
            this.SecondryClass.ReadOnly = true;
            this.SecondryClass.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SecondryClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TokenTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.TokenView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TokenTable";
            this.Text = "TokenTable";
            this.Load += new System.EventHandler(this.TokenTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TokenView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TokenView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiteralKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecondryClass;
    }
}