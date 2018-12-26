using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tytanium.Continuum
{
    public partial class NewFile : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void TitlePane_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.FindForm().WindowState == FormWindowState.Normal)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Minimize_MouseEnter(object sender, EventArgs e)
        {
            Minimize.Image = Properties.Resources.MinimizeHighlight;
        }

        private void Minimize_MouseLeave(object sender, EventArgs e)
        {
            Minimize.Image = Properties.Resources.Minimize;
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.FindForm().WindowState = FormWindowState.Minimized;
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            close.Image = Properties.Resources.CloseHighlight;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            close.Image = Properties.Resources.Close;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        bool OpenFile;

        public NewFile(Home H)
        {
            this.Owner = H;
            this.TopLevel = false;
            Parent = H;
            Hx = H;
            InitializeComponent();
            this.Show();
            OpenFile = false;
            this.Text = "Create new code file";
            this.Location = new Point(100, 100);
        }

        Home Hx;
        public NewFile(Home H,bool openFile)
        {
            this.Owner = H;
            this.TopLevel = false;
            Parent = H;
            Hx = H;

            InitializeComponent();
            this.Show();

            this.Text = "Open a code file";
            title.Text = "Open a code file";
            upperlbl.Text = "Please Select the file you wish to open";
            ok_lbl.Text = "Open";
            Icon_Add.Image = Properties.Resources.Open;
            OpenFile = true;
            this.Location = new Point(150, 150);
        }

        string Dir="";

        private void Ok_btn_MouseEnter(object sender, EventArgs e)
        {
            Ok_btn.BackgroundImage = Properties.Resources.MenuSelection;
        }

        private void Ok_btn_MouseLeave(object sender, EventArgs e)
        {
            Ok_btn.BackgroundImage = Properties.Resources.MenuBarPattern3;
        }

        private void Ok_btn_Click(object sender, EventArgs e)
        {
            if (OpenFile && subjectText.Text!="")
            {
                string data=System.IO.File.ReadAllText(subjectText.Text);
                Hx.AddControltomainPane(new Editor(Hx, data,subjectText.Text.Split('\\').Last()));
            }
            else if (!OpenFile)
            {
                Hx.AddControltomainPane(new Editor(Hx, subjectText.Text.Split('\\').Last()));
            }
            this.Close();
        }

        private void Browse_MouseEnter(object sender, EventArgs e)
        {
            Browse.BackgroundImage = Properties.Resources.MenuSelection;
        }

        private void Browse_MouseLeave(object sender, EventArgs e)
        {
            Browse.BackgroundImage = Properties.Resources.MenuBarPattern3;
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (OpenFile)
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Filter = "Tiny Code File|*.txt;*.tiny;";
                OFD.InitialDirectory=@"C:\Users\Moataz\Projects\Jigsaw Project\Test Cases";
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    subjectText.Text = OFD.FileName;
                }
            }
            else
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "Tiny Code File|*.txt;*.tiny;";
                SFD.AddExtension = true;
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    subjectText.Text = SFD.FileName;
                }
            }
        }
    }
}
