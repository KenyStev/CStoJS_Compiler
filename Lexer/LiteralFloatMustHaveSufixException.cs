using System;

namespace Compiler
{
    public class LiteralFloatMustHaveSufixException : Exception
    {
        public LiteralFloatMustHaveSufixException()
        {
        }

        public LiteralFloatMustHaveSufixException(string message) : base(message)
        {
        }

        public LiteralFloatMustHaveSufixException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}