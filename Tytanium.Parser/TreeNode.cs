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
        Undefined,
        Datatype_boolean
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
            return datatype;
        }

    }

    public class FunctionID : Identifier
    {
        public List<Datatype> Parameters = new List<Datatype>();

        public override Datatype DatatypeRestriction(int i)
        {
            return Parameters[i];
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
        const string CallMismatch = "Identifier incompatible in line %LINE%";
        const string unknwonIdentifier = "Identifier Unacknolwedged in Line %LINE%";
        const string returnTypemismatch = "Method type mismatch in Line %LINE%";
        const string LineMacro = "%LINE%";

        varAppend fnAppendID;
        varVerify fnVerifyID;
        inConsistancyReg fnRegisterInconsistancy;

        List<Error> Inconsisities = new List<Error>();
        public Dictionary<string, Identifier> Variables = new Dictionary<string, Identifier>();

        NodeClass NodeType;
        public void setDelegates(varAppend a, varVerify b, inConsistancyReg c)
        {
            fnAppendID = a;
            fnVerifyID = b;
            fnRegisterInconsistancy = c;
        }

        public void AssociateNode(TreeNode N)
        {
            if (NodeType==NodeClass.Scope)
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

        public Dictionary<Attribute, object> Attributes = new Dictionary<Attribute, object>();

        public readonly List<TreeNode> Children = new List<TreeNode>();

        public Datatype DataType
        {
            get
            {
                return (Datatype)Attributes[Attribute.Datatype];
            }
        }

        public Token Token
        {
            get
            {
                return (Token)Attributes[Attribute.HostedToken];
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

        public string Literal
        {
            get
            {
                return Token.Literal;
            }
        }

        public string getLabel()
        {
            if (NodeType!=NodeClass.Terminal)
            {
                return label;
            }
            else
            {
                return label + " : " + Token.Literal;
            }
        }
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
                {NodeClass.FunctionCall,RuntimeCall },
                {NodeClass.Assignment,Assignment },
                {NodeClass.SimpleExpression,simpleExpression }
            };
        }

        private void LoopBound(TreeNode N)
        {

        }

        private void FunctionCall(TreeNode N)
        {

        }

        public void declaration(TreeNode N)
        {
            if (Children.Count != 0)
            {
                if (N.NodeType==NodeClass.Assignment)
                {

                }
                else if (verifyID(N.label) == null)
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
            if (Children.Count == 0)
            {
                Identifier ID = verifyID(N.Token.Literal);
                if (ID == null)
                {
                    RegisterInconsistancy(unknwonIdentifier.Replace(LineMacro, N.Token.Line.ToString()));
                    Attributes[Attribute.Datatype] = Datatype.Undefined;
                }
                else if (N.NodeType==NodeClass.Assignment && ID is FunctionID)
                {
                    RegisterInconsistancy(CallMismatch.Replace(LineMacro, N.Token.ToString()));
                }
                else if (N.NodeType==NodeClass.FunctionCall && !(ID is FunctionID))
                {
                    RegisterInconsistancy(CallMismatch.Replace(LineMacro, N.Token.ToString()));
                }
                else
                {
                    N.Attributes[Attribute.Datatype] = ID.datatype;
                    Attributes[Attribute.Datatype] = ID.datatype;
                }
            }
            else
            {
                Datatype EntryDatatype=Datatype.Undefined;
                Identifier ID = verifyID(Children[0].Token.Literal);
                if (N.NodeType==NodeClass.Assignment)
                {
                    EntryDatatype = N.DataType;
                }
                else
                {
                    if (N.Token.Type==Refrence.Class.Assignment_Identifier)
                    {
                        Identifier EntryID = verifyID(N.Token.Literal);
                        EntryDatatype = EntryID.datatype;
                    }
                    else
                    {
                        EntryDatatype = (Datatype)N.Token.Type - 2;
                    }
                    N.Attributes[Attribute.Datatype] = EntryDatatype;
                }

                if (ID.DatatypeRestriction(Children.Count - 1) != EntryDatatype)
                {
                    RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Token.Line.ToString()));
                }

            }
        }

        private void ScopeResolution(TreeNode N)
        {

        }

        private void FnSignature(TreeNode N)
        {
            if (Children.Count != 0)
            {
                FunctionID signature = verifyID(Children[0].Token.Literal) as FunctionID;
                foreach (TreeNode Child in N.Children)
                {
                    fnAppendID(Child.Children[1].Token.Literal,
                        new Identifier((Datatype)Child.Attributes[Attribute.Datatype], Child.Children[1].Token.Literal));
                    signature.Parameters.Add((Datatype)Child.Attributes[Attribute.Datatype]);
                }
            }
            else if (Children.Count == 0)
            {
                Identifier signature = verifyID(N.Token.Literal);
                if (signature == null)
                {
                    RegisterInconsistancy(unknwonIdentifier.Replace(LineMacro, N.Token.Line.ToString()));
                }
                else
                {
                    Attributes[Attribute.Datatype] = signature.datatype;
                    N.Attributes[Attribute.Datatype] = signature.datatype;
                }
            }
        }

        private void Directive(TreeNode N)
        {
            if (Children.Count!=0 && Children[0].Token.Type == Refrence.Class.Directive_return)
            {
                //if (N.Attributes[Attribute.Datatype] != Attributes[Attribute.Datatype])
                //{
                //    RegisterInconsistancy(returnTypemismatch.Replace(LineMacro, N.Token.Line.ToString()));
                //}
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
                    RegisterInconsistancy(TypeMismatch.Replace(LineMacro,N.Token.Line.ToString()));
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
                    RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Token.Line.ToString()));
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
                    RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Token.Line.ToString()));
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

                        if (Children.Count >= 2)
                        {
                            postFixAppend("Const(" + N.Literal + ")");
                        }
                        else
                        {
                            appendToValue("Const(" + N.Literal + ")");
                        }
                        break;
                    case Refrence.Class.Assignment_Identifier:
                        Identifier ID = verifyID(N.Literal);
                        if (ID == null) { RegisterInconsistancy(unknwonIdentifier.Replace(LineMacro, N.Token.Line.ToString())); }
                        else
                        {
                            Attributes[Attribute.Datatype] = ID.datatype;
                            N.Attributes[Attribute.Datatype] = ID.datatype;
                            if (Children.Count == 2)
                            {
                                postFixAppend("Var(" + N.Literal + ")");
                            }
                            else
                            {
                                appendToValue("Var(" + N.Literal + ")");
                            }
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
                        RegisterInconsistancy(TypeMismatch.Replace(LineMacro, N.Token.Line.ToString()));
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
                        val += N.Children[i].Literal;
                        if (i < N.Children.Count - 1) { val += ","; }
                    }
                    appendToValue(val+"))");
                }
            }
        }
    }
}