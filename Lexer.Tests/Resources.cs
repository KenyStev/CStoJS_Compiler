using System;
using System.Collections.Generic;
using Compiler;

namespace Lexer.Tests
{
    internal class Resources
    {
        internal static List<ITokenGenerator> getTokenGenerators()
        {
            var tokenGenerators = new List<ITokenGenerator>();
            
            tokenGenerators.Add(new IDReservedWordTokenGenerator());
            tokenGenerators.Add(new LiteralIntTokenGenerator());
            tokenGenerators.Add(new LiteralCharTokenGenerator());
            tokenGenerators.Add(new LiteralStringTokenGenerator());
            tokenGenerators.Add(new EOFTokenGenerator());

            return tokenGenerators;
        }
    }
}