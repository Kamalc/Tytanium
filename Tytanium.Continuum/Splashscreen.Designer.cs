using System;

namespace Tytanium.Continuum
{
    partial class Splashscreen
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
            this.AnimationBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AnimationBox
            // 
            this.AnimationBox.Image = global::Tytanium.Continuum.Properties.Resources.SEQ0001;
            this.AnimationBox.Location = new System.Drawing.Point(1, 1);
            this.AnimationBox.Name = "AnimationBox";
            this.AnimationBox.Size = new System.Drawing.Size(480, 240);
            this.AnimationBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.AnimationBox.TabIndex = 1;
            this.AnimationBox.TabStop = false;
            this.AnimationBox.UseWaitCursor = true;
            // 
            // Splashscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(482, 242);
            this.Controls.Add(this.AnimationBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splashscreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AnimationBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AnimationBox;
    }
}

