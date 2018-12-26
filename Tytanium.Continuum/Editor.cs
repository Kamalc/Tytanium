using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Tytanium.Continuum
{
    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }

    public enum Message : uint
    {
        WM_VSCROLL = 0x0115
    }

    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }
    public partial class Editor : Form
    {
        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


        private void CodeBox1_VScroll(object sender, EventArgs e)
        {
            int nPos = GetScrollPos(CodeBox1.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(LineNoBox.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }

        int CharCount = 0;

        private void CodeBox1_TextChanged(object sender, EventArgs e)
        {
            if (CodeBox1.Text.Length - CharCount > 3)
            {
                TextBox T = new TextBox();
                T.Text = CodeBox1.Text;
                CodeBox1.Clear();
                CodeBox1.Text = T.Text;
                CharCount = CodeBox1.Text.Length;

            }
            else if (CodeBox1.Text.Length - CharCount > 0)
                CharCount++;
            else if (CodeBox1.Text.Length - CharCount < 0)
                CharCount--;
        }


        string titleStr;
        string Code;
        Home Hx;
        public Editor(Home H, string s, string titlex)
        {
            this.Owner = H;
            Hx = H;
            this.TopLevel = false;
            Code = s;
            this.Show();
            InitializeComponent();
            titleStr = titlex;
            this.Text = titleStr;

        }
        public Editor(Home H, string titlex)
        {
            this.Owner = H;
            Hx = H;
            this.TopLevel = false;
            this.Show();
            InitializeComponent();
            titleStr = titlex;
            this.Text = titleStr;
        }


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
                Hx.ChangeActiveEditor(this);
            }
        }

        private void Maximize_Click(object sender, EventArgs e)
        {
            if (this.FindForm().WindowState == FormWindowState.Maximized)
            {
                this.FindForm().WindowState = FormWindowState.Normal;
            }
            else
            {
                this.FindForm().WindowState = FormWindowState.Maximized;
            }
        }

        private void Maximize_MouseEnter(object sender, EventArgs e)
        {
            Maximize.Image = Properties.Resources.MaximizedHighlight;
        }

        private void Maximize_MouseLeave(object sender, EventArgs e)
        {
            Maximize.Image = Properties.Resources.Maximized;
        }

        private void Editor_Shown(object sender, EventArgs e)
        {
            title.Text = titleStr;
            CodeBox1.Text = Code;
            Hx.ChangeActiveEditor(this);
            for (int i = 1; i <= 200; i++)
            {
                LineNoBox.Text += i + "\n";
            }
            LineNoBox.Enabled = false;
            title.Text = titleStr;
            this.Text = titleStr;
        }

        public string getCode()
        {
            return CodeBox1.Text;
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
    }
}
