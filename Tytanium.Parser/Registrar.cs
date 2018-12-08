using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tytanium.Scanner;

namespace Tytanium.Parser
{
    public class Registrar
    {
        public enum Datatype
        {
            Datatype_int,
            Datatype_float,
            Datatype_string,
            Undefined
        }

        public enum Attribute
        {
            Variable,
            Value,
            Datatype
        };

        public Dictionary<Refrence.Class, string> Macros = new Dictionary<Refrence.Class, string>()
        {
            {Refrence.Class.Macro_endl,"\r\n" }
        };

        public Dictionary<string, Datatype> Variables = new Dictionary<string, Datatype>();
    }
}
