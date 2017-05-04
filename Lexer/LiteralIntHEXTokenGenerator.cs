using System;

namespace Compiler{
    public class LiteralIntHEXTokenGenerator : LiteralIntTokenGenerator
    {
        public LiteralIntHEXTokenGenerator(int lexemeRow, int lexemeCol)
        {
            this.lexemeRow = lexemeRow;
            this.lexemeCol = lexemeCol;
        }

        public override Token getToken()
        {
            do{
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }while(Char.IsDigit(currentSymbol.character) || "abcdefABCDEF".IndexOf(currentSymbol.character)>=0);
            return new Token(TokenType.LIT_INT,lexeme.ToString(),lexemeRow,lexemeCol);
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if("xX".IndexOf(currentSymbol.character)>=0)
                return true;
            return false;
        }
    }
}