using System;

namespace Compiler
{
    internal class ExpectedDigitBeforeException : Exception
    {
        public ExpectedDigitBeforeException()
        {
        }

        public ExpectedDigitBeforeException(string message) : base(message)
        {
        }

        public ExpectedDigitBeforeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}