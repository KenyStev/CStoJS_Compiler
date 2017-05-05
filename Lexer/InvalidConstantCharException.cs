using System;

namespace Compiler
{
    public class InvalidConstantCharException : Exception
    {
        public InvalidConstantCharException()
        {
        }

        public InvalidConstantCharException(string message) : base(message)
        {
        }

        public InvalidConstantCharException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}