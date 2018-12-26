namespace Tytanium.Continuum
{
    partial class NewFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewFile));
            this.TitlePane = new System.Windows.Forms.Panel();
            this.close = new System.Windows.Forms.PictureBox();
            this.Minimize = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.upperlbl = new System.Windows.Forms.Label();
            this.subjectText = new System.Windows.Forms.TextBox();
            this.ok_lbl = new System.Windows.Forms.Label();
            this.Ok_btn = new System.Windows.Forms.Panel();
            this.Icon_Add = new System.Windows.Forms.PictureBox();
            this.Browse = new System.Windows.Forms.Panel();
            this.Browse_lbl = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TitlePane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).BeginInit();
            this.Ok_btn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_Add)).BeginInit();
            this.Browse.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitlePane
            // 
            this.TitlePane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitlePane.BackColor = System.Drawing.Color.Transparent;
            this.TitlePane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TitlePane.BackgroundImage")));
            this.TitlePane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TitlePane.Controls.Add(this.close);
            this.TitlePane.Controls.Add(this.Minimize);
            this.TitlePane.Controls.Add(this.title);
            this.TitlePane.Location = new System.Drawing.Point(-4, -1);
            this.TitlePane.Name = "TitlePane";
            this.TitlePane.Size = new System.Drawing.Size(418, 24);
            this.TitlePane.TabIndex = 11;
            this.TitlePane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitlePane_MouseDown);
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.Image = ((System.Drawing.Image)(resources.GetObject("close.Image")));
            this.close.Location = new System.Drawing.Point(386, 2);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(20, 20);
            this.close.TabIndex = 13;
            this.close.TabStop = false;
            this.close.MouseClick += new System.Windows.Forms.MouseEventHandler(this.close_Click);
            this.close.MouseEnter += new System.EventHandler(this.close_MouseEnter);
            this.close.MouseLeave += new System.EventHandler(this.close_MouseLeave);
            // 
            // Minimize
            // 
            this.Minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Minimize.BackColor = System.Drawing.Color.Transparent;
            this.Minimize.Image = ((System.Drawing.Image)(resources.GetObject("Minimize.Image")));
            this.Minimize.Location = new System.Drawing.Point(344, 2);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(20, 20);
            this.Minimize.TabIndex = 12;
            this.Minimize.TabStop = false;
            this.Minimize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Minimize_Click);
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
            this.title.Location = new System.Drawing.Point(10, 2);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(161, 20);
            this.title.TabIndex = 2;
            this.title.Text = "Create a new code file";
            // 
            // upperlbl
            // 
            this.upperlbl.AutoSize = true;
            this.upperlbl.BackColor = System.Drawing.Color.Transparent;
            this.upperlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upperlbl.ForeColor = System.Drawing.Color.White;
            this.upperlbl.Location = new System.Drawing.Point(86, 69);
            this.upperlbl.Name = "upperlbl";
            this.upperlbl.Size = new System.Drawing.Size(191, 20);
            this.upperlbl.TabIndex = 12;
            this.upperlbl.Text = "Please Enter the filename";
            // 
            // subjectText
            // 
            this.subjectText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.subjectText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectText.ForeColor = System.Drawing.Color.DimGray;
            this.subjectText.Location = new System.Drawing.Point(35, 128);
            this.subjectText.Name = "subjectText";
            this.subjectText.Size = new System.Drawing.Size(348, 18);
            this.subjectText.TabIndex = 13;
            // 
            // ok_lbl
            // 
            this.ok_lbl.AutoSize = true;
            this.ok_lbl.BackColor = System.Drawing.Color.Transparent;
            this.ok_lbl.Enabled = false;
            this.ok_lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok_lbl.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.ok_lbl.Location = new System.Drawing.Point(17, 2);
            this.ok_lbl.Name = "ok_lbl";
            this.ok_lbl.Size = new System.Drawing.Size(55, 20);
            this.ok_lbl.TabIndex = 14;
            this.ok_lbl.Text = "Create";
            // 
            // Ok_btn
            // 
            this.Ok_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Ok_btn.BackgroundImage")));
            this.Ok_btn.Controls.Add(this.ok_lbl);
            this.Ok_btn.Location = new System.Drawing.Point(304, 178);
            this.Ok_btn.Name = "Ok_btn";
            this.Ok_btn.Size = new System.Drawing.Size(91, 26);
            this.Ok_btn.TabIndex = 15;
            this.Ok_btn.Click += new System.EventHandler(this.Ok_btn_Click);
            this.Ok_btn.MouseEnter += new System.EventHandler(this.Ok_btn_MouseEnter);
            this.Ok_btn.MouseLeave += new System.EventHandler(this.Ok_btn_MouseLeave);
            // 
            // Icon_Add
            // 
            this.Icon_Add.BackColor = System.Drawing.Color.Transparent;
            this.Icon_Add.Image = global::Tytanium.Continuum.Properties.Resources.NewFile;
            this.Icon_Add.Location = new System.Drawing.Point(26, 55);
            this.Icon_Add.Name = "Icon_Add";
            this.Icon_Add.Size = new System.Drawing.Size(45, 45);
            this.Icon_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Icon_Add.TabIndex = 16;
            this.Icon_Add.TabStop = false;
            // 
            // Browse
            // 
            this.Browse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Browse.BackgroundImage")));
            this.Browse.Controls.Add(this.Browse_lbl);
            this.Browse.Location = new System.Drawing.Point(202, 179);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(91, 26);
            this.Browse.TabIndex = 16;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            this.Browse.MouseEnter += new System.EventHandler(this.Browse_MouseEnter);
            this.Browse.MouseLeave += new System.EventHandler(this.Browse_MouseLeave);
            // 
            // Browse_lbl
            // 
            this.Browse_lbl.AutoSize = true;
            this.Browse_lbl.BackColor = System.Drawing.Color.Transparent;
            this.Browse_lbl.Enabled = false;
            this.Browse_lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Browse_lbl.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.Browse_lbl.Location = new System.Drawing.Point(17, 2);
            this.Browse_lbl.Name = "Browse_lbl";
            this.Browse_lbl.Size = new System.Drawing.Size(57, 20);
            this.Browse_lbl.TabIndex = 14;
            this.Browse_lbl.Text = "Browse";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Khaki;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(-35, 214);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(450, 5);
            this.panel3.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Khaki;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(403, -41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 300);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Khaki;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(-31, -41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 300);
            this.panel2.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Khaki;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(-1, -41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(5, 301);
            this.panel4.TabIndex = 23;
            // 
            // NewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(407, 218);
            this.Controls.Add(this.TitlePane);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.Icon_Add);
            this.Controls.Add(this.Ok_btn);
            this.Controls.Add(this.subjectText);
            this.Controls.Add(this.upperlbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "NewFile";
            this.TitlePane.ResumeLayout(false);
            this.TitlePane.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).EndInit();
            this.Ok_btn.ResumeLayout(false);
            this.Ok_btn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_Add)).EndInit();
            this.Browse.ResumeLayout(false);
            this.Browse.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TitlePane;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.PictureBox Minimize;
        private System.Windows.Forms.Label upperlbl;
        private System.Windows.Forms.TextBox subjectText;
        private System.Windows.Forms.Label ok_lbl;
        private System.Windows.Forms.Panel Ok_btn;
        private System.Windows.Forms.PictureBox Icon_Add;
        private System.Windows.Forms.Panel Browse;
        private System.Windows.Forms.Label Browse_lbl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
    }
}