using System;
using System.Runtime.Serialization;

namespace Summer19
{
    [Serializable]
    internal class ParseError : Exception
    {
        private TokenType type;
        private string message;

        public ParseError()
        {
        }

        public ParseError(string message) : base(message)
        {
        }

        public ParseError(TokenType type, string message)
        {
            this.type = type;
            this.message = message;
        }

        public ParseError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParseError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}