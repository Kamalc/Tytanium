using System.Collections.Generic;
using Tytanium.Scanner;

namespace Tytanium.Parser
{



    public abstract class TreeNode
    {
        public Dictionary<Registrar.Attribute, object> Attributes = new Dictionary<Registrar.Attribute, object>();


        protected string label;

        public abstract string getLabel();

        public bool Composite=false;
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

        public string getLiteral()
        {
            return Token.Literal;
        }

        public override string getLabel()
        {
            return label + " : " + Token.Literal;
        }
    }

    public class Tree
    {
        public TreeNode Root;

        public Tree(TreeNode root)
        {
            Root = root;
        }

    }
}
