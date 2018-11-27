using System;
using System.Collections.Generic;
using System.Linq;
using Handler;
using Tytanium.Scanner;

namespace Tytanium.Parser
{
    public class Parser
    {
        delegate TreeNode ParserFn();

        Dictionary<Refrence.Class, ParserFn> Parsers = new Dictionary<Refrence.Class, ParserFn>();

        private List<Token> _tokens;
        public List<Error> Errors;
        private int currentToken;



        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            Errors = new List<Error>();
            currentToken = 0;

            //Intializing parser refrence dictionary
            Parsers = new Dictionary<Refrence.Class, ParserFn>()
            {
                //Declarations
                {Refrence.Class.DataType_int,declaration_stmt },
                {Refrence.Class.DataType_float,declaration_stmt },
                {Refrence.Class.DataType_string,declaration_stmt },

                //Branching Agents
                {Refrence.Class.BranchingAgent_if, if_stmt},

                //Hardcoded directives
                {Refrence.Class.Directive_read,read_stmt },
                {Refrence.Class.Directive_write,write_stmt },

                //Looping Agents
                {Refrence.Class.LoopBound_repeat, repeat_stmt },

                //Others
                {Refrence.Class.Assignment_Identifier, identifier_call },
                {Refrence.Class.AssignmentOperator,assign_stmt },
                {Refrence.Class.LeftBracket,function_sig },
                {Refrence.Class.LeftCurlyBrace,function_body },
            };
        }

        private TreeNode identifier_call()
        {
            NonTerminalTreeNode identifierNode = new NonTerminalTreeNode("Runtime Statment");
            identifierNode.append_child(new TerminalTreeNode("Calling Identifier",_tokens[currentToken]));
            currentToken++;
            identifierNode.append_child(statement());
            return identifierNode;
        }

        private TreeNode program()
        {
            NonTerminalTreeNode prog_root = new NonTerminalTreeNode("Program Syntatic structure");
            while (currentToken < _tokens.Count)
            {
                prog_root.append_child(stmt_sequence());
            }
            return prog_root;
        }

        private TreeNode stmt_sequence()
        {
            NonTerminalTreeNode seq_root = new NonTerminalTreeNode("Statements sequence");

            //while (currentToken != _tokens.Count && _tokens[currentToken].Type != Refrence.Class.BranchingAgent_end &&
            //       _tokens[currentToken].Type != Refrence.Class.BranchingAgent_else &&
            //       _tokens[currentToken].Type != Refrence.Class.LoopBound_until)
            while (currentToken != _tokens.Count)
            {
                TreeNode child = statement();
                if (child != null)
                {
                    seq_root.append_child(child);
                }
            }
            return seq_root;
        }

        private TreeNode statement()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Statement");

            TreeNode child = null;

            if (currentToken == _tokens.Count)
                return null;

            if (_tokens[currentToken].UpperType == Refrence.UpperClass.Comment)
            {
                TerminalTreeNode Nodex = new TerminalTreeNode("Comment", _tokens[currentToken]);
                currentToken++;
                return Nodex;
            }

            if (Parsers.Keys.Contains(_tokens[currentToken].Type))
            {
                child = Parsers[_tokens[currentToken].Type]();
            }
            else
            {
                Errors.Add(new Error("Unexpected token " + _tokens[currentToken].Literal, Error.ErrorType.ParserError));
            }

            if (child != null)
                root.append_child(child);

            if (currentToken != _tokens.Count && _tokens[currentToken].Type == Refrence.Class.SemiColon)
            {
                currentToken++;
            }
            return root;
        }

        private TreeNode declaration_stmt()
        {
            NonTerminalTreeNode dec_node = new NonTerminalTreeNode("Declaration Statment");
            dec_node.append_child(new TerminalTreeNode("Datatype", _tokens[currentToken]));
            currentToken++;
            if (!match(Refrence.Class.Assignment_Identifier))
                return dec_node;

            dec_node.append_child(new TerminalTreeNode("Identifier", _tokens[currentToken]));
            currentToken++; 

            return dec_node;
        }

        private TreeNode function_sig()
        {
            currentToken--;
            NonTerminalTreeNode fnHeader = new NonTerminalTreeNode("Function signature");
            if (match(Refrence.Class.Assignment_Identifier))
                fnHeader.append_child(new TerminalTreeNode("Function Nsmr", _tokens[currentToken]));
            currentToken++;
            while (true)
            {
                fnHeader.append_child(declaration_stmt());

                if (_tokens[currentToken].Type == Refrence.Class.RightBracket || !match(Refrence.Class.Comma))
                {
                    break;
                }
            }
            currentToken++;
            return fnHeader;
        }

        private TreeNode function_body()
        {
            NonTerminalTreeNode fnBody = new NonTerminalTreeNode("Function body");
            currentToken++;

            while (currentToken != _tokens.Count)
            {
                if (_tokens[currentToken].Type == Refrence.Class.RightCurlyBrace)
                {
                    currentToken++;
                    return fnBody;
                }
                else if (_tokens[currentToken].Type == Refrence.Class.Directive_return)
                {
                    NonTerminalTreeNode returnCode = new NonTerminalTreeNode("Return Statment");
                    returnCode.append_child(new TerminalTreeNode("return directive", _tokens[currentToken]));
                    currentToken++;
                    returnCode.append_child(exp());
                    match(Refrence.Class.SemiColon);
                    currentToken++;
                    continue;
                }

                TreeNode child = statement();
                if (child != null)
                {
                    fnBody.append_child(child);
                }
            }
            return fnBody;

        }

        public Tree parse()
        {
            if (_tokens == null) return null;
            if (_tokens.Count == 0)  return new Tree(null);
            currentToken = 0;
            return new Tree(program());
        }

        private bool match(Refrence.Class expected)
        {
            if (currentToken < _tokens.Count && _tokens[currentToken].Type == expected)
                return true;
            else
            {
                if (currentToken >= _tokens.Count)
                    Errors.Add(new Error("Expected a " + expected + " but reached End Of File!",
                        Error.ErrorType.ParserError));
                else
                    Errors.Add(new Error("Expected a " + expected + " but a " + _tokens[currentToken].Type + " was found!",
                        Error.ErrorType.ParserError));
            }
            return false;
        }

        private TreeNode if_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("If statement");
            match(Refrence.Class.BranchingAgent_if);
            root.append_child(new TerminalTreeNode("If Intiator",_tokens[currentToken]));
            currentToken++;
            root.append_child(exp());
            match(Refrence.Class.BranchingAgent_then);
            currentToken++;
            int ElsesCount = 1;
            NonTerminalTreeNode seQ = new NonTerminalTreeNode("If Body");
            while (_tokens[currentToken].Type != Refrence.Class.BranchingAgent_end)
            {
                if (_tokens[currentToken].Type == Refrence.Class.BranchingAgent_elseIf)
                {
                    if (ElsesCount==0)
                    {
                        Errors.Add(new Error("An ElseIf Scope can not be opened following an else scope",Error.ErrorType.ParserError));
                    }
                    currentToken++;
                    root.append_child(seQ);
                    seQ = new NonTerminalTreeNode("Else If#" + ElsesCount++.ToString() +  "Body");
                }

                if (_tokens[currentToken].Type == Refrence.Class.BranchingAgent_else)
                {
                    if (ElsesCount == 0)
                    {
                        Errors.Add(new Error("A single if can not contain 2 else scopes", Error.ErrorType.ParserError));
                    }
                    ElsesCount = 0;
                    currentToken++;
                    root.append_child(seQ);
                    seQ = new NonTerminalTreeNode("Else Body");
                }
                seQ.append_child(statement());
            }
            root.append_child(seQ);
            match(Refrence.Class.BranchingAgent_end);
            currentToken++;
            return root;
        }

        private TreeNode repeat_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Loop (Repeat)");
            match(Refrence.Class.LoopBound_repeat);
            currentToken++;
            NonTerminalTreeNode seQ = new NonTerminalTreeNode("Body");

            while (_tokens[currentToken].Type!=Refrence.Class.LoopBound_until)
            {
                seQ.append_child(statement());
            }

            root.append_child(seQ);

            match(Refrence.Class.LoopBound_until);
            currentToken++;
            root.append_child(exp());
            return root; 
        }

        private TreeNode write_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Write");
            match(Refrence.Class.Directive_write);
            currentToken++;
            root.append_child(exp());
            return root;
        }

        private TreeNode read_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Read");
            match(Refrence.Class.Directive_read);
            currentToken++;
            root.append_child(exp());
            return root;
        }

        private TreeNode assign_stmt()
        {
            currentToken--;
            NonTerminalTreeNode root = new NonTerminalTreeNode("Assign");
            if (_tokens[currentToken].Type == Refrence.Class.Assignment_Identifier)
            {
                root.append_child(new TerminalTreeNode("Identifier", _tokens[currentToken]));
                match(Refrence.Class.Assignment_Identifier);
                currentToken++;
            }
            match(Refrence.Class.AssignmentOperator);
            currentToken++;
            root.append_child(exp());
            return root;
        }

        private TreeNode exp()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Expression");
            root.append_child(simple_exp());
            if (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ComparisonOperatorLessThan ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorEQ ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorGreaterThan ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorNQ))
            {
                root.append_child(new TerminalTreeNode("Operator", _tokens[currentToken]));
                match(_tokens[currentToken].Type);
                currentToken++;
                root.append_child(simple_exp());
            }

            return root;
        }

        private TreeNode simple_exp()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Simple expression");
            root.append_child(term());
            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ArithmeticAddition ||
                   _tokens[currentToken].Type == Refrence.Class.Arithmeticsubtraction ||
                   _tokens[currentToken].Type == Refrence.Class.ArithmeticsubtractionOperator2))
            {
                TreeNode operatorNode = new TerminalTreeNode("Operator", _tokens[currentToken]);
                match(_tokens[currentToken].Type);
                currentToken++;
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
            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ArithmeticMultiplication ||
                   _tokens[currentToken].Type == Refrence.Class.ArithmeticDivision))
            {
                TreeNode operatorNode = new TerminalTreeNode("Operator", _tokens[currentToken]);
                match(_tokens[currentToken].Type);
                currentToken++;
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
            switch (_tokens[currentToken].Type)
            {
                case Refrence.Class.DataType_int:
                case Refrence.Class.DataType_float:
                case Refrence.Class.DataType_string:
                    root = new TerminalTreeNode("Constant", _tokens[currentToken]);
                    match(_tokens[currentToken].Type);
                    currentToken++;
                    break;
                case Refrence.Class.Assignment_Identifier:
                    root = new TerminalTreeNode("Identifier", _tokens[currentToken]);
                    match(_tokens[currentToken].Type);
                    currentToken++;
                    break;
                case Refrence.Class.LeftBracket:
                    match(Refrence.Class.LeftBracket);
                    currentToken++;
                    root = exp();
                    match(Refrence.Class.RightBracket);
                    currentToken++;
                    break;
                default:
                    Errors.Add(new Error("Unexpected token " + _tokens[currentToken].Literal, Error.ErrorType.ParserError));
                    currentToken++;
                    break;
            }
            return root;
        }
    }
}
