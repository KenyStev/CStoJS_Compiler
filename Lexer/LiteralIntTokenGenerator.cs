using System;
using System.Text;

namespace Compiler
{
    public class LiteralIntTokenGenerator : ITokenGenerator
    {
        protected StringBuilder lexeme;
        protected int lexemeRow;
        protected int lexemeCol;

        public override Token getToken()
        {
            lexeme = new StringBuilder();
            lexemeRow = currentSymbol.rowCount;
            lexemeCol = currentSymbol.colCount;

            var literalHex = new LiteralIntHEXTokenGenerator(lexemeRow, lexemeCol);
            var literalBin = new LiteralIntBinaryTokenGenerator(lexemeRow, lexemeCol);
            var literalFloat = new LiteralFloatTokenGenerator(lexemeRow, lexemeCol);

            if(currentSymbol.character == '0'){
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                if(literalHex.validStart(currentSymbol))
                {
                    //TODO: Hex
                    literalHex.setCurrentSymbol(currentSymbol);
                    literalHex.setInputString(inputString);
                    literalHex.setLexeme(lexeme);
                    var tokenToReturn = literalHex.getToken();
                    currentSymbol = literalHex.getCurrentSymbol();
                    inputString = literalHex.getInputString();
                    return tokenToReturn;
                }else if(literalBin.validStart(currentSymbol))
                {
                    //TODO: Binary
                    literalBin.setCurrentSymbol(currentSymbol);
                    literalBin.setInputString(inputString);
                    literalBin.setLexeme(lexeme);
                    var tokenToReturn = literalBin.getToken();
                    currentSymbol = literalBin.getCurrentSymbol();
                    inputString = literalBin.getInputString();
                    return tokenToReturn;
                }
            }

            while(Char.IsDigit(currentSymbol.character)){
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }

            if(literalFloat.validStart(currentSymbol))
            {
                //TODO: float
                literalFloat.setCurrentSymbol(currentSymbol);
                literalFloat.setInputString(inputString);
                literalFloat.setLexeme(lexeme);
                var tokenToReturn = literalFloat.getToken();
                currentSymbol = literalFloat.getCurrentSymbol();
                inputString = literalFloat.getInputString();
                return tokenToReturn;
            }

            return new Token(TokenType.LIT_INT,lexeme.ToString(),lexemeRow,lexemeCol);
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if(Char.IsDigit(currentSymbol.character))
                return true;
            return false;
        }

        protected void setLexeme(StringBuilder lexeme){
            this.lexeme = lexeme;
        }
    }
}