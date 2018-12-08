using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Handler;
using Tytanium.Scanner;

namespace Tytanium.Parser
{
    public class Parser
    {
        Registrar Rx = new Registrar();

        delegate TreeNode ParserFn(); //Parser Delegation

        Dictionary<Refrence.Class, ParserFn> Parsers = new Dictionary<Refrence.Class, ParserFn>();

        List<Refrence.Class> Macros = new List<Refrence.Class>() //Things that will get replaced during semantic analysis Endl=>\n\r
        {
            Refrence.Class.Macro_endl
        };

        private List<Token> _tokens;
        public List<Error> ErrorList;
        private int currentToken;



        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            ErrorList = new List<Error>();
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

        //Invoked in case of identifier intiating statment variable := or variable(.)...etc
        //May be used in operator overloading and dot operator functions
        //Applications are limitless
        private TreeNode identifier_call() 
        {
            NonTerminalTreeNode identifierNode = new NonTerminalTreeNode("Runtime Statment");
            identifierNode.append_child(new TerminalTreeNode("Calling Identifier", _tokens[currentToken]));
            
            //Attributes Registerar
            identifierNode.Children[0].Attributes[Registrar.Attribute.Datatype] = Rx.Variables[_tokens[currentToken].Literal];

            currentToken++;
            identifierNode.append_child(statement());

            identifierNode.Composite = true;
            return identifierNode;
        }

        //Program Syntitcal tree outer structure
        //Recursion intiator function
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

        //Linear statment parsing
        private TreeNode statement()
        {
            TreeNode root = new NonTerminalTreeNode("Statement");

            if (currentToken == _tokens.Count)
                return null;

            //Comment Ad-Hoc model, will be removed in V2.0
            if (_tokens[currentToken].UpperType == Refrence.UpperClass.Comment)
            {
                TerminalTreeNode Nodex = new TerminalTreeNode("Comment", _tokens[currentToken]);
                currentToken++;
                return Nodex;
            }

            if (Parsers.Keys.Contains(_tokens[currentToken].Type))
            {
                root = Parsers[_tokens[currentToken].Type](); //Calling parser function
            }
            else
            {
                ErrorList.Add(new Error("Unexpected token " + _tokens[currentToken].Literal + " in line" + _tokens[currentToken].Line.ToString(), Error.ErrorType.ParserError));
                currentToken++;
                return null;
            }

            //if  a statment composite like If,repeat loops, int x=0 where
            //A single statment is made of multiple statments yet intiated here
            //then a flag called composite is raised to avoid checking for a not
            //Existing semicolon
            if (!root.Composite && match(Refrence.Class.SemiColon))
            {
                currentToken++;
            }

            return root;
        }

        //Supports multiple declarations via the loop below
        private TreeNode declaration_stmt()
        {
            NonTerminalTreeNode dec_node = new NonTerminalTreeNode("Declaration Statment");
            dec_node.append_child(new TerminalTreeNode("Datatype", _tokens[currentToken]));
            dec_node.Attributes[Registrar.Attribute.Datatype] = (Registrar.Attribute) _tokens[currentToken].Type - 2;
            currentToken++;
            if (!match(Refrence.Class.Assignment_Identifier))
                return dec_node;
            while (_tokens[currentToken].Type == Refrence.Class.Assignment_Identifier)
            {
                dec_node.append_child(new TerminalTreeNode("Identifier", _tokens[currentToken]));
                currentToken++;
                if (_tokens[currentToken].Type==Refrence.Class.AssignmentOperator)
                {
                    dec_node.append_child(assign_stmt());
                }
                if (_tokens[currentToken].Type != Refrence.Class.Comma)
                {
                    break;
                }
                else
                {
                    currentToken++;
                }
            }
            //Checks in case of function then the statment is composed of declaration and a signature
            if (currentToken < _tokens.Count && _tokens[currentToken].Type == Refrence.Class.LeftBracket)
            {
                dec_node.Composite = true;
            }

            Rx.Variables.Add(((TerminalTreeNode)dec_node.Children[1]).getLiteral(), 
                (Registrar.Datatype)((TerminalTreeNode)dec_node.Children[1]).Token.Type - 1);

            return dec_node;
        }

        //Parses function calls with parameters
        private TreeNode function_call()
        {
            currentToken--;
            NonTerminalTreeNode fnHeader = new NonTerminalTreeNode("Function Call");
            if (match(Refrence.Class.Assignment_Identifier))
                fnHeader.append_child(new TerminalTreeNode("Function Name", _tokens[currentToken]));
            currentToken++;
            match(Refrence.Class.LeftBracket);
            currentToken++;
            NonTerminalTreeNode Attributes = new NonTerminalTreeNode("Parameters");
            int parameterCount = 1;
            while (_tokens[currentToken].Type != Refrence.Class.RightBracket)
            {
                Attributes.append_child(new TerminalTreeNode("Parameter# " + parameterCount.ToString(), _tokens[currentToken]));
                currentToken++;
                if (_tokens[currentToken].Type == Refrence.Class.RightBracket || !match(Refrence.Class.Comma))
                {
                    break;
                }
                parameterCount++;
                currentToken++;
            }
            fnHeader.append_child(Attributes);
            currentToken++;
            return fnHeader;
        }

        //Parses the 2nd half of the function declaration Function Name (Attributes)
        private TreeNode function_sig()
        {
            currentToken--;
            NonTerminalTreeNode fnHeader = new NonTerminalTreeNode("Function signature");
            if (match(Refrence.Class.Assignment_Identifier))
                fnHeader.append_child(new TerminalTreeNode("Function Name", _tokens[currentToken]));
            currentToken++;
            match(Refrence.Class.LeftBracket);
            currentToken++;
            NonTerminalTreeNode Attributes = new NonTerminalTreeNode("Attributes");
            int sigDec = 1;
            while (_tokens[currentToken].Type != Refrence.Class.RightBracket)
            {
                NonTerminalTreeNode Tn = new NonTerminalTreeNode("Signature Declaration# " + sigDec++.ToString());
                
                if (_tokens[currentToken].Type == Refrence.Class.DataType_int||
                    _tokens[currentToken].Type == Refrence.Class.DataType_float||
                    _tokens[currentToken].Type == Refrence.Class.DataType_string)
                {
                    Tn.append_child(new TerminalTreeNode("Datatype", _tokens[currentToken++]));
                }
                else
                {
                    ErrorList.Add(new Error("Unexpected lexeme in Line %LINE%, A datatype expected".Replace("%LINE%",_tokens[currentToken].Line.ToString()),Error.ErrorType.ParserError));
                }


                if (_tokens[currentToken].Type == Refrence.Class.Assignment_Identifier)
                {
                    Tn.append_child(new TerminalTreeNode("Identifier", _tokens[currentToken++]));
                }
                else
                {
                    ErrorList.Add(new Error("Unexpected lexeme in Line %LINE%, identifier expected".Replace("%LINE%", _tokens[currentToken].Line.ToString()), Error.ErrorType.ParserError));
                }
                Attributes.append_child(Tn);
                if (_tokens[currentToken].Type == Refrence.Class.RightBracket || !match(Refrence.Class.Comma))
                {
                    break;
                }
                currentToken++;
            }
            fnHeader.append_child(Attributes);
            fnHeader.Composite = true;
            currentToken++;
            return fnHeader;
        }

        //Switches the parser to an in function mode
        //to avoid nesting functions (lambda may still be made though if intented)
        //can handle multiple returns
        private TreeNode function_body()
        {
            Parsers[Refrence.Class.LeftBracket] = function_call;
            NonTerminalTreeNode fnBody = new NonTerminalTreeNode("Function body");
            currentToken++;

            while (currentToken != _tokens.Count)
            {
                if (_tokens[currentToken].Type == Refrence.Class.RightCurlyBrace)
                {
                    currentToken++;
                    break;
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
            Parsers[Refrence.Class.LeftBracket] = function_sig;
            fnBody.Composite = true;
            return fnBody;
        }

        //Entry function
        public Tree parse()
        {
            if (_tokens == null) return null;
            if (_tokens.Count == 0) return new Tree(null);
            currentToken = 0;
            return new Tree(program());
        }

        //Now doesn't change caret location
        //Merly used to register errors and check for matching
        //Will change the caret location in V2
        private bool match(Refrence.Class expected)
        {
            if (currentToken < _tokens.Count && _tokens[currentToken].Type == expected)
                return true;
            else
            {
                if (currentToken >= _tokens.Count)
                    ErrorList.Add(new Error("Expected a " + expected + " but reached End Of File!",
                        Error.ErrorType.ParserError));
                else
                    ErrorList.Add(new Error("Expected a " + expected + " in line " +_tokens[currentToken].Line.ToString() + " but a " + _tokens[currentToken].Type + " was found!",
                        Error.ErrorType.ParserError));
            }
            return false;
        }

        //Jigsaw if elseif^n else model
        //prevents a dual else or else if preceding else
        private TreeNode if_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("If statement");
            match(Refrence.Class.BranchingAgent_if);
            root.append_child(new TerminalTreeNode("If Intiator", _tokens[currentToken]));
            currentToken++;
            root.append_child(exp());
            match(Refrence.Class.BranchingAgent_then);
            currentToken++;
            int ElsesCount = 1;
            //seQ is our buffer each time we transfer from an if body to an else/elseif body we
            //append seQ and restart all over again in the new body
            //We keep track of our elses via Elses count
            //which in reality is an if body count
            NonTerminalTreeNode seQ = new NonTerminalTreeNode("If Body");
            while (_tokens[currentToken].Type != Refrence.Class.BranchingAgent_end)
            {
                if (_tokens[currentToken].Type == Refrence.Class.BranchingAgent_elseif)
                {
                    if (ElsesCount == 0)
                    {
                        ErrorList.Add(new Error("An ElseIf Scope can not be opened following an else scope", Error.ErrorType.ParserError));
                    }
                    currentToken++;
                    root.append_child(seQ);
                    seQ = new NonTerminalTreeNode("Else If#" + ElsesCount++.ToString());
                    NonTerminalTreeNode ConditionNode = new NonTerminalTreeNode("Condition");
                    ConditionNode.append_child(exp());
                    seQ.append_child(ConditionNode);
                    match(Refrence.Class.BranchingAgent_then);
                    seQ.append_child(new TerminalTreeNode("Keyword:", _tokens[currentToken]));
                    currentToken++;
                }

                if (_tokens[currentToken].Type == Refrence.Class.BranchingAgent_else)
                {
                    if (ElsesCount == 0)
                    {
                        ErrorList.Add(new Error("A single if can not contain 2 else scopes", Error.ErrorType.ParserError));
                    }
                    ElsesCount = 0;
                    currentToken++;
                    root.append_child(seQ);
                    seQ = new NonTerminalTreeNode("Else Body");
                }
                //All the branching above sets the seQ for this linear statment to append statments from tokens
                seQ.append_child(statement());
            }
            root.append_child(seQ);
            match(Refrence.Class.BranchingAgent_end);
            currentToken++;
            root.Composite = true;
            return root;
        }

        //Repeat bounds parser
        private TreeNode repeat_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Loop (Repeat)");
            match(Refrence.Class.LoopBound_repeat);
            currentToken++;
            NonTerminalTreeNode seQ = new NonTerminalTreeNode("Body");

            while (_tokens[currentToken].Type != Refrence.Class.LoopBound_until)
            {
                seQ.append_child(statement());
            }

            match(Refrence.Class.LoopBound_until);
            currentToken++;
            root.append_child(exp());
            root.append_child(seQ);
            root.Composite = true;
            return root;
        }

        private TreeNode write_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Write Statment");
            match(Refrence.Class.Directive_write);
            root.append_child(new TerminalTreeNode(("Write Directive"), _tokens[currentToken]));
            currentToken++;
            root.append_child(exp());
            return root;
        }

        private TreeNode read_stmt()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Read Statment");
            match(Refrence.Class.Directive_read);
            root.append_child(new TerminalTreeNode("Read Directive", _tokens[currentToken]));
            currentToken++;
            root.append_child(exp());
            return root;
        }

        private TreeNode assign_stmt()
        {
            currentToken--; //assign stmt is invoked by := therefore we must backtrack to find
                            //the target of assignment
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
            root.Attributes[Registrar.Attribute.Value] = (List<string>)root.Children.Last().Attributes[Registrar.Attribute.Value];
            root.Attributes[Registrar.Attribute.Datatype] =
                  (Registrar.Datatype)root.Children.Last().Attributes[Registrar.Attribute.Datatype];
            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ComparisonOperatorLessThan ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorEQ ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorGreaterThan ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorNQ) ||
                _tokens[currentToken].Type == Refrence.Class.LogicOperatorAND ||
                _tokens[currentToken].Type == Refrence.Class.LogicOperatorOR)
            {
                root.append_child(new TerminalTreeNode("Operator", _tokens[currentToken]));
                match(_tokens[currentToken].Type);
                currentToken++;
                root.append_child(simple_exp());

                if ((Registrar.Datatype)root.Children.Last().Attributes[Registrar.Attribute.Datatype] != (Registrar.Datatype)root.Attributes[Registrar.Attribute.Datatype])
                {
                    ErrorList.Add(new Error("Datatype mismatch at %LINE%, unable to calculate"
                        .Replace("%LINE%", _tokens[currentToken].Line.ToString()), Error.ErrorType.Inconsistency));
                }

                ((List<string>)root.Attributes[Registrar.Attribute.Value])
                    .AddRange((List<string>)root.Children.Last().Attributes[Registrar.Attribute.Value]);
                ((List<string>)root.Attributes[Registrar.Attribute.Value])
                    .Add(((TerminalTreeNode)root.Children[root.Children.Count - 2]).getLiteral());
            }

            return root;
        }

        private TreeNode simple_exp()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Simple expression");
            root.append_child(term());
            root.Attributes.Add(Registrar.Attribute.Value, root.Children.Last().Attributes[Registrar.Attribute.Value]);
            root.Attributes[Registrar.Attribute.Datatype] = 
                (Registrar.Datatype)root.Children.Last().Attributes[Registrar.Attribute.Datatype];
            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ArithmeticAddition ||
                   _tokens[currentToken].Type == Refrence.Class.Arithmeticsubtraction ||
                   _tokens[currentToken].Type == Refrence.Class.ArithmeticsubtractionOperator2))
            {
                TreeNode operatorNode = new TerminalTreeNode("Operator", _tokens[currentToken]);
                match(_tokens[currentToken].Type);
                currentToken++;
                TreeNode child = term();

                if ((Registrar.Datatype)child.Attributes[Registrar.Attribute.Datatype] != (Registrar.Datatype)root.Attributes[Registrar.Attribute.Datatype])
                {
                    ErrorList.Add(new Error("Datatype mismatch at %LINE%, unable to evaluate"
                        .Replace("%LINE%", _tokens[currentToken].Line.ToString()), Error.ErrorType.Inconsistency));
                }

                if (root.Children.Count == 2)
                {
                    NonTerminalTreeNode leftChild = root;
                    root = new NonTerminalTreeNode("Simple expression");
                    root.append_child(leftChild);
                    root.append_child(operatorNode);
                    root.append_child(child);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .AddRange((List<string>)leftChild.Attributes[Registrar.Attribute.Value]);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .AddRange((List<string>)child.Attributes[Registrar.Attribute.Value]);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .Add(((TerminalTreeNode)operatorNode).getLiteral());
                }
                else
                {
                    root.append_child(operatorNode);
                    root.append_child(child);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .AddRange((List<string>)child.Attributes[Registrar.Attribute.Value]);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .Add(((TerminalTreeNode)operatorNode).getLiteral());
                }
            }

            return root;
        }

        private TreeNode term()
        {
            NonTerminalTreeNode root = new NonTerminalTreeNode("Term");
            root.append_child(factor());
            root.Attributes[Registrar.Attribute.Value] = new List<string>();
            root.Attributes[Registrar.Attribute.Datatype] = 
                (Registrar.Datatype)root.Children.Last().Attributes[Registrar.Attribute.Datatype];
            ((List<string>)root.Attributes[Registrar.Attribute.Value]).AddRange
                (((List<string>)root.Children.Last().Attributes[Registrar.Attribute.Value]));

            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ArithmeticMultiplication ||
                   _tokens[currentToken].Type == Refrence.Class.ArithmeticDivision))
            {
                TreeNode operatorNode = new TerminalTreeNode("Operator", _tokens[currentToken]);
                match(_tokens[currentToken].Type);
                currentToken++;
                TreeNode child = factor();

                if ((Registrar.Datatype)child.Attributes[Registrar.Attribute.Datatype]!= (Registrar.Datatype)root.Attributes[Registrar.Attribute.Datatype])
                {
                    ErrorList.Add(new Error("Datatype mismatch at line %LINE%, unable to evaluate"
                        .Replace("%LINE%", _tokens[currentToken].Line.ToString()),Error.ErrorType.Inconsistency));
                }

                if (root.Children.Count == 2)
                {
                    NonTerminalTreeNode leftChild = root;
                    root = new NonTerminalTreeNode("Term");
                    root.append_child(leftChild);
                    root.append_child(operatorNode);
                    root.append_child(child);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .AddRange((List<string>)leftChild.Attributes[Registrar.Attribute.Value]);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .AddRange((List<string>)child.Attributes[Registrar.Attribute.Value]);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .Add(((TerminalTreeNode)operatorNode).getLiteral());
                }
                else
                {
                    root.append_child(operatorNode);
                    root.append_child(child);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .AddRange((List<string>)child.Attributes[Registrar.Attribute.Value]);
                    ((List<string>)root.Attributes[Registrar.Attribute.Value])
                        .Add(((TerminalTreeNode)operatorNode).getLiteral());
                }
            }
            return root;
        }

        private TreeNode factor()
        {
            TreeNode root = null;

            if (Macros.Contains(_tokens[currentToken].Type))
            {
                root = new TerminalTreeNode("Constant", _tokens[currentToken]);
                root.Attributes[Registrar.Attribute.Value]=Rx.Macros[Refrence.Class.Macro_endl];
                root.Attributes[Registrar.Attribute.Datatype]=Registrar.Datatype.Datatype_string;
                currentToken++;
                return root;
            }
            if (_tokens[currentToken+1].Type==Refrence.Class.LeftBracket)
            {
                Dictionary<Registrar.Attribute, object> Attributes = new Dictionary<Registrar.Attribute, object>();
                if (Rx.Variables.ContainsKey(_tokens[currentToken].Literal))
                {
                    Attributes[Registrar.Attribute.Datatype] = Rx.Variables[_tokens[currentToken].Literal];
                    Attributes[Registrar.Attribute.Value] = new List<string>() { "Fun(" + _tokens[currentToken].Literal + ")" };
                }
                else
                {
                    ErrorList.Add(new Error("Identifier Unacknolwedged at Line %LINE%".Replace("%LINE%", _tokens[currentToken].Line.ToString()), Error.ErrorType.Inconsistency));
                    Attributes[Registrar.Attribute.Datatype] = Registrar.Datatype.Undefined;
                    Attributes[Registrar.Attribute.Value] = new List<string>() { "Undefined" };
                }
                currentToken++;
                root = function_call();
                root.Attributes = Attributes;
                return root;
            }

            switch (_tokens[currentToken].Type)
            {
                case Refrence.Class.DataType_int:
                case Refrence.Class.DataType_float:
                case Refrence.Class.DataType_string:
                    root = new TerminalTreeNode("Constant", _tokens[currentToken]);
                    match(_tokens[currentToken].Type);
                    root.Attributes[Registrar.Attribute.Value] = new List<string>() { "Const(" + _tokens[currentToken].Literal + ")" };
                    root.Attributes[Registrar.Attribute.Datatype] = (Registrar.Datatype) _tokens[currentToken].Type-2;
                    root.Attributes[Registrar.Attribute.Variable] = false;
                    currentToken++;
                    break;
                case Refrence.Class.Assignment_Identifier:
                    root = new TerminalTreeNode("Identifier", _tokens[currentToken]);
                    match(_tokens[currentToken].Type);
                    if (Rx.Variables.ContainsKey(_tokens[currentToken].Literal))
                    {
                        root.Attributes[Registrar.Attribute.Value] = new List<string>(){ "Var(" + _tokens[currentToken].Literal + ")"};
                        root.Attributes[Registrar.Attribute.Datatype] = Rx.Variables[_tokens[currentToken].Literal];
                        root.Attributes[Registrar.Attribute.Variable] = true;
                    }
                    else
                    {
                        ErrorList.Add(new Error("Identifier Unacknolwedged at Line %LINE%".Replace("%LINE%", _tokens[currentToken].Line.ToString()), Error.ErrorType.Inconsistency));
                    }
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
                    ErrorList.Add(new Error("Unexpected token " + _tokens[currentToken].Literal + " in line" + _tokens[currentToken].Line.ToString(), Error.ErrorType.ParserError));
                    currentToken++;
                    break;
            }
            return root;
        }
    }
}
