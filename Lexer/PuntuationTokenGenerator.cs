using System;
using System.Collections.Generic;

namespace Compiler
{
    public class PuntuationTokenGenerator : ITokenGenerator
    {
        private List<char> startPrefixes;
        private Dictionary<string, TokenType> singlePuntuation;
        public PuntuationTokenGenerator()
        {
            initializePrefixes();
            initializeSinglePsinglePuntuation();
        }

        public override Token getToken()
        {
            var lexeme = "" + currentSymbol.character;
            var lexemeRow = currentSymbol.rowCount;
            var lexemeCol = currentSymbol.colCount;
            currentSymbol = inputString.GetNextSymbol();
            return new Token(singlePuntuation[lexeme],lexeme,lexemeRow,lexemeCol);
        }

        public override bool validStart(Symbol currentSymbol)
        {
            return startPrefixes.Contains(currentSymbol.character);
        }

        private void initializeSinglePsinglePuntuation()
        {
            singlePuntuation = new Dictionary<string, TokenType>();
            singlePuntuation.Add(".",TokenType.PUNT_ACCESOR);
            singlePuntuation.Add(":",TokenType.PUNT_COLON);
            singlePuntuation.Add(",",TokenType.PUNT_COMMA);
            singlePuntuation.Add(";",TokenType.PUNT_END_STATEMENT_SEMICOLON);
            singlePuntuation.Add("(",TokenType.PUNT_PAREN_OPEN);
            singlePuntuation.Add(")",TokenType.PUNT_PAREN_CLOSE);
            singlePuntuation.Add("{",TokenType.PUNT_CURLY_BRACKET_OPEN);
            singlePuntuation.Add("}",TokenType.PUNT_CURLY_BRACKET_CLOSE);
            singlePuntuation.Add("[",TokenType.PUNT_SQUARE_BRACKET_OPEN);
            singlePuntuation.Add("]",TokenType.PUNT_SQUARE_BRACKET_CLOSE);
        }

        private void initializePrefixes()
        {
            startPrefixes = new List<char>();
            startPrefixes.Add('.');
            startPrefixes.Add(':');
            startPrefixes.Add(',');
            startPrefixes.Add(';');
            startPrefixes.Add('(');
            startPrefixes.Add(')');
            startPrefixes.Add('{');
            startPrefixes.Add('}');
            startPrefixes.Add('[');
            startPrefixes.Add(']');
        }
    }
}