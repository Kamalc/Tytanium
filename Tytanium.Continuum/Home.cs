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
using Tytanium.Parser;

namespace Tytanium.Continuum
{
    public partial class Home : Form
    {

        public Dictionary<int, Editor> EditForms = new Dictionary<int, Editor>();
        Editor ActiveEditor;
        LiveTree Preview;

        public void ChangeActiveEditor(Editor E)
        {
            ActiveEditor = E;
        }

        public void AddControltomainPane(Editor E)
        {
            MainPane1.Controls.Add(E);
        }

        public void UpdateStatusScreen(string str)
        {
            StatusScr.Text = str;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Add(new NewFile(this));
            Controls[Controls.Count - 1].BringToFront();
        }
        public Home()
        {
            InitializeComponent();
            this._mainmenu.Renderer = new ToolstripRenderMagic(new TestColorTable());
            foreach (ToolStripDropDownItem Item in _mainmenu.Items)
            {
                Item.ForeColor = Color.DarkOrange;
                Item.DropDown.AutoSize = false;
                Item.DropDown.Size = new Size(156, Item.DropDown.Height);
                Item.DropDown.BackColor = Color.DarkGray/*.FromArgb(31, 83, 133);*/;
                Item.DropDown.DropShadowEnabled = false;
                Item.DropDown.Padding = new Padding(0);
                foreach (ToolStripItem It in Item.DropDownItems)
                {
                    It.AutoSize = false;
                    It.BackgroundImage = Properties.Resources.MenuBackground;
                    It.ForeColor = Color.DarkOrange;
                    It.Size = new Size(156, It.Height);
                }
            }
            Preview = new LiveTree(this);
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Add(new NewFile(this, true));
            Controls[Controls.Count - 1].BringToFront();
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Handler.Error> Errors = new List<Handler.Error>();
            if (ActiveEditor == null)
                return;
            Scanner.Scanner S = new Scanner.Scanner(ActiveEditor.getCode());
            S.Scan();
            if (S.ErrorList.Count > 0)
            {
                Errors.AddRange(S.ErrorList);
            }
            Parser.Parser parser = new Parser.Parser(S.Tokens);
            Tree parseTree = parser.parse();
            if (parser.ErrorList.Count > 0)
            {
                Errors.AddRange(S.ErrorList);
            }
            Preview.UpdateTree(parseTree);
            Preview.showPane(false);
            if (Errors.Count > 0)
            {
                Preview.showPane(true);
                Output ErrorsWin = new Output(Errors);
                Controls.Add(ErrorsWin);
                ErrorsWin.BringToFront();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void clearRealTimeCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Controls.Add(Preview);
        }
    }
}
