namespace Tytanium.Continuum
{
    partial class Editor
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
            this.CodeBox1 = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LineNoBox = new System.Windows.Forms.RichTextBox();
            this.Minimize = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.PictureBox();
            this.Maximize = new System.Windows.Forms.PictureBox();
            this.TitlePane = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximize)).BeginInit();
            this.TitlePane.SuspendLayout();
            this.SuspendLayout();
            // 
            // CodeBox1
            // 
            this.CodeBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CodeBox1.DetectUrls = false;
            this.CodeBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CodeBox1.Location = new System.Drawing.Point(47, 31);
            this.CodeBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CodeBox1.Name = "CodeBox1";
            this.CodeBox1.Size = new System.Drawing.Size(790, 456);
            this.CodeBox1.TabIndex = 1;
            this.CodeBox1.TabStop = false;
            this.CodeBox1.Text = "";
            this.CodeBox1.VScroll += new System.EventHandler(this.CodeBox1_VScroll);
            this.CodeBox1.TextChanged += new System.EventHandler(this.CodeBox1_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Khaki;
            this.panel3.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(0, 495);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(850, 5);
            this.panel3.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Khaki;
            this.panel1.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(846, -50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 700);
            this.panel1.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Khaki;
            this.panel2.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(-1, -100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 700);
            this.panel2.TabIndex = 26;
            // 
            // LineNoBox
            // 
            this.LineNoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LineNoBox.BackColor = System.Drawing.Color.White;
            this.LineNoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineNoBox.Location = new System.Drawing.Point(10, 31);
            this.LineNoBox.Name = "LineNoBox";
            this.LineNoBox.ReadOnly = true;
            this.LineNoBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LineNoBox.Size = new System.Drawing.Size(37, 456);
            this.LineNoBox.TabIndex = 9;
            this.LineNoBox.Text = "";
            // 
            // Minimize
            // 
            this.Minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Minimize.BackColor = System.Drawing.Color.Transparent;
            this.Minimize.Image = global::Tytanium.Continuum.Properties.Resources.Minimize;
            this.Minimize.Location = new System.Drawing.Point(778, 2);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(20, 20);
            this.Minimize.TabIndex = 5;
            this.Minimize.TabStop = false;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            this.Minimize.MouseEnter += new System.EventHandler(this.Minimize_MouseEnter);
            this.Minimize.MouseLeave += new System.EventHandler(this.Minimize_MouseLeave);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Enabled = false;
            this.title.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.title.Location = new System.Drawing.Point(9, 1);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(111, 20);
            this.title.TabIndex = 2;
            this.title.Text = "Untitled.jigsaw";
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.Image = global::Tytanium.Continuum.Properties.Resources.Close;
            this.close.Location = new System.Drawing.Point(826, 2);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(20, 20);
            this.close.TabIndex = 7;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            this.close.MouseEnter += new System.EventHandler(this.close_MouseEnter);
            this.close.MouseLeave += new System.EventHandler(this.close_MouseLeave);
            // 
            // Maximize
            // 
            this.Maximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Maximize.BackColor = System.Drawing.Color.Transparent;
            this.Maximize.Image = global::Tytanium.Continuum.Properties.Resources.Maximized;
            this.Maximize.Location = new System.Drawing.Point(802, 2);
            this.Maximize.Name = "Maximize";
            this.Maximize.Size = new System.Drawing.Size(20, 20);
            this.Maximize.TabIndex = 8;
            this.Maximize.TabStop = false;
            this.Maximize.Click += new System.EventHandler(this.Maximize_Click);
            this.Maximize.MouseEnter += new System.EventHandler(this.Maximize_MouseEnter);
            this.Maximize.MouseLeave += new System.EventHandler(this.Maximize_MouseLeave);
            // 
            // TitlePane
            // 
            this.TitlePane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitlePane.BackColor = System.Drawing.Color.Transparent;
            this.TitlePane.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.TitlePane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TitlePane.Controls.Add(this.Maximize);
            this.TitlePane.Controls.Add(this.close);
            this.TitlePane.Controls.Add(this.title);
            this.TitlePane.Controls.Add(this.Minimize);
            this.TitlePane.Location = new System.Drawing.Point(-5, -1);
            this.TitlePane.Name = "TitlePane";
            this.TitlePane.Size = new System.Drawing.Size(867, 24);
            this.TitlePane.TabIndex = 8;
            this.TitlePane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitlePane_MouseDown);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.Controls.Add(this.LineNoBox);
            this.Controls.Add(this.TitlePane);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.CodeBox1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Editor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Code Editor";
            this.Shown += new System.EventHandler(this.Editor_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Maximize)).EndInit();
            this.TitlePane.ResumeLayout(false);
            this.TitlePane.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox CodeBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox LineNoBox;
        private System.Windows.Forms.PictureBox Minimize;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.PictureBox Maximize;
        private System.Windows.Forms.Panel TitlePane;
    }
}