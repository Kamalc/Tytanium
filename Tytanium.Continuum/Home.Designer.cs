using System;
using System.Drawing;
using System.Windows.Forms;
namespace Tytanium.Continuum
{
    partial class Home
    {

        static Color MenuBorderColor = Color.Orange;
        public class ToolstripRenderMagic : ToolStripProfessionalRenderer
        {
            Bitmap Selectionimg = Properties.Resources.MenuSelection;
            TextureBrush SelectionBrush;

            Bitmap BaseImg = Properties.Resources.MenuBackground;
            TextureBrush BaseBrush;

            Pen GoldShade = new Pen(Color.DarkGoldenrod);



            public ToolstripRenderMagic(TestColorTable PTC)
                : base(PTC)
            {
                SelectionBrush = new TextureBrush(Selectionimg);
                BaseBrush = new TextureBrush(BaseImg);
                base.RoundedEdges = true;
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {
                    if (e.Item.IsOnDropDown)
                    {

                        Rectangle rc = new Rectangle(new Point(-1, 0), new Size(3, e.Item.Height));
                        e.Graphics.FillRectangle(BaseBrush, rc);
                    }
                    base.OnRenderMenuItemBackground(e);
                }
                else if (e.Item.Pressed)
                {
                    Rectangle rc = new Rectangle(new Point(-1, 0), new Size(e.Item.Width, e.Item.Height));
                    if (e.Item.IsOnDropDown)
                    {
                        e.Graphics.FillRectangle(SelectionBrush, rc);
                        e.Graphics.DrawRectangle(GoldShade, 0, 0, rc.Width - 1, rc.Height);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(BaseBrush, rc);
                        e.Graphics.DrawRectangle(new Pen(MenuBorderColor), 0, 0, rc.Width - 1, rc.Height);
                    }
                }
                else
                {
                    Rectangle rc = new Rectangle(new Point(2, 0), new Size(e.Item.Size.Width - 2, e.Item.Height));
                    e.Graphics.FillRectangle(SelectionBrush, rc);
                    e.Graphics.DrawRectangle(GoldShade, 1, 0, rc.Width, rc.Height - 1.2f);
                }

            }
        }

        public class TestColorTable : ProfessionalColorTable
        {
            public TestColorTable()
            {
                base.UseSystemColors = false;
            }

            public override Color MenuBorder
            {
                get { return MenuBorderColor; }
                //get { return Color.FromArgb(32, 51, 83); }

            }

            public override Color ToolStripDropDownBackground
            {
                get
                {
                    return Color.Orange;
                }
            }

            public override Color MenuItemBorder
            {
                get { return Color.Orange; }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.Orange; }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(252, 207, 106); }
            }


        }


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.MainPane1 = new System.Windows.Forms.Panel();
            this._mainmenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRealTimeCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.Panel();
            this.StatusScr = new System.Windows.Forms.Label();
            this._mainmenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPane1
            // 
            this.MainPane1.BackColor = System.Drawing.Color.Transparent;
            this.MainPane1.Location = new System.Drawing.Point(-1, 39);
            this.MainPane1.Name = "MainPane1";
            this.MainPane1.Size = new System.Drawing.Size(1270, 600);
            this.MainPane1.TabIndex = 6;
            // 
            // _mainmenu
            // 
            this._mainmenu.AutoSize = false;
            this._mainmenu.BackColor = System.Drawing.Color.Transparent;
            this._mainmenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_mainmenu.BackgroundImage")));
            this._mainmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this._mainmenu.Location = new System.Drawing.Point(0, 0);
            this._mainmenu.Name = "_mainmenu";
            this._mainmenu.Size = new System.Drawing.Size(1270, 36);
            this._mainmenu.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.AutoSize = false;
            this.fileToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.clearRealTimeCacheToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 36);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.AutoSize = false;
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.AutoSize = false;
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.AutoSize = false;
            this.optionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsToolStripMenuItem.Image")));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.AutoSize = false;
            this.buildToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("buildToolStripMenuItem.Image")));
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.buildToolStripMenuItem.Text = "Build..";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.AutoSize = false;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // clearRealTimeCacheToolStripMenuItem
            // 
            this.clearRealTimeCacheToolStripMenuItem.AutoSize = false;
            this.clearRealTimeCacheToolStripMenuItem.Name = "clearRealTimeCacheToolStripMenuItem";
            this.clearRealTimeCacheToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.clearRealTimeCacheToolStripMenuItem.Text = "Clear Real-Time";
            this.clearRealTimeCacheToolStripMenuItem.Click += new System.EventHandler(this.clearRealTimeCacheToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.AutoSize = false;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StatusBar.BackgroundImage")));
            this.StatusBar.Controls.Add(this.StatusScr);
            this.StatusBar.Location = new System.Drawing.Point(0, 639);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1270, 22);
            this.StatusBar.TabIndex = 7;
            // 
            // StatusScr
            // 
            this.StatusScr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusScr.AutoSize = true;
            this.StatusScr.BackColor = System.Drawing.Color.Transparent;
            this.StatusScr.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusScr.ForeColor = System.Drawing.Color.White;
            this.StatusScr.Location = new System.Drawing.Point(4, 3);
            this.StatusScr.Name = "StatusScr";
            this.StatusScr.Size = new System.Drawing.Size(41, 15);
            this.StatusScr.TabIndex = 0;
            this.StatusScr.Text = "Ready";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1270, 661);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainPane1);
            this.Controls.Add(this._mainmenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._mainmenu;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Project Continuum - Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this._mainmenu.ResumeLayout(false);
            this._mainmenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPane1;
        private System.Windows.Forms.MenuStrip _mainmenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem buildToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem clearRealTimeCacheToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Panel StatusBar;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private Label StatusScr;
    }
}