using System;
using Compiler.TreeNodes;

namespace Compiler
{
    public class Utils
    {
        public static void ThrowError(string message)
        {
            throw new SemanticException(message);
        }

        public static bool isValidEncapsulation(EncapsulationNode encapsulation, TokenType encapsulationType)
        {
            if(encapsulation == null)
                return true;
            return encapsulation.type == encapsulationType;
        }
    }
}