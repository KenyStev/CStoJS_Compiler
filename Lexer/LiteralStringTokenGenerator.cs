using System;
using System.Text;

namespace Compiler
{
    public class LiteralStringTokenGenerator : ITokenGenerator
    {
        protected StringBuilder lexeme;
        protected int lexemeRow;
        protected int lexemeCol;

        public override Token getToken()
        {
            lexeme = new StringBuilder();
            lexemeRow = currentSymbol.rowCount;
            lexemeCol = currentSymbol.colCount;

            var literalVarbatim = new LiteralVerbatimStringokenGenerator(lexemeRow, lexemeCol);
            
            if(literalVarbatim.validStart(currentSymbol))
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                literalVarbatim.setCurrentSymbol(currentSymbol);
                literalVarbatim.setInputString(inputString);
                literalVarbatim.setLexeme(lexeme);
                var tokenToReturn = literalVarbatim.getToken();
                currentSymbol = literalVarbatim.getCurrentSymbol();
                inputString = literalVarbatim.getInputString();
                return tokenToReturn;
            }
            lexeme.Append(currentSymbol.character);
            currentSymbol = inputString.GetNextSymbol();

            do{
                checkEOFbeforeClose();
                checkForEscapeCharacter();
                checkForValidString();
            }while(currentSymbol.character!='"');

            lexeme.Append(currentSymbol.character);
            currentSymbol = inputString.GetNextSymbol();

            return new Token(TokenType.LIT_STRING,lexeme.ToString(),lexemeRow,lexemeCol);
        }

        private void checkForValidString()
        {
            if(("\n".IndexOf(currentSymbol.character)>=0))
                throw new InvalidConstantStringException("unexpected: "+currentSymbol.character+ "Bad construction of constant string. line:"+lexemeRow+","+lexemeCol);

            if(currentSymbol.character!='"')
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }
            checkEOFbeforeClose();
        }

        private void checkForEscapeCharacter()
        {
            if(currentSymbol.character=='\\')
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                checkEOFbeforeClose();
                if(!("0abfnrtv\'\"\\?".IndexOf(currentSymbol.character)>=0))
                    throw new InvalidEscapeCharacterException("excpected [0abfnrtv\'\"\\?] before "+currentSymbol.character+". line:"+lexemeRow+","+lexemeCol);
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }
            checkEOFbeforeClose();
        }

        protected void checkEOFbeforeClose()
        {
            if(currentSymbol.character=='\0')
                throw new ConstantStringMustBeClosedException("\" expected before end of file. line:"+lexemeRow+","+lexemeCol);
        }

        private void setLexeme(StringBuilder lexeme)
        {
            this.lexeme = lexeme;
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if("@\"".IndexOf(currentSymbol.character)>=0)
                return true;
            return false;
        }
    }
}