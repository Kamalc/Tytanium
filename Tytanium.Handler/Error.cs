using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class Error
    {
        public enum ErrorType
        {
            ScannerError,
            ParserError,
            Inconsistency,
            SynthesisFailure
        }

        public string ErrorMassege;
        public ErrorType Type;

        public Error(string msg, ErrorType ET)
        {
            ErrorMassege = msg;
            Type = ET;
        }

    }
}
