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
            ConstructTreeView();
            ConstructTreeImage();
        }

        private void ConstructTreeView()
        {
            ParserTreeView.Nodes.Clear();
            if(Tree.Root == null) return;
            ParserTreeView.Nodes.Add(CreateTreeViewNode(Tree.Root));
        }

        private System.Windows.Forms.TreeNode CreateTreeViewNode(TreeNode node)
        {
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

        private void ConstructTreeImage()
        {
            string inputFilename = "tree" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".dot";
            string dotOutput = Tree.print_dot();
            File.WriteAllText(inputFilename, dotOutput);
            string outputFilename = inputFilename + ".png";
            Process p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    FileName = @"D:\Program Files (x86)\Graphviz2.38\bin\dot.exe",
                    Arguments = "-Tpng -O " + inputFilename
                }
            };
            p.Start();
            p.WaitForExit();
            ParserTreeImage.Image = new Bitmap(outputFilename);
        }
    }
}
