using System;

namespace Compiler
{
    public class InvalidConstantStringException : Exception
    {
        public InvalidConstantStringException()
        {
        }

        public InvalidConstantStringException(string message) : base(message)
        {
        }

        public InvalidConstantStringException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}