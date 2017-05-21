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

            //Reserved Words data types
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

            //Reserved Words
            reservedWordsDict["using"] = TokenType.RW_USING;
            reservedWordsDict["abstract"] = TokenType.RW_ABSTRACT;
            reservedWordsDict["case"] = TokenType.RW_CASE;
            reservedWordsDict["class"] = TokenType.RW_CLASS;
            reservedWordsDict["continue"] = TokenType.RW_CONTINUE;
            reservedWordsDict["do"] = TokenType.RW_DO;
            reservedWordsDict["else"] = TokenType.RW_ELSE;
            reservedWordsDict["enum"] = TokenType.RW_ENUM;
            reservedWordsDict["for"] = TokenType.RW_FOR;
            reservedWordsDict["foreach"] = TokenType.RW_FOREACH;
            reservedWordsDict["if"] = TokenType.RW_IF;
            reservedWordsDict["interface"] = TokenType.RW_INTERFACE;
            reservedWordsDict["namespace"] = TokenType.RW_NAMESPACE;
            reservedWordsDict["new"] = TokenType.RW_NEW;
            reservedWordsDict["override"] = TokenType.RW_OVERRIDE;
            reservedWordsDict["private"] = TokenType.RW_PRIVATE;
            reservedWordsDict["public"] = TokenType.RW_PUBLIC;
            reservedWordsDict["protected"] = TokenType.RW_PROTECTED;
            reservedWordsDict["return"] = TokenType.RW_RETURN;
            reservedWordsDict["break"] = TokenType.RW_BREAK;
            reservedWordsDict["static"] = TokenType.RW_STATIC;
            reservedWordsDict["switch"] = TokenType.RW_SWITCH;
            reservedWordsDict["this"] = TokenType.RW_THIS;
            reservedWordsDict["void"] = TokenType.RW_VOID;
            reservedWordsDict["while"] = TokenType.RW_WHILE;
            reservedWordsDict["virtual"] = TokenType.RW_VIRTUAL;
            reservedWordsDict["base"] = TokenType.RW_BASE;
            reservedWordsDict["var"] = TokenType.RW_VAR;
        }
    }
}