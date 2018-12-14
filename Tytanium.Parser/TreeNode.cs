using System.Collections.Generic;
using Tytanium.Scanner;
using Handler;
using System;

namespace Tytanium.Parser
{
    public enum NodeClass
    {
        Declaration,
        FunctionSignature,
        RuntimeCall,
        FunctionCall,
        Assignment,
        Scope,
        Expression,
        Terminal,
        ifCondition,
        LoopBound,
        Directive,
        Term,
        SimpleExpression
    }
    public enum Datatype
    {
        Datatype_int,
        Datatype_float,
        Datatype_string,
        Datatype_boolean,
        Undefined,
        NULL
    }

    public enum Attribute
    {
        Signature,
        Value,
        Datatype,
        HostedToken
    };

    public class Identifier
    {
        public List<Datatype> DataTypeRestrictions = new List<Datatype>();

        public bool FunctionID
        {
            get; set;
        }
        public Identifier()
        {

        }

        public Identifier(Datatype DT, string n)
        {
            datatype = DT;
            name = n;
        }
        public Datatype datatype
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public virtual Datatype DatatypeRestriction(int i)
        {
            if (FunctionID)
            {
                return DataTypeRestrictions[i];
            }

            return datatype;
        }

    }

    public class Tree
    {
        public List<Error> Inconsisities = new List<Error>();

        public TreeNode Root;

        public Dictionary<Refrence.Class, string> Macros = new Dictionary<Refrence.Class, string>()
        {
            {Refrence.Class.Macro_endl,"\r\n" }
        };

        void RegisterInconsistancy(string input)
        {
           Inconsisities.Add(new Error(input, Error.ErrorType.Inconsistency));
        }

        public void AssociateRoot(TreeNode Entry)
        {
            Entry.setDelegates(null, null, RegisterInconsistancy);
        }

        public Tree(TreeNode root)
        {
            Root = root;
        }

        public Tree()
        {

        }

    }

    public class TreeNode
    {
        const string TypeMismatch = "Datatype mismatch in line %LINE%, unable to evaluate";
        const string Incompara = "Method %ID% parameters incompatible in line %LINE%";
        const string CallMismatch = "Identifier %ID% incompatible definition in line %LINE%";
        const string unknwonIdentifier = "Identifier Unacknolwedged in Line %LINE%";
        const string returnTypemismatch = "Method return type mismatch in Line %LINE%";
        const string variableNameRedefinition = "Redefinition Not Possible in %LINE%";
        const string LineMacro = "%LINE%";
        const string IDMACRO = "%ID%";

        varAppend fnAppendID;
        varVerify fnVerifyID;
        inConsistancyReg fnRegisterInconsistancy;

        List<Error> Inconsisities = new List<Error>();
        public Dictionary<string, Identifier> Variables = new Dictionary<string, Identifier>();

        public NodeClass NodeType;
        public void setDelegates(varAppend a, varVerify b, inConsistancyReg c)
        {
            fnAppendID = a;
            fnVerifyID = b;
            fnRegisterInconsistancy = c;
        }

        public void AssociateNode(TreeNode N)
        {
            if (N == null) { return; }
            if (NodeType==NodeClass.Scope || NodeType==NodeClass.FunctionSignature)
            {
                N.setDelegates(appendID, verifyID, fnRegisterInconsistancy);
            }
            else
            {
                N.setDelegates(fnAppendID, verifyID, fnRegisterInconsistancy);
            }

        }

        public delegate void varAppend(string s, Identifier id);
        public delegate Identifier varVerify(string s);
        public delegate void inConsistancyReg(string input);

        void appendID(string s, Identifier id)
        {
            Variables.Add(s, id);
        }

        Identifier verifyID(string s)
        {
            if (Variables.ContainsKey(s))
            {
                return Variables[s];
            }
            else if (fnVerifyID!=null)
            {
                return fnVerifyID(s);
            }
            else
            {
                return null;
            }
        }

        void RegisterInconsistancy(string input)
        {
            if (fnRegisterInconsistancy == null)
            {
                Inconsisities.Add(new Error(input, Error.ErrorType.Inconsistency));
            }
            else
            {
                fnRegisterInconsistancy(input);
            }
        }

        public readonly List<TreeNode> Children = new List<TreeNode>();

        public Dictionary<Attribute, object> Attributes = new Dictionary<Attribute, object>();

        //Datatype of the node
        public Datatype DataType
        {
            get
            {
                if (Attributes.ContainsKey(Attribute.Datatype))
                {
                    return (Datatype)Attributes[Attribute.Datatype];
                }
                return Datatype.NULL;
            }
            set
            {
                Attributes[Attribute.Datatype] = value;
            }
        }

        //Value of the node
        public string Value
        {
            get
            {
                if (Attributes.ContainsKey(Attribute.Value))
                {
                    if (Attributes[Attribute.Value] is string)
                    {
                        return (string)Attributes[Attribute.Value];
                    }
                    else
                    {
                        return String.Join(" ", ((List<string>)(Attributes[Attribute.Value])).ToArray());
                    }
                }
                else
                {
                    return Token.Literal;
                }
            }
        }

        //Retrieves the actual literal of the node
        public string Literal
        {
            get
            {
                if (NodeType == NodeClass.Terminal)
                    return Token.Literal;
                else
                    return Children[0].Literal;
            }
        }

        //Get Label for Tree View construction
        public string getLabel()
        {
            if (NodeType != NodeClass.Terminal)
            {
                return label;
            }
            else
            {
                return label + " : " + Token.Literal;
            }
        }

        public Token Token
        {
            get
            {
                if (Attributes.ContainsKey(Attribute.HostedToken))
                {
                    return (Token)Attributes[Attribute.HostedToken];
                }
                else
                {
                    return null;
                }
            }
        }

        public string label;
        private TreeNode()
        {
            label = "";
        }

        public TreeNode(string label,NodeClass Class)
        {
            NodeType = Class;
            this.label = label;
            LoadAssimilators();
        }

        public TreeNode(string label, Token token)
        {
            this.label = label;
            NodeType = NodeClass.Terminal;
            Token T = token;
            Attributes[Attribute.HostedToken] = T;
            LoadAssimilators();
        }

        public bool Composite = false;


        public int startingToken
        {
            get;
            set;
        }

        public int endingToken
        {
            get;
            set;
        }

        public void append_child(TreeNode Entry)
        {
            if (Entry == null || Entry.Line==-1) { return; }
            Assimilators[NodeType](Entry);
            AssociateNode(Entry);
            Children.Add(Entry);
            return;
        }

        delegate void Assimilator(TreeNode N);
        Dictionary<NodeClass, Assimilator> Assimilators = new Dictionary<NodeClass, Assimilator>();
        private void LoadAssimilators()
        {
            Assimilators = new Dictionary<NodeClass, Assimilator>()
            {
                {NodeClass.Declaration,declaration },
                {NodeClass.RuntimeCall,RuntimeCall },
                {NodeClass.Scope,ScopeResolution },
                {NodeClass.FunctionSignature,FnSignature },
                {NodeClass.Directive,Directive },
                {NodeClass.Expression,Expression },
                {NodeClass.Term,Term },
                {NodeClass.ifCondition,ifCondition },
                {NodeClass.LoopBound,LoopBound},
                {NodeClass.FunctionCall,FunctionCall },
                {NodeClass.Assignment,Assignment },
                {NodeClass.SimpleExpression,simpleExpression }
            };
        }

        private void LoopBound(TreeNode N)
        {

        }

        public int Line
        {
            get
            {
                if (Attributes.ContainsKey(Attribute.HostedToken))
                {
                    return Token.Line;
                }
                else
                {
                    if (Children.Count>0)
                        return Children[0].Line;
                    else return -1;
                }
            }
        }

        private void FunctionCall(TreeNode N)
        {
            Identifier ID;
            if (Children.Count == 0)
            {
                ID = verifyID(N.Literal);
                return;
            }
            else { ID = verifyID(Literal); }

            if (!ID.FunctionID)
            {
                RegisterInconsistancy(CallMismatch.Replace(LineMacro, N.Line.ToString()).Replace(IDMACRO,Literal));
            }
            else if (ID.DatatypeRestriction(Children.Count - 1) != N.DataType)
            {
                RegisterInconsistancy(Incompara.Replace(LineMacro, N.Line.ToString()).Replace("%ID%",Children[0].Literal));
            }

        }

        public void declaration(TreeNode N)
        {
            if (Children.Count != 0)
            {
                if (N.NodeType == NodeClass.Assignment)
                {

                }
                else if (verifyID(N.Literal) != null)
                {
                    RegisterInconsistancy(variableNameRedefinition.Replace(LineMacro, N.Token.Line.ToString()));
                }
                else
                {
                    N.Attributes[Attribute.Datatype] = Attributes[Attribute.Datatype];
                    fnAppendID(N.Token.Literal, new Identifier(DataType, N.Token.Literal));
                }
            }
            else
            {
                N.Attributes[Attribute.Datatype] = (Datatype)N.Token.Type - 2;
                Attributes[Attribute.Datatype] = (Datatype)N.Token.Type - 2;
            }
        }

        public void RuntimeCall(TreeNode N)
        {
            Identifier ID;
            ID = verifyID(N.Literal);
            //First Conditional: if ID not existant then halt, if it exists than establish the node's properties
            if (ID == null)
            {
                if (N.NodeType == NodeClass.Terminal && N.Token.Type!=Refrence.Class.Assignment_Identifier) { N.DataType = (Datatype)N.Token.Type - 2; }
                else
                {
                    RegisterInconsistancy(unknwonIdentifier.Replace(LineMacro, N.Line.ToString()).Replace(IDMACRO, N.Literal.ToString()));
                    return;
                }
            }
            else { N.DataType = ID.datatype; }

            //First Conditional: if ID is a function then call the designated subroutine, else compare it to the assigning datatype
            if (Children.Count == 0)
            {
                DataType = ID.datatype;
                if (verifyID(N.Literal).FunctionID) { NodeType = NodeClass.FunctionCall; }
                else { NodeType = NodeClass.Assignment; }
                return;
            }
        }

        private void ScopeResolution(TreeNode N)
        {

        }

        private void FnSignature(TreeNode N)
        {
            if (N.NodeType==NodeClass.Scope)
            {
                N.Attributes[Attribute.Datatype] = DataType;
                return;
            }
            if (Children.Count != 0)
            {

                Identifier signature = verifyID(Children[0].Token.Literal);
                N.Attributes[Attribute.Datatype] = ((Datatype)N.Children[0].Token.Type - 2);
                signature.DataTypeRestrictions.Add(((Datatype)N.Children[0].Token.Type - 2));

            }
            else
            {
                Identifier signature = verifyID(N.Token.Literal);
                if (signature == null)
                {
                    RegisterInconsistancy(unknwonIdentifier.Replace(LineMacro, N.Token.Line.ToString()));
                }
                else
                {
                    signature.FunctionID = true;
                    Attributes[Attribute.Datatype] = signature.datatype;
                    N.Attributes[Attribute.Datatype] = signature.datatype;
                }
            }
        }

        private void Directive(TreeNode N)
        {
            if (Children.Count!=0 && Children[0].Token.Type == Refrence.Class.Directive_return)
            {
                if ((Datatype)N.Attributes[Attribute.Datatype] != (Datatype)Attributes[Attribute.Datatype])
                {
                    RegisterInconsistancy(returnTypemismatch.Replace(LineMacro, Children[0].Token.Line.ToString()));
                }
            }
        }

        private void Assignment(TreeNode N)
        {
            Term(N);
            if (Children.Count==0)
            {
                Attributes[Attribute.Datatype] = N.DataType;
            }
            else
            {
                if (DataType!=N.DataType)
                {
                    RegisterInconsistancy(TypeMismatch.Replace(LineMacro,N.Line.ToString()));
                }
            }
        }

        private void ifCondition(TreeNode N)
        {

        }

        private void simpleExpression(TreeNode N)
        {
            if (Children.Count == 0)
            {
                Attributes[Attribute.Datatype] = N.DataType;
                if (N.Attributes[Attribute.Value] is List<string>)
                {
                    appendToValue((List<string>)N.Attributes[Attribute.Value]);
                }
                else if (N.Attributes[Attribute.Value] is string)
                {
                    appendToValue((string)N.Attributes[Attribute.Value]);
                }
            }
            if (Children.Count == 1) { }
            else if (Children.Count == 2)
            {
                if (N.DataType != DataType)
                {
                    RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Line.ToString()));
                }
                if (N.Attributes[Attribute.Value] is List<string>)
                {
                    appendToValue((List<string>)N.Attributes[Attribute.Value]);
                }
                else if (N.Attributes[Attribute.Value] is string)
                {
                    appendToValue((string)N.Attributes[Attribute.Value]);
                }

                appendToValue(Children[1].Token.Literal);
            }
        }
        private void Expression(TreeNode N)
        {
            if (Children.Count == 0)
            {
                Attributes[Attribute.Datatype] = N.DataType;
                if (N.Attributes[Attribute.Value] is List<string>)
                {
                    appendToValue((List<string>)N.Attributes[Attribute.Value]);
                }
                else if (N.Attributes[Attribute.Value] is string)
                {
                    appendToValue((string)N.Attributes[Attribute.Value]);
                }
            }
            if (Children.Count == 1) { }
            else if (Children.Count == 2)
            {
                if (N.DataType != DataType)
                {
                    RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Line.ToString()));
                }
                if (N.Attributes[Attribute.Value] is List<string>)
                {
                    appendToValue((List<string>)N.Attributes[Attribute.Value]);
                }
                else if (N.Attributes[Attribute.Value] is string)
                {
                    appendToValue((string)N.Attributes[Attribute.Value]);
                }

                appendToValue(Children[1].Token.Literal);
            }
        }

        void appendToValue(string s)
        {
            if (!Attributes.ContainsKey(Attribute.Value))
            {
                Attributes[Attribute.Value] = new List<string>();
            }
            ((List<string>)Attributes[Attribute.Value]).Add(s);
        }

        void appendToValue(List<string> s)
        {
            if (!Attributes.ContainsKey(Attribute.Value))
            {
                Attributes[Attribute.Value] = new List<string>();
            }
           ((List<string>)Attributes[Attribute.Value]).AddRange(s);
        }

        void postFixAppend(string s)
        {
            int LastIndex = ((List<string>)Attributes[Attribute.Value]).Count - 1;
            string temp = ((List<string>)Attributes[Attribute.Value])[LastIndex];
            ((List<string>)Attributes[Attribute.Value])[LastIndex] = s;
            ((List<string>)Attributes[Attribute.Value]).Add(temp);
        }
        void postFixAppend(List<string> s)
        {
            int LastIndex = ((List<string>)Attributes[Attribute.Value]).Count-1;
            string temp = ((List<string>)Attributes[Attribute.Value])[LastIndex];
            ((List<string>)Attributes[Attribute.Value]).RemoveAt(LastIndex);
            ((List<string>)Attributes[Attribute.Value]).AddRange(s);
            ((List<string>)Attributes[Attribute.Value]).Add(temp);
        }

        void SmartAppend(string Gen,string Literal)
        {
            if (Children.Count >= 2)
            {
                postFixAppend(Gen + "(" + Literal + ")");
            }
            else
            {
                appendToValue(Gen + "(" + Literal + ")");
            }
        }

        private void Term(TreeNode N)
        {
            if (N.NodeType == NodeClass.Terminal)
            {
                switch (N.Token.Type)
                {
                    case Refrence.Class.DataType_int:
                    case Refrence.Class.DataType_float:
                    case Refrence.Class.DataType_string:
                        N.Attributes[Attribute.Datatype] = (Datatype)N.Token.Type - 2;
                        if (Children.Count == 0)
                        {
                            Attributes[Attribute.Datatype] = N.DataType;
                        }
                        else if ((Datatype)N.Attributes[Attribute.Datatype]!= (Datatype)Attributes[Attribute.Datatype])
                        { RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Token.Line.ToString())); }
                        SmartAppend("Const", N.Literal);
                        break;
                    case Refrence.Class.Assignment_Identifier:
                        Identifier ID = verifyID(N.Literal);
                        if (ID == null)
                        {
                            RegisterInconsistancy(unknwonIdentifier.Replace(LineMacro, N.Token.Line.ToString()));
                            Attributes[Attribute.Datatype] = Datatype.Undefined;
                            N.Attributes[Attribute.Datatype] = Datatype.Undefined;
                            SmartAppend("Unknown", N.Literal);
                        }
                        else
                        {
                            Attributes[Attribute.Datatype] = ID.datatype;
                            N.Attributes[Attribute.Datatype] = ID.datatype;
                            SmartAppend("Var", N.Literal);
                        }
                        break;
                    default:
                        appendToValue(N.Token.Literal);
                        break;
                }
            }
            else if (N.NodeType==NodeClass.Expression)
            {
                if (Children.Count == 0)
                {
                    Attributes[Attribute.Datatype] = N.DataType;
                    appendToValue(((List<string>)N.Attributes[Attribute.Value]));
                }
                else if (Children.Count == 1) { appendToValue(((List<string>)N.Attributes[Attribute.Value])); }
                else if (Children.Count >= 2)
                {
                    if (DataType == N.DataType)
                    {
                        postFixAppend(((List<string>)N.Attributes[Attribute.Value]));
                    }
                    else
                    {
                        RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Line.ToString()));
                    }
                }

            }
            else if (N.NodeType==NodeClass.FunctionCall)
            {
                Identifier ID = verifyID(N.Children[0].Literal);
                if (ID == null) { RegisterInconsistancy(unknwonIdentifier.Replace(LineMacro, N.Token.Line.ToString())); }
                else
                {
                    Attributes[Attribute.Datatype] = ID.datatype;
                    N.Attributes[Attribute.Datatype] = ID.datatype;
                    string val = "Fun(" + N.Children[0].Literal+"(";
                    for (int i = 1; i < N.Children.Count; i++)
                    {
                        val += N.Children[i].Value;
                        if (i < N.Children.Count - 1) { val += ","; }
                    }
                    appendToValue(val+"))");
                }
            }
        }
    }
}