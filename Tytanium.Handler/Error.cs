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

        public string ErrorMessage;
        public ErrorType Type;

        public Error(string msg, ErrorType ET)
        {
            ErrorMessage = msg;
            Type = ET;
        }

    }
}
