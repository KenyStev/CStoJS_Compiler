using System;

namespace Compiler
{
    public class EOFTokenGenerator : ITokenGenerator
    {
        public override Token getToken()
        {
            return new Token(
                    TokenType.EOF,
                    "",
                    currentSymbol.rowCount,
                    currentSymbol.colCount
                    );
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if(currentSymbol.character == '\0')
                return true;
            return false;
        }
    }
}