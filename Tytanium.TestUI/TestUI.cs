using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tytanium.Scanner;
using System.Runtime.InteropServices;

namespace Tytanium
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

    public partial class TestUI : Form
    {

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public TestUI()
        {
            InitializeComponent();
        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scanner.Scanner S = new Scanner.Scanner(CodeBox1.Text);
            S.Scan();
            TokenTable T = new TokenTable(S.Tokens);
            T.Show();
            ErrorList EL = new ErrorList(S.ErrorList);
            EL.Show();
        }

        private void TestUI_Load(object sender, EventArgs e)
        {
            this.Location = new Point(50, 100);

            for (int i=1;i<=200;i++)
            {
                LineNoBox.Text += i.ToString() + "\n";
            }
        }

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
            if (CodeBox1.Text.Length-CharCount>3)
            {
                TextBox T=new TextBox();
                T.Text = CodeBox1.Text;
                CodeBox1.Clear();
                CodeBox1.Text = T.Text;
                CharCount = CodeBox1.Text.Length;
                
            }
            else if (CodeBox1.Text.Length-CharCount>0)
                CharCount++;
            else if (CodeBox1.Text.Length - CharCount < 0)
                CharCount--;
        }
    }
}
