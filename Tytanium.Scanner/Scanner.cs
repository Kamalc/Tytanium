using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handler;

namespace Tytanium.Scanner
{
    public class Scanner
    {
        const string EndOFFile = "End of File was found before meeting a match for the %ITEM% in in line %LINE%";
        const string FormatError = "Format of constant %TYPE% in line %CHAR% is invalid : %REASON%";
        const string OperatorInvalid = "usage of operator %LITERAL% is unrecognized! in line %LINE%";
        const string TokenInvalid = "usage of invalid token at line %LINE%";

        int curLine = 1;

        public List<Error> ErrorList = new List<Error>(); 

        public List<Token> Tokens = new List<Token>();

        delegate void ScanFn();

        Dictionary<string, ScanFn> Scanners = new Dictionary<string, ScanFn>();
        int ScannerMaxLength = int.MinValue;
        List<char> Numerics = new List<char>();
        List<char> Alphabet = new List<char>();

        int ScannerLocation=0;
        string SubjectText;

        bool isWS()
        {
            if (SubjectText[ScannerLocation] == ' ' || SubjectText[ScannerLocation] == '\n' || SubjectText[ScannerLocation] == '\t')
                return true;
            return false;
        }

        void SkipWS()
        {
            while (SubjectText[ScannerLocation] == ' ' || SubjectText[ScannerLocation] == '\n' || SubjectText[ScannerLocation] == '\t')
            {
                if (SubjectText[ScannerLocation] == '\n')
                {
                    curLine++;
                }
                ScannerLocation++;
                if (ScannerLocation >= SubjectText.Length)
                {
                    return;
                }
            }
        }

        public Scanner(string Entry)
        {
            SubjectText = Entry;

            Scanners.Add("_", Literal);

            for (char c='A';c<='Z';c++)
            {
                Alphabet.Add(c);
                Scanners.Add(c.ToString(), Literal);
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                Alphabet.Add(c);
                Scanners.Add(c.ToString(), Literal);
            }

            Alphabet.Add('_');

            for (char c = '0'; c <= '9'; c++)
            {
                Numerics.Add(c);
                Scanners.Add(c.ToString(), Numeric);
            }
            Numerics.Add('.');

            foreach (string s in Refrence.OperatorLookup.Values)
            {
                Scanners.Add(s, Operator);
            }

            Scanners.Add("/*", Comment);
            Scanners.Add("\"", String);

            foreach (string s in Scanners.Keys)
            {
                if (s.Length > ScannerMaxLength)
                    ScannerMaxLength = s.Length;
            }

            Alphabet.Add('#');
        }

        void Literal()
        {
            string buffer = "";
            while (!isWS() && Alphabet.Contains(SubjectText[ScannerLocation]) || Numerics.Contains(SubjectText[ScannerLocation]) && ScannerLocation<SubjectText.Length)
            {
                buffer += SubjectText[ScannerLocation++];
            }
            if (Refrence.RefrenceTable.Keys.Contains(buffer))
                Tokens.Add(new Token(buffer, Refrence.UpperClass.ReservedWord, Refrence.RefrenceTable[buffer]));
            else
                Tokens.Add(new Token(buffer, Refrence.UpperClass.Identifier,Refrence.Class.NotApplicable));
        }

        void Comment()
        {
            string buffer = "";
            while (ScannerLocation < SubjectText.Length)
            {
                //ERROR DETECTION BRANCH
                if (ScannerLocation>=SubjectText.Length)
                {
                    ErrorList.Add(new Error(EndOFFile.Replace("%ITEM%","Comment Itiator").Replace("%LINE%", curLine.ToString()) , Error.ErrorType.ScannerError));
                }

                if (SubjectText[ScannerLocation]=='*' && ScannerLocation<SubjectText.Length-2 && SubjectText[ScannerLocation+1]=='/'&&buffer.Length>2)
                {
                    buffer += "*/";
                    ScannerLocation += 1;
                    break;
                }
                buffer += SubjectText[ScannerLocation++];

            }
            Tokens.Add(new Token(buffer, Refrence.UpperClass.Comment));
        }

        void String()
        {
            string buffer = "\"";
            ScannerLocation++;
            while (ScannerLocation < SubjectText.Length)
            {
                //ERROR DETECTION BRANCH


                buffer += SubjectText[ScannerLocation];
                if (SubjectText[ScannerLocation]=='\"')
                {
                    ScannerLocation++;
                    break;
                }
                else
                {
                    ScannerLocation++;
                    if (ScannerLocation >= SubjectText.Length)
                    {
                        ErrorList.Add(new Error(EndOFFile.Replace("%ITEM%", "String Intiator").Replace("%LINE%", "containing string" + buffer), Error.ErrorType.ScannerError));
                        return;
                    }
                }

            }

            Tokens.Add(new Token(buffer, Refrence.UpperClass.Constant,Refrence.Class.DataType_string));
        }

        void Numeric()
        {
            bool floated = false;
            string buffer = "";

            while (Numerics.Contains(SubjectText[ScannerLocation]))
            {
                buffer += SubjectText[ScannerLocation];
                if (SubjectText[ScannerLocation] == '.')
                {
                    if (!floated)
                    {
                        floated = true;
                    }
                    else
                    {
                        ErrorList.Add(new Error(FormatError.Replace("%TYPE%", "float").Replace("%CHAR%", curLine.ToString()).Replace("%REASON%", "2 Floating points encountered!"), Error.ErrorType.ScannerError));
                        return;
                    }
                }

                ScannerLocation++;
            }

            if (!Refrence.OperatorLookup.ContainsValue(SubjectText[ScannerLocation].ToString()) && !isWS() && SubjectText[ScannerLocation] != ';')
            {
                ErrorList.Add(new Error(FormatError.Replace("%TYPE%", "\""+ buffer + SubjectText[ScannerLocation] + "\"").Replace("%CHAR%", curLine.ToString()).Replace("%REASON%", "Invalid Numeric Value"), Error.ErrorType.ScannerError));
            }

            if (floated)
                Tokens.Add(new Token(buffer, Refrence.UpperClass.Constant, Refrence.Class.DataType_float));
            else
                Tokens.Add(new Token(buffer, Refrence.UpperClass.Constant, Refrence.Class.DataType_int));

        }

        void Operator()
        {
            string buffer = ""+SubjectText[ScannerLocation];
            int maxLength=int.MinValue;

            List<string> Possiblities = new List<string>();
            Possiblities.Add(buffer);

            foreach (string s in Refrence.OperatorLookup.Values)
            {
                if (s.Length > maxLength)
                    maxLength = s.Length;
            }

            for (int i=1;i<maxLength && ScannerLocation+1<SubjectText.Length;i++)
            {
                buffer += SubjectText[ScannerLocation + i];
                Possiblities.Add(buffer);
            }

            Possiblities.Reverse();

            foreach (string s in Possiblities)
            {
                if (Refrence.RefrenceTable.Keys.Contains(s))
                {
                    Tokens.Add(new Token(s, Refrence.UpperClass.Operator, Refrence.RefrenceTable[s]));
                    ScannerLocation += s.Length;
                    return;
                }
            }
            ErrorList.Add(new Error(OperatorInvalid.Replace("LITERAL", Possiblities.First()).Replace("%LINE%",curLine.ToString()), Error.ErrorType.ScannerError));
        }

        public void Scan()
        {
            for (;ScannerLocation<SubjectText.Length;)
            {
                SkipWS();
                if (ScannerLocation >= SubjectText.Length)
                    return;

                string buffer = "" + SubjectText[ScannerLocation];

                List<string> Possiblities = new List<string>();
                Possiblities.Add(buffer);

                for (int i = 1; i < ScannerMaxLength && ScannerLocation + 1 < SubjectText.Length; i++)
                {
                    buffer += SubjectText[ScannerLocation + i];
                    Possiblities.Add(buffer);
                }

                Possiblities.Reverse();

                bool completed = false;

                foreach (string s in Possiblities)
                {
                    if (Scanners.Keys.Contains(s))
                    {
                        Scanners[s]();
                        completed = true;
                        break;
                    }
                }
                if (!completed)
                {
                    ErrorList.Add(new Error(TokenInvalid.Replace("%LINE%", curLine.ToString()),Error.ErrorType.ScannerError));
                    ScannerLocation++;
                }
                
            }
        }
    }
}
