using System;

namespace Compiler
{
    public class LiteralVerbatimStringokenGenerator : LiteralStringTokenGenerator
    {
        public LiteralVerbatimStringokenGenerator(int lexemeRow, int lexemeCol)
        {
            this.lexemeRow = lexemeRow;
            this.lexemeCol = lexemeCol;
        }

        public override Token getToken()
        {
            if(currentSymbol.character!='"')
                throw new InvalidConstantStringException("\" expected before "+currentSymbol.character+". line:"+lexemeRow+","+lexemeCol);
            
            lexeme.Append(currentSymbol.character);
            currentSymbol = inputString.GetNextSymbol();

            do{
                checkEOFbeforeClose();
                checkForValidString();
                checkForEscapeCharacter();
            }while(currentSymbol.character!='"');
            lexeme.Append(currentSymbol.character);
            currentSymbol = inputString.GetNextSymbol();

            return new Token(TokenType.LIT_STRING,lexeme.ToString(),lexemeRow,lexemeCol);
        }

        private void checkForValidString()
        {
            if(currentSymbol.character!='"')
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }
            checkEOFbeforeClose();
        }

        private void checkForEscapeCharacter()
        {
            if(currentSymbol.character=='"')
            {
                Symbol aheadSymbol = inputString.LookAheadSymbol();
                if(aheadSymbol.character=='"')
                {
                    lexeme.Append(currentSymbol.character);
                    currentSymbol = inputString.GetNextSymbol();
                    lexeme.Append(currentSymbol.character);
                    currentSymbol = inputString.GetNextSymbol();
                }
            }
            checkEOFbeforeClose();
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if(currentSymbol.character=='@')
                return true;
            return false;
        }
    }
}