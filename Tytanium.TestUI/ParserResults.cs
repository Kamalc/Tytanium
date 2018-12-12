using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
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
            //this.Size = new Size(720, 360);
            this.Size = new Size(530, 500);
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
            if (node == null)
            {
                return new System.Windows.Forms.TreeNode("Parser Error");
            }
            System.Windows.Forms.TreeNode res = new System.Windows.Forms.TreeNode(node.getLabel());
            System.Windows.Forms.TreeNode attribs = new System.Windows.Forms.TreeNode("CompilerAttributes");

            bool empty = true;

            if (node.Attributes.ContainsKey(Parser.Attribute.Value))
            {
                empty = false;
                string Temp = "";
                foreach (var i in ((List<string>)node.Attributes[Parser.Attribute.Value]))
                {
                    Temp += i + " ";
                }
                attribs.Nodes.Add(Temp);
            }

            if (node.Attributes.ContainsKey(Parser.Attribute.Datatype))
            {
                empty = false;
                attribs.Nodes.Add(((Datatype)node.Attributes[Parser.Attribute.Datatype]).ToString());
            }

            if (!empty)
            {
                res.Nodes.Add(attribs);
            }


            foreach (var child in node.Children)
            {
                res.Nodes.Add(CreateTreeViewNode(child));
            }

            return res;
        }
    }
}
