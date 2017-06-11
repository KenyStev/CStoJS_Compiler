using System;

namespace Compiler
{
    public class Utils
    {
        public static void ThrowError(string message)
        {
            throw new SemanticException(message);
        }
    }
}