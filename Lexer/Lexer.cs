using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Lexer
    {
        private IInput inputString;
        private Symbol currentSymbol;
        private List<ITokenGenerator> tokenGenerators;

        public Lexer(IInput inputString, List<ITokenGenerator> tokenGenerators)
        {
            this.inputString = inputString;
            this.tokenGenerators = tokenGenerators;
            this.currentSymbol = inputString.GetNextSymbol();
        }

        public Token GetNextToken()
        {
            if(checkForCommentOrWitheSpace())
            {
                return GetNextToken();
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

        public Token LookAheadToken()
        {
            if(checkForCommentOrWitheSpace())
            {
                return GetNextToken();
            }

            foreach(var tokenGenerator in tokenGenerators)
            {
                if(tokenGenerator.validStart(currentSymbol))
                {
                    tokenGenerator.setCurrentSymbol(currentSymbol);
                    tokenGenerator.setInputString(inputString);
                    return tokenGenerator.getToken();
                }
            }

            throw new LexicalException("Symbol '"
                + currentSymbol.character +"' not supported line:"
                + currentSymbol.rowCount +","
                + currentSymbol.colCount +".");
        }

        private bool checkForCommentOrWitheSpace()
        {
            bool detected = false;
            while(Char.IsWhiteSpace(currentSymbol.character))
            {
                currentSymbol = inputString.GetNextSymbol();
                detected = true;
            }
            var mayBeComment = new StringBuilder();
            var nextOne = inputString.LookAheadSymbol();
            mayBeComment.Append(currentSymbol.character);
            mayBeComment.Append(nextOne.character);
            if(mayBeComment.ToString()=="//")
            {
                detected = true;
                do{
                    currentSymbol = inputString.GetNextSymbol();
                }while(!("\n\0".IndexOf(currentSymbol.character)>=0));
            }else if(mayBeComment.ToString()=="/*")
            {
                detected = true;
                currentSymbol = inputString.GetNextSymbol();
                do{
                    mayBeComment.Clear();
                    currentSymbol = inputString.GetNextSymbol();
                    nextOne = inputString.LookAheadSymbol();

                    if(nextOne.character=='\0')
                        throw new BlockCommentMustBeCloseException("*/ Expected before End of File. line:"+currentSymbol.rowCount+","+currentSymbol.colCount);

                    mayBeComment.Append(currentSymbol.character);
                    mayBeComment.Append(nextOne.character);
                }while(mayBeComment.ToString()!="*/");
            }
            return detected;
        }
    }
}
