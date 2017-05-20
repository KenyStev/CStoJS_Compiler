using System;
using System.Collections.Generic;

namespace Compiler
{
    public class Resources
    {
        public static List<ITokenGenerator> getTokenGenerators()
        {
            var tokenGenerators = new List<ITokenGenerator>();
            
            tokenGenerators.Add(new IDReservedWordTokenGenerator());
            tokenGenerators.Add(new LiteralIntTokenGenerator());
            tokenGenerators.Add(new LiteralCharTokenGenerator());
            tokenGenerators.Add(new LiteralStringTokenGenerator());
            tokenGenerators.Add(new OperatorsTokenGenerator());
            tokenGenerators.Add(new PuntuationTokenGenerator());
            tokenGenerators.Add(new EOFTokenGenerator());

            return tokenGenerators;
        }
    }
}