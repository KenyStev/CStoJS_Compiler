using System;

namespace Compiler
{
    public class InvalidEscapeCharacterException : Exception
    {
        public InvalidEscapeCharacterException()
        {
        }

        public InvalidEscapeCharacterException(string message) : base(message)
        {
        }

        public InvalidEscapeCharacterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}