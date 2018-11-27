using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Tytanium.Parser;
using TreeNode = Tytanium.Parser.TreeNode;

namespace Tytanium
{
    public partial class ParserResults : Form
    {
        private Tree Tree;

        public ParserResults(Tree tree)
        {
            Tree = tree;
            InitializeComponent();
        }

        private void TokenTable_Load(object sender, EventArgs e)
        {
            this.Size = new Size(720, 360);
            ConstructTreeView();
            //ParserTreeView.ExpandAll();
        }

        private void ConstructTreeView()
        {
            ParserTreeView.Nodes.Clear();
            if(Tree.Root == null) return;
            ParserTreeView.Nodes.Add(CreateTreeViewNode(Tree.Root));
        }

        private System.Windows.Forms.TreeNode CreateTreeViewNode(TreeNode node)
        {
            if (node==null)
            {
                return new System.Windows.Forms.TreeNode("Parser Error");
            }
            System.Windows.Forms.TreeNode res = new System.Windows.Forms.TreeNode(node.getLabel());
            if (node is NonTerminalTreeNode)
            {
                var nonTerminalNode = node as NonTerminalTreeNode;
                foreach (var child in nonTerminalNode.Children)
                {
                    res.Nodes.Add(CreateTreeViewNode(child));
                }
            }
            return res;
        }
    }
}
