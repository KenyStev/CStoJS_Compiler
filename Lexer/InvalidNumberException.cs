using System;

namespace Compiler
{
    internal class InvalidNumberException : Exception
    {
        public InvalidNumberException()
        {
        }

        public InvalidNumberException(string message) : base(message)
        {
        }

        public InvalidNumberException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}