using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tytanium.Scanner
{
    public static class Refrence //Language hard coded scanner refrence
    {
        public enum UpperClass
        {
            ReservedWord,       //Any reserved word
            Comment,            //A continuos stream of characters like strings
            Identifier,         //self eplainatory
            Constant,           //self eplainatory
            Symbol,             //Syntax operators like \,.(){}...etc
            Operator            //All operators ever
        }


        //Items of interest are entered in standard form Description_actualcode as in IntegerDataType_int
        //This allows the reserved word  inference to operate, minimizing debugging effort
        //if the code you wish to add is an operator please add it first to operator lookup hashtable

        public enum Class
        {
            NotApplicable,
            Assignment_Identifier,         //self eplainatory

            DataType_int,
            DataType_float,
            DataType_string,

            Directive_read,
            Directive_write,
            Directive_return,

            BranchingAgent_if,
            BranchingAgent_else,
            BranchingAgent_elseIf,
            BranchingAgent_then,
            BranchingAgent_end,
            LoopBound_repeat,          //until, repeat....etc
            LoopBound_until,

            AssignmentOperator, //:=
            SemiColon,          //;
            ArithmeticAddition,
            Arithmeticsubtraction,
            ArithmeticsubtractionOperator2,
            ArithmeticMultiplication,
            ArithmeticDivision,
            LogicOperatorAND,      // OR ||, AND && NOT to be confused with Comparison operators
            LogicOperatorOR,
            ComparisonOperatorEQ, //=,<>,<,>
            ComparisonOperatorLessThan,
            ComparisonOperatorGreaterThan,
            ComparisonOperatorNQ,

            LeftBracket,
            RightBracket,
            LeftCurlyBrace,
            RightCurlyBrace,
            Comma,

            Macro_endl         //Macro resolves to \n
        }

        public static Dictionary<string,Class> RefrenceTable = new Dictionary<string, Class>();

        public static Hashtable OperatorLookup = new Hashtable()
        {
            {Class.AssignmentOperator, ":=" },
            {Class.SemiColon,";" },
            {Class.ArithmeticAddition,"+" },
            {Class.ArithmeticMultiplication,"*" },
            {Class.Arithmeticsubtraction,"–" },
            {Class.ArithmeticsubtractionOperator2, "-" },
            {Class.ArithmeticDivision,"/" },
            {Class.LogicOperatorAND,"&&" },
            {Class.LogicOperatorOR,"||" },
            {Class.ComparisonOperatorEQ,"=" },
            {Class.ComparisonOperatorLessThan,"<" },
            {Class.ComparisonOperatorGreaterThan,">" },
            {Class.ComparisonOperatorNQ,"<>" },
            {Class.LeftBracket,"(" },
            {Class.RightBracket,")" },
            {Class.LeftCurlyBrace,"{" },
            {Class.RightCurlyBrace,"}" },
            {Class.Comma,"," }
        };

        public static void brighter()
        {
        }

        static Refrence()
        {
            int SkipStartOffset = 1;
            foreach (var item in Enum.GetValues(typeof(Class)))
            {
                if (SkipStartOffset!=0)
                {
                    SkipStartOffset--;
                    continue;
                }

                if (item.ToString().Contains('_'))
                    RefrenceTable.Add(item.ToString().Split('_')[1], (Class) item);
                else
                    RefrenceTable.Add(OperatorLookup[item].ToString(), (Class) item);
            }
        }
    }
}
