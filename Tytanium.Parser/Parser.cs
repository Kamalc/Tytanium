using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Handler;
using Tytanium.Scanner;

namespace Tytanium.Parser
{
    public class Parser
    {
        public Tree ParseTree = new Tree();

        public List<Error> Inconsisties
        {
            get
            {
                return ParseTree.Inconsisities;
            }
        }

        delegate TreeNode ParserFn(TreeNode Parent); //Parser Delegation

        Dictionary<Refrence.Class, ParserFn> Parsers = new Dictionary<Refrence.Class, ParserFn>();

        List<Refrence.Class> Macros = new List<Refrence.Class>() //Things that will get replaced during semantic analysis Endl=>\n\r
        {
            Refrence.Class.Macro_endl
        };

        private List<Token> _tokens;
        public List<Error> ErrorList;
        private int currentToken;



        const string LineMacro = "%LINE%";
        const string TokenMacro = "%TOKEN%";
        const string EndOfFile = "End of File was found before %TOKEN% matching for scope intiated in line %LINE%";

        private Error CreateError(string E, string Token,int line)
        {
            string error = E.Replace(TokenMacro, Token).Replace(LineMacro, line.ToString());
            return new Error(error, Error.ErrorType.ParserError);
        }

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
                //{Refrence.Class.LeftCurlyBrace,function_body },
            };
        }

        //Invoked in case of identifier intiating statment variable := or variable(.)...etc
        //May be used in operator overloading and dot operator functions
        //Applications are limitless
        private TreeNode identifier_call(TreeNode Parent) 
        {
            TreeNode identifierNode = new TreeNode("Runtime Statment",NodeClass.RuntimeCall);
            Parent.AssociateNode(identifierNode);
            identifierNode.append_child(new TreeNode("Calling Identifier", _tokens[currentToken]));
            currentToken++;
            identifierNode.append_child(statement(identifierNode));
            identifierNode.Composite = true;
            return identifierNode;
        }

        //Program Syntitcal tree outer structure
        //Recursion intiator function
        private TreeNode program()
        {
            TreeNode prog_root = new TreeNode("Program Syntatic structure",NodeClass.Scope);
            ParseTree.AssociateRoot(prog_root);
            while (currentToken < _tokens.Count)
            {
                prog_root.append_child(stmt_sequence(prog_root));
            }
            return prog_root;
        }

        private TreeNode stmt_sequence(TreeNode Parent)
        {
            TreeNode seq_root = new TreeNode("Statements sequence",NodeClass.Scope);
            Parent.AssociateNode(seq_root);

            while (currentToken != _tokens.Count)
            {
                TreeNode child = statement(seq_root);
                if (child != null)
                {
                    seq_root.append_child(child);
                }
            }

            return seq_root;
        }

        //Linear statment parsing
        private TreeNode statement(TreeNode Parent)
        {
            TreeNode root = null;

            if (currentToken == _tokens.Count)
                return null;

            //Comment Ad-Hoc model, will be removed in V2.0
            else if (_tokens[currentToken].UpperType == Refrence.UpperClass.Comment)
            {
                root = new TreeNode("Comment", _tokens[currentToken]);
                currentToken++;
                return root;
            }

            else if (Parsers.Keys.Contains(_tokens[currentToken].Type))
            {
                root = Parsers[_tokens[currentToken].Type](Parent); //Calling parser function
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
            if (root==null||!root.Composite && match(Refrence.Class.SemiColon))
            {
                currentToken++;
            }

            return root;
        }

        //Supports multiple declarations via the loop below
        private TreeNode declaration_stmt(TreeNode Parent)
        {
            TreeNode dec_node = new TreeNode("Declaration Statment",NodeClass.Declaration);
            Parent.AssociateNode(dec_node);
            dec_node.append_child(new TreeNode("Datatype", _tokens[currentToken]));
            currentToken++;
            if (!match(Refrence.Class.Assignment_Identifier))
                return dec_node;
            while (_tokens[currentToken].Type == Refrence.Class.Assignment_Identifier)
            {
                dec_node.append_child(new TreeNode("Identifier", _tokens[currentToken]));
                currentToken++;
                if (_tokens[currentToken].Type==Refrence.Class.AssignmentOperator)
                {
                    dec_node.append_child(assign_stmt(dec_node));
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

            return dec_node;
        }

        //Parses function calls with parameters
        private TreeNode function_call(TreeNode Parent)
        {
            currentToken--;
            TreeNode fnHeader = new TreeNode("Function Call",NodeClass.FunctionCall);
            Parent.AssociateNode(fnHeader);
            if (match(Refrence.Class.Assignment_Identifier))
                fnHeader.append_child(new TreeNode("Function Name", _tokens[currentToken]));
            currentToken++;
            match(Refrence.Class.LeftBracket);
            currentToken++;
            //TreeNode Attributes = new TreeNode("Parameters",NodeClass.Scope);
            int parameterCount = 1;
            while (_tokens[currentToken].Type != Refrence.Class.RightBracket)
            {
                TreeNode Px = exp(fnHeader);
                if (Px == null) { return null; }
                if (_tokens[currentToken].Type == Refrence.Class.RightBracket || !match(Refrence.Class.Comma))
                {
                    break;
                }
                parameterCount++;
                currentToken++;
                fnHeader.append_child(Px);
            }
            currentToken++;
            return fnHeader;
        }

        //Parses the 2nd half of the function declaration Function Name (Attributes)
        private TreeNode function_sig(TreeNode Parent)
        {
            currentToken--;
            TreeNode fnHeader = new TreeNode("Function signature",NodeClass.FunctionSignature);
            Parent.AssociateNode(fnHeader);
            if (match(Refrence.Class.Assignment_Identifier))
                fnHeader.append_child(new TreeNode("Function Name", _tokens[currentToken]));
            currentToken++;
            match(Refrence.Class.LeftBracket);
            currentToken++;
            int sigDec = 1;
            while (_tokens[currentToken].Type != Refrence.Class.RightBracket)
            {
                TreeNode Tn = new TreeNode("Signature Declaration# " + sigDec++.ToString(),NodeClass.Declaration);
                fnHeader.AssociateNode(Tn);
                if (_tokens[currentToken].Type == Refrence.Class.DataType_int||
                    _tokens[currentToken].Type == Refrence.Class.DataType_float||
                    _tokens[currentToken].Type == Refrence.Class.DataType_string)
                {
                    Tn.append_child(new TreeNode("Datatype", _tokens[currentToken++]));
                }
                else
                {
                    ErrorList.Add(new Error("Unexpected lexeme in Line %LINE%, A datatype expected".Replace("%LINE%",_tokens[currentToken].Line.ToString()),Error.ErrorType.ParserError));
                }


                if (_tokens[currentToken].Type == Refrence.Class.Assignment_Identifier)
                {
                    Tn.append_child(new TreeNode("Identifier", _tokens[currentToken++]));
                }
                else
                {
                    ErrorList.Add(new Error("Unexpected lexeme in Line %LINE%, identifier expected".Replace("%LINE%", _tokens[currentToken].Line.ToString()), Error.ErrorType.ParserError));
                }
                fnHeader.append_child(Tn);
                if (_tokens[currentToken].Type == Refrence.Class.RightBracket || !match(Refrence.Class.Comma))
                {
                    break;
                }
                currentToken++;
            }
            currentToken++;
            fnHeader.append_child(function_body(fnHeader));
            fnHeader.Composite = true;

            return fnHeader;
        }

        //Switches the parser to an in function mode
        //to avoid nesting functions (lambda may still be made though if intented)
        //can handle multiple returns
        private TreeNode function_body(TreeNode Parent)
        {
            Parsers[Refrence.Class.LeftBracket] = function_call;
            TreeNode fnBody = new TreeNode("Function body",NodeClass.Scope);
            Parent.AssociateNode(fnBody);
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
                    TreeNode returnCode = new TreeNode("Return Statment",NodeClass.Directive);
                    fnBody.AssociateNode(returnCode);
                    returnCode.Attributes[Attribute.Datatype] = Parent.Children[0].DataType;
                    returnCode.append_child(new TreeNode("Return directive", _tokens[currentToken]));
                    currentToken++;
                    returnCode.append_child(exp(returnCode));
                    match(Refrence.Class.SemiColon);
                    currentToken++;
                    continue;
                }

                TreeNode child = statement(fnBody);
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
            TreeNode Root = program();
            ParseTree.Root = Root;
            return ParseTree;
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
        private TreeNode if_stmt(TreeNode Parent)
        {
            TreeNode root = new TreeNode("If statement",NodeClass.ifCondition);
            Parent.AssociateNode(root);
            match(Refrence.Class.BranchingAgent_if);
            root.append_child(new TreeNode("If Intiator", _tokens[currentToken]));
            currentToken++;
            root.append_child(exp(root));
            match(Refrence.Class.BranchingAgent_then);
            currentToken++;
            int ElsesCount = 1;
            //seQ is our buffer each time we transfer from an if body to an else/elseif body we
            //append seQ and restart all over again in the new body
            //We keep track of our elses via Elses count
            //which in reality is an if body count
            TreeNode seQ = new TreeNode("If Body",NodeClass.Scope);
            root.AssociateNode(seQ);
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
                    seQ = new TreeNode("Else If#" + ElsesCount++.ToString(),NodeClass.Scope);
                    TreeNode ConditionNode = new TreeNode("Condition",NodeClass.Scope);
                    ConditionNode.append_child(exp(ConditionNode));
                    seQ.append_child(ConditionNode);
                    match(Refrence.Class.BranchingAgent_then);
                    seQ.append_child(new TreeNode("Keyword:", _tokens[currentToken]));
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
                    seQ = new TreeNode("Else Body",NodeClass.Scope);
                }

                //All the branching above sets the seQ for this linear statment to append statments from tokens
                seQ.append_child(statement(seQ));
                if (currentToken == _tokens.Count)
                {
                    ErrorList.Add(CreateError(EndOfFile, "end", root.Children[0].Line));
                    root.append_child(seQ);
                    return root;
                }
            }
            root.append_child(seQ);
            match(Refrence.Class.BranchingAgent_end);
            currentToken++;
            root.Composite = true;
            return root;
        }

        //Repeat bounds parser
        private TreeNode repeat_stmt(TreeNode Parent)
        {
            TreeNode root = new TreeNode("Loop (Repeat)",NodeClass.LoopBound);
            Parent.AssociateNode(root);
            match(Refrence.Class.LoopBound_repeat);
            currentToken++;
            TreeNode seQ = new TreeNode("Body",NodeClass.Scope);
            root.AssociateNode(seQ);
            while (_tokens[currentToken].Type != Refrence.Class.LoopBound_until)
            {
                seQ.append_child(statement(seQ));
            }
            match(Refrence.Class.LoopBound_until);
            currentToken++;
            root.append_child(exp(root));
            root.append_child(seQ);
            root.Composite = true;
            return root;
        }

        private TreeNode write_stmt(TreeNode Parent)
        {
            TreeNode root = new TreeNode("Write Statment",NodeClass.Directive);
            Parent.AssociateNode(root);
            match(Refrence.Class.Directive_write);
            root.append_child(new TreeNode(("Write Directive"), _tokens[currentToken]));
            currentToken++;
            root.append_child(exp(root));
            return root;
        }

        private TreeNode read_stmt(TreeNode Parent)
        {
            TreeNode root = new TreeNode("Read Statment",NodeClass.Directive);
            Parent.AssociateNode(root);
            match(Refrence.Class.Directive_read);
            root.append_child(new TreeNode("Read Directive", _tokens[currentToken]));
            currentToken++;
            root.append_child(exp(root));
            return root;
        }

        private TreeNode assign_stmt(TreeNode Parent)
        {
            TreeNode root = new TreeNode("Assign",NodeClass.Assignment);
            Parent.AssociateNode(root);

            match(Refrence.Class.AssignmentOperator);
            currentToken++;

            root.append_child(exp(root));
            return root;
        }

        private TreeNode exp(TreeNode Parent)
        {
            TreeNode root = new TreeNode("Expression",NodeClass.Expression);
            Parent.AssociateNode(root);

            TreeNode Tx = simple_exp(root);
            if (Tx != null && Tx.Children.Count != 0) { root.append_child(Tx); }
            else { return null; }

            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ComparisonOperatorLessThan ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorEQ ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorGreaterThan ||
                _tokens[currentToken].Type == Refrence.Class.ComparisonOperatorNQ) ||
                _tokens[currentToken].Type == Refrence.Class.LogicOperatorAND ||
                _tokens[currentToken].Type == Refrence.Class.LogicOperatorOR)
            {
                root.append_child(new TreeNode("Operator", _tokens[currentToken]));
                match(_tokens[currentToken].Type);
                currentToken++;
                root.append_child(simple_exp(root));
            }

            return root;
        }

        private TreeNode simple_exp(TreeNode Parent)
        {
            TreeNode root = new TreeNode("Simple expression", NodeClass.SimpleExpression);
            Parent.AssociateNode(root);
            TreeNode Tx = term(root);
            if (Tx!=null && Tx.Children.Count != 0) { root.append_child(Tx); }
            else { return null; }
            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ArithmeticAddition ||
                   _tokens[currentToken].Type == Refrence.Class.Arithmeticsubtraction ||
                   _tokens[currentToken].Type == Refrence.Class.ArithmeticsubtractionOperator2))
            {
                TreeNode operatorNode = new TreeNode("Operator", _tokens[currentToken]);
                match(_tokens[currentToken].Type);
                currentToken++;
                TreeNode child = term(root);

                if (root.Children.Count == 2)
                {
                    TreeNode leftChild = root;
                    root = new TreeNode("Simple expression",NodeClass.Expression);
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

        private TreeNode term(TreeNode Parent)
        {
            TreeNode root = new TreeNode("Term",NodeClass.Term);
            Parent.AssociateNode(root);
            TreeNode Tx = factor(root);
            if (Tx != null && Tx.Children.Count != 0) { root.append_child(Tx); }
            else { return null; }
            root.append_child(Tx);
            while (currentToken < _tokens.Count && (_tokens[currentToken].Type == Refrence.Class.ArithmeticMultiplication ||
                   _tokens[currentToken].Type == Refrence.Class.ArithmeticDivision))
            {
                TreeNode operatorNode = new TreeNode("Operator", _tokens[currentToken]);
                match(_tokens[currentToken].Type);
                currentToken++;
                TreeNode child = factor(root);

                if (root.Children.Count == 2)
                {
                    TreeNode leftChild = root;
                    root = new TreeNode("Term",NodeClass.Term);
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

        private TreeNode factor(TreeNode Parent)
        {
            TreeNode root = null;
            if (Macros.Contains(_tokens[currentToken].Type))
            {
                root = new TreeNode("Constant", _tokens[currentToken]);
                currentToken++;
                return root;
            }
            if (_tokens[currentToken+1].Type==Refrence.Class.LeftBracket)
            {
                currentToken++;
                root = function_call(Parent);
                return root;
            }
            switch (_tokens[currentToken].Type)
            {
                case Refrence.Class.DataType_int:
                case Refrence.Class.DataType_float:
                case Refrence.Class.DataType_string:
                    root = new TreeNode("Constant", _tokens[currentToken]);
                    match(_tokens[currentToken].Type);
                    currentToken++;
                    break;
                case Refrence.Class.Assignment_Identifier:
                    root = new TreeNode("Identifier", _tokens[currentToken]);
                    match(_tokens[currentToken].Type);
                    currentToken++;
                    break;
                case Refrence.Class.LeftBracket:
                    match(Refrence.Class.LeftBracket);
                    currentToken++;
                    root = exp(Parent);
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
