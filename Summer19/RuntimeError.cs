using System;
using System.Runtime.Serialization;

namespace Summer19
{
    [Serializable]
    internal class RuntimeError : Exception
    {
        public RuntimeError()
        {
        }

        public RuntimeError(string message) : base(message)
        {
        }

        public RuntimeError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RuntimeError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}