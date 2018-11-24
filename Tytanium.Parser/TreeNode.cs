using System.Collections.Generic;
using Tytanium.Scanner;

namespace Tytanium.Parser
{
    public abstract class TreeNode
    {
        protected string label;

        public abstract string getLabel();

        public abstract string  print_dot(int id, ref int maxId);
    }

    public class NonTerminalTreeNode : TreeNode
    {
        private NonTerminalTreeNode()
        {
            label = "";
        }

        public NonTerminalTreeNode(string label)
        {
            this.label = label;
        }

        public readonly List<TreeNode> Children = new List<TreeNode>();

        public override string print_dot(int id, ref int maxId)
        {
            string res = "\tNode" + id + "[label=\"" + getLabel() + "\"];\n";
            foreach(var child in Children)
            {
                int childId = ++maxId;
                res += child.print_dot(childId, ref maxId);
                res += "\tNode" + id + " -> Node" + childId + "[dir=none];\n";
            }
            return res;
        }

        public override string getLabel()
        {
            return label;
        }

        public void append_child(TreeNode child)
        {
            Children.Add(child);
        }
    }

    public class TerminalTreeNode : TreeNode
    {
        public Token Token;
        private TerminalTreeNode()
        {
            label = "";
        }

        public TerminalTreeNode(string label, Token token)
        {
            this.label = label;
            Token = token;
        }

        public override string getLabel()
        {
            return label + " : " + Token.Literal;
        }

        public override string print_dot(int id, ref int maxId)
        {
            return "\tNode" + id + "[label=\"" + getLabel() +  "\"];\n";
        }
    }

    public class Tree
    {
        public TreeNode Root;

        public Tree(TreeNode root)
        {
            Root = root;
        }

        public string print_dot()
        {
            string res = "digraph G {\n\tnode[shape=plaintext];\n" ;
            if (Root != null)
            {
                int maxid = 0;
                res += Root.print_dot(0, ref maxid);
            }
            res += "}\n";
            return res;
        }

    }
}
