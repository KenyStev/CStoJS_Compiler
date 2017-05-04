using System;

namespace Compiler{
    public class LiteralFloatTokenGenerator : LiteralIntTokenGenerator
    {
        public LiteralFloatTokenGenerator(int lexemeRow, int lexemeCol)
        {
            this.lexemeRow = lexemeRow;
            this.lexemeCol = lexemeCol;
        }

        public override Token getToken()
        {
            throw new NotImplementedException();
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if(currentSymbol.character == '.' || currentSymbol.character == 'f' || currentSymbol.character == 'F')
                return true;
            return false;
        }
    }
}