using System;

namespace Compiler
{
    internal class EOFTokenExpectedException : Exception
    {
        public EOFTokenExpectedException()
        {
        }

        public EOFTokenExpectedException(string message) : base(message)
        {
        }

        public EOFTokenExpectedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}