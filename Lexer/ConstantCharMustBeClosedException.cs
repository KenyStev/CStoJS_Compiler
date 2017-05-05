using System;

namespace Compiler
{
    public class ConstantCharMustBeClosedException : Exception
    {
        public ConstantCharMustBeClosedException()
        {
        }

        public ConstantCharMustBeClosedException(string message) : base(message)
        {
        }

        public ConstantCharMustBeClosedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}