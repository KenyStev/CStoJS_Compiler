using System;

namespace Compiler
{
    internal class SyntaxTokenExpectedException : Exception
    {
        public SyntaxTokenExpectedException()
        {
        }

        public SyntaxTokenExpectedException(string message) : base(message)
        {
        }

        public SyntaxTokenExpectedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}