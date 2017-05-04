using System;

namespace Compiler{
    public class LiteralIntBinaryTokenGenerator : LiteralIntTokenGenerator
    {
        public LiteralIntBinaryTokenGenerator(int lexemeRow, int lexemeCol)
        {
            this.lexemeRow = lexemeRow;
            this.lexemeCol = lexemeCol;
        }

        public override Token getToken()
        {
            lexeme.Append(currentSymbol.character);
            currentSymbol = inputString.GetNextSymbol();
            if(!("01".IndexOf(currentSymbol.character)>=0))
                throw new InvalidNumberException("Bad construction of [BINARY] number. line:"+lexemeRow+","+lexemeCol);
            do{
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }while("01".IndexOf(currentSymbol.character)>=0);
            return new Token(TokenType.LIT_INT,lexeme.ToString(),lexemeRow,lexemeCol);
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if("bB".IndexOf(currentSymbol.character)>=0)
                return true;
            return false;
        }
    }
}