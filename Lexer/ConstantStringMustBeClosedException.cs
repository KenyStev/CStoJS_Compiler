using System;

namespace Compiler
{
    public class ConstantStringMustBeClosedException : Exception
    {
        public ConstantStringMustBeClosedException()
        {
        }

        public ConstantStringMustBeClosedException(string message) : base(message)
        {
        }

        public ConstantStringMustBeClosedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}