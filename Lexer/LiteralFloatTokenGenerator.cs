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
            lexeme.Append(currentSymbol.character);
            if(currentSymbol.character == '.')
            {
                currentSymbol = inputString.GetNextSymbol();
                if(!Char.IsDigit(currentSymbol.character))
                    throw new ExpectedDigitBeforeException("Expected [0-9] before '"+ currentSymbol.character +"'.");

                do{
                    lexeme.Append(currentSymbol.character);
                    currentSymbol = inputString.GetNextSymbol();
                }while(Char.IsDigit(currentSymbol.character));

                if(!setLiteralFloatSufix())
                    throw new LiteralFloatMustHaveSufixException("The literal '"+lexeme.ToString()+"' must end with: [fF].");
                return new Token(TokenType.LIT_FLOAT,lexeme.ToString(),lexemeRow,lexemeCol);
            }
            currentSymbol = inputString.GetNextSymbol();
            return new Token(TokenType.LIT_FLOAT,lexeme.ToString(),lexemeRow,lexemeCol);
        }

        private bool setLiteralFloatSufix()
        {
            if("fF".IndexOf(currentSymbol.character)>=0)
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                return true;
            }
            return false;
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if(".fF".IndexOf(currentSymbol.character)>=0)
                return true;
            return false;
        }
    }
}