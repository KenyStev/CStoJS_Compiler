using System;
using System.Collections.Generic;
using System.Text;
using Compiler;

namespace Compiler
{
    public class IdTokenGenerator : ITokenGenerator
    {
        private Dictionary<string, TokenType> reservedWordsDict;

        public IdTokenGenerator()
        {
            InitReservedWordsDictionary();
        }

        public override Token getToken()
        {
            var lexeme = new StringBuilder();
            var lexemeRow = currentSymbol.rowCount;
            var lexemeCol = currentSymbol.colCount;
            do
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            } while (Char.IsLetter(currentSymbol.character) || Char.IsDigit(currentSymbol.character) || '_' == currentSymbol.character);

            var tokenType = reservedWordsDict.ContainsKey(lexeme.ToString()) ? 
                reservedWordsDict[lexeme.ToString()] : TokenType.ID;
            
            return new Token(
                tokenType,
                lexeme.ToString(),
                lexemeRow,
                lexemeCol
            );
        }

        public override bool validStart(Symbol currentSymbol)
        {
            if(Char.IsLetter(currentSymbol.character))
            {
                return true;
            }
            return false;
        }

        private void InitReservedWordsDictionary()
        {
            reservedWordsDict = new Dictionary<string, TokenType>();
            reservedWordsDict["int"] = TokenType.RW_INT;
            reservedWordsDict["string"] = TokenType.RW_STRING;
        }
    }
}