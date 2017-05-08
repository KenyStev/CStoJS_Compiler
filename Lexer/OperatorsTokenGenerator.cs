using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class OperatorsTokenGenerator : ITokenGenerator
    {
        private List<char> startPrefixes;
        private Dictionary<string, TokenType> singleOperators;
        private Dictionary<string, TokenType> doubleOperators;
        private Dictionary<string, TokenType> tripleOperators;
        public OperatorsTokenGenerator()
        {
            initializePrefixes();
            initializeSingleOperators();
            initializeDoubleOperators();
            initializeTripleOperators();
        }

        public override Token getToken()
        {
            var nextOne = inputString.LookAheadSymbol(0);
            var nextTwo = inputString.LookAheadSymbol(1);
            var op = new StringBuilder();
            op.Append(currentSymbol.character);
            if(nextOne.character!='\0')op.Append(nextOne.character);
            if(nextTwo.character!='\0')op.Append(nextTwo.character);
            var lexeme = new StringBuilder();
            var lexemeRow = currentSymbol.rowCount;
            var lexemeCol = currentSymbol.colCount;

            lexeme.Append(currentSymbol.character);
            currentSymbol = inputString.GetNextSymbol();

            if(tripleOperators.ContainsKey(op.ToString()))
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                return new Token(tripleOperators[lexeme.ToString()],lexeme.ToString(),lexemeRow,lexemeCol);
            }
            op.Clear();
            op.Append(currentSymbol.character);
            if(nextOne.character!='\0')op.Append(nextOne.character);
            if(doubleOperators.ContainsKey(op.ToString()))
            {
                lexeme.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
                return new Token(doubleOperators[lexeme.ToString()],lexeme.ToString(),lexemeRow,lexemeCol);
            }
            return new Token(singleOperators[lexeme.ToString()],lexeme.ToString(),lexemeRow,lexemeCol);
        }

        public override bool validStart(Symbol currentSymbol)
        {
            return startPrefixes.Contains(currentSymbol.character);
        }

        private void initializeTripleOperators()
        {
            tripleOperators = new Dictionary<string, TokenType>();
            tripleOperators.Add("<<=",TokenType.OP_ASSIGN_SHIFT_LEFT);
            tripleOperators.Add(">>=",TokenType.OP_ASSIGN_SHIFT_RIGHT);
        }

        private void initializeDoubleOperators()
        {
            doubleOperators = new Dictionary<string, TokenType>();
            doubleOperators.Add("+=",TokenType.OP_ASSIGN_SUM);
            doubleOperators.Add("-=",TokenType.OP_ASSIGN_SUBSTRACT);
            doubleOperators.Add("*=",TokenType.OP_ASSIGN_MULTIPLICATION);
            doubleOperators.Add("/=",TokenType.OP_ASSIGN_DIVISION);
            doubleOperators.Add("%=",TokenType.OP_ASSIGN_MODULO);
            doubleOperators.Add("&=",TokenType.OP_ASSIGN_BITWISE_AND);
            doubleOperators.Add("^=",TokenType.OP_ASSIGN_XOR);
            doubleOperators.Add("|=",TokenType.OP_ASSIGN_BITWISE_OR);
            doubleOperators.Add("<<",TokenType.OP_SHIFT_LEFT);
            doubleOperators.Add(">>",TokenType.OP_SHIFT_RIGHT);
            doubleOperators.Add("&&",TokenType.OP_AND);
            doubleOperators.Add("||",TokenType.OP_OR);
            doubleOperators.Add("??",TokenType.OP_NULL_COALESCING);
            doubleOperators.Add("==",TokenType.OP_EQUAL);
            doubleOperators.Add("!=",TokenType.OP_DISTINCT);
            doubleOperators.Add(">=",TokenType.OP_MORE_AND_EQUAL_THAN);
            doubleOperators.Add("<=",TokenType.OP_LESS_AND_EQUAL_THAN);
            doubleOperators.Add("++",TokenType.OP_PLUS_PLUS);
            doubleOperators.Add("--",TokenType.OP_MINUS_MINUS);
        }

        private void initializeSingleOperators()
        {
            singleOperators = new Dictionary<string, TokenType>();
            singleOperators.Add("=",TokenType.OP_ASSIGN);
            singleOperators.Add("&",TokenType.OP_BITWISE_AND);
            singleOperators.Add("|",TokenType.OP_BITWISE_OR);
            singleOperators.Add("^",TokenType.OP_XOR);
            singleOperators.Add("~",TokenType.OP_BITWISE_NOT);
            singleOperators.Add("!",TokenType.OP_NOT);
            singleOperators.Add("?",TokenType.OP_TERNARY);
            singleOperators.Add(">",TokenType.OP_MORE_THAN);
            singleOperators.Add("<",TokenType.OP_LESS_THAN);
            singleOperators.Add("+",TokenType.OP_SUM);
            singleOperators.Add("-",TokenType.OP_SUBSTRACT);
            singleOperators.Add("*",TokenType.OP_MULTIPLICATION);
            singleOperators.Add("/",TokenType.OP_DIVISION);
            singleOperators.Add("%",TokenType.OP_MODULO);
        }

        private void initializePrefixes()
        {
            startPrefixes = new List<char>();
            startPrefixes.Add('=');
            startPrefixes.Add('+');
            startPrefixes.Add('-');
            startPrefixes.Add('*');
            startPrefixes.Add('/');
            startPrefixes.Add('%');
            startPrefixes.Add('<');
            startPrefixes.Add('>');
            startPrefixes.Add('&');
            startPrefixes.Add('^');
            startPrefixes.Add('|');
            startPrefixes.Add('~');
            startPrefixes.Add('!');
            startPrefixes.Add('?');
        }
    }
}