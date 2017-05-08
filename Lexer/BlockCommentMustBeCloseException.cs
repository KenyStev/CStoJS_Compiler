using System;

namespace Compiler
{
    public class BlockCommentMustBeCloseException : Exception
    {
        public BlockCommentMustBeCloseException()
        {
        }

        public BlockCommentMustBeCloseException(string message) : base(message)
        {
        }

        public BlockCommentMustBeCloseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}