using System.Collections.Generic;
using Handler;
using Tytanium.Scanner;

namespace Tytanium.Parser
{
    public class Parser
    {
        private List<Token> _tokens;
        public List<Error> Errors;
        private int tokenId;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            Errors = new List<Error>();
            tokenId = 0;
        }

        public Tree parse()
        {
            if (_tokens == null) return null;
            if (_tokens.Count == 0)
            {
                return new Tree(null);
            }
            tokenId = 0;
            return new Tree(stmt_sequence());
        }

        private void match(Refrence.Class expected)
        {
            if (tokenId < _tokens.Count && _tokens[tokenId].Type == expected) tokenId++;
            else
            {
                if (tokenId >= _tokens.Count)
                    Errors.Add(new Error("Expected a " + expected + " but reached End Of File!",
                        Error.ErrorType.ParserError));
                else
                    Errors.Add(new Error("Expected a " + expected + " but a " + _tokens[tokenId].Type + " was found!",
                        Error.ErrorType.ParserError));
            }
        }

        private TreeNode stmt_sequence()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Statements sequence");
            bool firstStatement = true;
            while (tokenId != _tokens.Count && _tokens[tokenId].Type != Refrence.Class.BranchingAgent_end &&
                   _tokens[tokenId].Type != Refrence.Class.BranchingAgent_else &&
                   _tokens[tokenId].Type != Refrence.Class.LoopBound_until)
            {
                if(!firstStatement) match(Refrence.Class.SemiColon);
                firstStatement = false;
                TreeNode child = statement();
                if (child != null)
                {
                    root.append_child(child);
                }
            }
            return root;
        }

        private TreeNode statement()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Statement");
            TreeNode child = null;
            if (tokenId == _tokens.Count) return null;
            switch (_tokens[tokenId].Type)
            {
                case Refrence.Class.BranchingAgent_if:
                    child = if_stmt();
                    break;
                case Refrence.Class.LoopBound_repeat:
                    child = repeat_stmt();
                    break;
                case Refrence.Class.Directive_write:
                    child = write_stmt();
                    break;
                case Refrence.Class.Directive_read:
                    child = read_stmt();
                    break;
                case Refrence.Class.Assignment_Identifier:
                    child = assign_stmt();
                    break;
                default:
                    Errors.Add(new Error("Unexpected token " + _tokens[tokenId].Literal, Error.ErrorType.ParserError));
                    tokenId++;
                    break;
            }

            if(child != null)
                root.append_child(child);
            return root;
        }

        private TreeNode if_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("If statement");
            match(Refrence.Class.BranchingAgent_if);
            root.append_child(exp());
            match(Refrence.Class.BranchingAgent_then);
            root.append_child(stmt_sequence());
            if (_tokens[tokenId].Type == Refrence.Class.BranchingAgent_else)
            {
                match(Refrence.Class.BranchingAgent_else);
                root.append_child(stmt_sequence());
            }
            match(Refrence.Class.BranchingAgent_end);
            return root;    
        }

        private TreeNode repeat_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Repeat statement");
            match(Refrence.Class.LoopBound_repeat);
            root.append_child(stmt_sequence());
            match(Refrence.Class.LoopBound_until);
            root.append_child(exp());
            return root; 
        }

        private TreeNode write_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Write");
            match(Refrence.Class.Directive_write);
            root.append_child(exp());
            return root;
        }

        private TreeNode read_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Read");
            match(Refrence.Class.Directive_read);
            root.append_child(exp());
            return root;
        }

        private TreeNode assign_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Assign");
            if (_tokens[tokenId].Type == Refrence.Class.Assignment_Identifier)
            {
                root.append_child(new TerminalTreeNode("Identifier", _tokens[tokenId]));
                match(Refrence.Class.Assignment_Identifier);
            }
            match(Refrence.Class.AssignmentOperator);
            root.append_child(exp());
            return root;
        }

        private TreeNode exp()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Expression");
            root.append_child(simple_exp());
            if (tokenId < _tokens.Count && (_tokens[tokenId].Type == Refrence.Class.ComparisonOperatorLessThan ||
                _tokens[tokenId].Type == Refrence.Class.ComparisonOperatorEQ ||
                _tokens[tokenId].Type == Refrence.Class.ComparisonOperatorGreaterThan ||
                _tokens[tokenId].Type == Refrence.Class.ComparisonOperatorNQ))
            {
                root.append_child(new TerminalTreeNode("Operator", _tokens[tokenId]));
                match(_tokens[tokenId].Type);
                root.append_child(simple_exp());
            }

            return root;
        }

        private TreeNode simple_exp()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Simple expression");
            root.append_child(term());
            while (tokenId < _tokens.Count && (_tokens[tokenId].Type == Refrence.Class.ArithmeticAddition ||
                   _tokens[tokenId].Type == Refrence.Class.Arithmeticsubtraction ||
                   _tokens[tokenId].Type == Refrence.Class.ArithmeticsubtractionOperator2))
            {
                TreeNode operatorNode = new TerminalTreeNode("Operator", _tokens[tokenId]);
                match(_tokens[tokenId].Type);
                TreeNode child = term();
                if (root.Children.Count == 2)
                {
                    NonTerminalTreeNode leftChild = root;
                    root = new NonTerminalTreeNode("Simple expression");
                    root.append_child(leftChild);
                    root.append_child(operatorNode);
                    root.append_child(child);
                }
                else
                {
                    root.append_child(operatorNode);
                    root.append_child(child);
                }
            }

            return root;
        }

        private TreeNode term()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Term");
            root.append_child(factor());
            while (tokenId < _tokens.Count && (_tokens[tokenId].Type == Refrence.Class.ArithmeticMultiplication ||
                   _tokens[tokenId].Type == Refrence.Class.ArithmeticDivision))
            {
                TreeNode operatorNode = new TerminalTreeNode("Operator", _tokens[tokenId]);
                match(_tokens[tokenId].Type);
                TreeNode child = factor();
                if (root.Children.Count == 2)
                {
                    NonTerminalTreeNode leftChild = root;
                    root = new NonTerminalTreeNode("Term");
                    root.append_child(leftChild);
                    root.append_child(operatorNode);
                    root.append_child(child);
                }
                else
                {
                    root.append_child(operatorNode);
                    root.append_child(child);
                }
            }
            return root;
        }

        private TreeNode factor()
        {
            TreeNode root = null;
            switch (_tokens[tokenId].Type)
            {
                case Refrence.Class.DataType_int:
                case Refrence.Class.DataType_float:
                case Refrence.Class.DataType_string:
                    root = new TerminalTreeNode("Constant", _tokens[tokenId]);
                    match(_tokens[tokenId].Type);
                    break;
                case Refrence.Class.Assignment_Identifier:
                    root = new TerminalTreeNode("Identifier", _tokens[tokenId]);
                    match(_tokens[tokenId].Type);
                    break;
                case Refrence.Class.LeftBracket:
                    match(Refrence.Class.LeftBracket);
                    root = exp();
                    match(Refrence.Class.RightBracket);
                    break;
                default:
                    Errors.Add(new Error("Unexpected token " + _tokens[tokenId].Literal, Error.ErrorType.ParserError));
                    tokenId++;
                    break;
            }
            return root;
        }
    }
}
