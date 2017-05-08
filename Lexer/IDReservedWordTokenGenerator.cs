using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class IDReservedWordTokenGenerator : ITokenGenerator
    {
        private Dictionary<string, TokenType> reservedWordsDict;

        public IDReservedWordTokenGenerator()
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
            } while (Char.IsLetterOrDigit(currentSymbol.character) || '_' == currentSymbol.character);

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

            //Reserved Words
            reservedWordsDict["int"] = TokenType.RW_INT;
            reservedWordsDict["string"] = TokenType.RW_STRING;
            reservedWordsDict["float"] = TokenType.RW_FLOAT;
            reservedWordsDict["char"] = TokenType.RW_CHAR;
            reservedWordsDict["bool"] = TokenType.RW_BOOL;

            //Literals
            reservedWordsDict["true"] = TokenType.LIT_BOOL;
            reservedWordsDict["false"] = TokenType.LIT_BOOL;

            //Operators
            reservedWordsDict["sizeof"] = TokenType.OP_SIZEOF;
            reservedWordsDict["is"] = TokenType.OP_IS;
            reservedWordsDict["as"] = TokenType.OP_AS;
        }
    }
}