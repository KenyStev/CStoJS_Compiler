using System;
using System.Text;

namespace Compiler
{
    public class LiteralCharTokenGenerator : ITokenGenerator
    {
        private int lexemeRow;
        private int lexemeCol;

        public override Token getToken()
        {
            Token tokenToReturn = null;
            var lexeme = new StringBuilder();
            lexemeRow = currentSymbol.rowCount;
            lexemeCol = currentSymbol.colCount;

            lexeme.Append(currentSymbol.character);
            currentSymbol = inputString.GetNextSymbol();

            checkEOFbeforeClose();

            if(currentSymbol.character=='\\')
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                checkEOFbeforeClose();
                if(!("abfnrtv\'\"\\?".IndexOf(currentSymbol.character)>=0))
                    throw new InvalidEscapeCharacterException("excpected [abfnrtv\'\"\\?] before "+currentSymbol.character+". line:"+lexemeRow+","+lexemeCol);
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                checkEOFbeforeClose();
                if(!(currentSymbol.character=='\''))
                    throw new InvalidEscapeCharacterException("' excpected before "+currentSymbol.character+". line:"+lexemeRow+","+lexemeCol);
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                tokenToReturn = new Token(TokenType.LIT_CHAR,lexeme.ToString(),lexemeRow,lexemeCol);
            }else if(Char.IsLetterOrDigit(currentSymbol.character) || Char.IsPunctuation(currentSymbol.character) && !("\\\'".IndexOf(currentSymbol.character)>=0))
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                checkEOFbeforeClose();
                if(currentSymbol.character!='\'')
                    throw new InvalidConstantCharException();
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                tokenToReturn = new Token(TokenType.LIT_CHAR,lexeme.ToString(),lexemeRow,lexemeCol);
            }

            if(tokenToReturn==null)
                throw new InvalidConstantCharException("Bad construction of constant char. line:"+lexemeRow+","+lexemeCol);
            return tokenToReturn;
        }

        private void checkEOFbeforeClose()
        {
            if(currentSymbol.character=='\0')
                throw new ConstantCharMustBeClosedException("' expected before end of file. line:"+lexemeRow+","+lexemeCol);
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if(currentSymbol.character=='\'')
                return true;
            return false;
        }
    }
}