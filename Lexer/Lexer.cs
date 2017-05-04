using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Lexer
    {
        private InputString inputString;
        private Symbol currentSymbol;
        private List<ITokenGenerator> tokenGenerators;

        public Lexer(InputString inputString, List<ITokenGenerator> tokenGenerators)
        {
            this.inputString = inputString;
            this.tokenGenerators = tokenGenerators;
            this.currentSymbol = inputString.GetNextSymbol();
        }

        public Token GetNextToken()
        {
            while(Char.IsWhiteSpace(currentSymbol.character))
            {
                currentSymbol = inputString.GetNextSymbol();
            }

            foreach(var tokenGenerator in tokenGenerators)
            {
                if(tokenGenerator.validStart(currentSymbol))
                {
                    tokenGenerator.setCurrentSymbol(currentSymbol);
                    tokenGenerator.setInputString(inputString);
                    var tokenToReturn = tokenGenerator.getToken();
                    this.currentSymbol = tokenGenerator.getCurrentSymbol();
                    this.inputString = tokenGenerator.getInputString();
                    return tokenToReturn;
                }
            }

            throw new LexicalException("Symbol '"
                + currentSymbol.character +"' not supported line:"
                + currentSymbol.rowCount +","
                + currentSymbol.colCount +".");
        }
    }
}
