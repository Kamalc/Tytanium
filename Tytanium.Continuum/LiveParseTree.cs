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
using Tytanium.Scanner;
using Tytanium.Parser;
using TreeNode = Tytanium.Parser.TreeNode;

namespace Tytanium.Continuum
{
    public partial class LiveTree: Form
    {
        private Tree Tree;
        Dictionary<System.Windows.Forms.TreeNode, string> Datatypes = new Dictionary<System.Windows.Forms.TreeNode, string>();
        Dictionary<System.Windows.Forms.TreeNode, string> Values = new Dictionary<System.Windows.Forms.TreeNode, string>();

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        Home Hx;
        public LiveTree(Home H)
        {
            this.Owner = H;
            Hx = H;
            this.TopLevel = false;
            this.Show();
            InitializeComponent();
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

        private void LiveTree_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            Rectangle WA = Screen.GetWorkingArea(Hx);
            Location = new Point(WA.Width-Width-30,WA.Height-Height-80);
        }

        private void ConstructTreeView()
        {
            ParserTreeView.Nodes.Clear();
            if (Tree.Root == null) return;
            ParserTreeView.Nodes.Add(CreateTreeViewNode(Tree.Root));
        }

        private System.Windows.Forms.TreeNode CreateTreeViewNode(TreeNode node)
        {
            if (node == null)
            {
                return new System.Windows.Forms.TreeNode("Parser Error");
            }
            System.Windows.Forms.TreeNode res = new System.Windows.Forms.TreeNode(node.getLabel());

            if (node.Attributes.ContainsKey(Parser.Attribute.Value))
            {
                string Temp = "";
                foreach (var i in ((List<string>)node.Attributes[Parser.Attribute.Value]))
                {
                    Temp += i + " ";
                }
                Values.Add(res, Temp);
            }
            else
            {
                Values.Add(res, "N/A");
            }

            if (node.Attributes.ContainsKey(Parser.Attribute.Datatype))
            {
                Datatypes.Add(res, ((Datatype)node.Attributes[Parser.Attribute.Datatype]).ToString());
            }
            else
            {
                Datatypes.Add(res, "N/A");
            }


            foreach (var child in node.Children)
            {
                res.Nodes.Add(CreateTreeViewNode(child));
            }

            return res;
        }

        public void UpdateTree(Tree Entry)
        {
            Tree = Entry;
            if (Tree.Root == null) return;
            ParserTreeView.Nodes.Add(CreateTreeViewNode(Tree.Root));
        }

        public void showPane(bool Input)
        {
            panel4.Visible = Input;
        }

        private void ParserTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ValueBox.Text = Values[e.Node];
            DatatypeBox.Text = Datatypes[e.Node];
        }
    }
}
