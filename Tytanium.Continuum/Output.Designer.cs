namespace Tytanium.Continuum
{
    partial class Output
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
            this.TitlePane = new System.Windows.Forms.Panel();
            this.close = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ErrorListBox = new System.Windows.Forms.ListBox();
            this.TitlePane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            this.SuspendLayout();
            // 
            // TitlePane
            // 
            this.TitlePane.BackColor = System.Drawing.Color.Transparent;
            this.TitlePane.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.TitlePane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TitlePane.Controls.Add(this.title);
            this.TitlePane.Controls.Add(this.close);
            this.TitlePane.Location = new System.Drawing.Point(-10, -1);
            this.TitlePane.Name = "TitlePane";
            this.TitlePane.Size = new System.Drawing.Size(867, 24);
            this.TitlePane.TabIndex = 10;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.Image = global::Tytanium.Continuum.Properties.Resources.Close;
            this.close.Location = new System.Drawing.Point(820, 1);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(20, 20);
            this.close.TabIndex = 14;
            this.close.TabStop = false;
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
            this.title.Size = new System.Drawing.Size(57, 20);
            this.title.TabIndex = 2;
            this.title.Text = "Output";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Khaki;
            this.panel3.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(5, 268);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(828, 5);
            this.panel3.TabIndex = 25;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Khaki;
            this.panel4.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(5, 273);
            this.panel4.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Khaki;
            this.panel1.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuSelection;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(833, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 273);
            this.panel1.TabIndex = 24;
            // 
            // ErrorListBox
            // 
            this.ErrorListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorListBox.Location = new System.Drawing.Point(3, 21);
            this.ErrorListBox.MultiColumn = true;
            this.ErrorListBox.Name = "ErrorListBox";
            this.ErrorListBox.Size = new System.Drawing.Size(832, 247);
            this.ErrorListBox.TabIndex = 27;
            // 
            // Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tytanium.Continuum.Properties.Resources.MenuBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(838, 273);
            this.Controls.Add(this.TitlePane);
            this.Controls.Add(this.ErrorListBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Output";
            this.Text = "Output";
            this.Shown += new System.EventHandler(this.Output_Shown);
            this.TitlePane.ResumeLayout(false);
            this.TitlePane.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TitlePane;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.ListBox ErrorListBox;
    }
}