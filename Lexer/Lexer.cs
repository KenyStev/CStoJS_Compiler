using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Lexer
    {
        private InputString inputString;
        private Symbol currentSymbol;
        private Dictionary<string, TokenType> reservedWordsDict;

        public Lexer(InputString inputString)
        {
            this.inputString = inputString;
            this.currentSymbol = inputString.GetNextSymbol();
            InitReservedWordsDictionary();
        }

        public Token GetNextToken()
        {
            if (currentSymbol.character == '/')
            {
                string placeholderLexema = currentSymbol.character.ToString();

                currentSymbol = inputString.GetNextSymbol();

                if (currentSymbol.character == '/')
                {
                    do
                       {
                        currentSymbol = inputString.GetNextSymbol();
                    } while (currentSymbol.character != '\n' || currentSymbol.character == '\0');
                }
                else
                {
                    var lexemaRow = currentSymbol.rowCount;
                    var lexemaColumn = currentSymbol.colCount;
                    
                    return new Token(
                        TokenType.OP_DIVISION,
                        placeholderLexema, 
                        lexemaRow,
                        lexemaColumn
                        );
                }
            }

            while (Char.IsWhiteSpace(currentSymbol.character))
            {
                currentSymbol = inputString.GetNextSymbol();
            }

            if (Char.IsLetter(currentSymbol.character))
            {
                var lexema = new StringBuilder();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaCol = currentSymbol.colCount;
                do
                {
                    lexema.Append(currentSymbol.character);
                    currentSymbol = inputString.GetNextSymbol();
                } while (Char.IsLetter(currentSymbol.character));

                var tokenType = reservedWordsDict.ContainsKey(lexema.ToString()) ? 
                    reservedWordsDict[lexema.ToString()] : TokenType.ID;
                
                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaCol
                );
            }
            else if (currentSymbol.character == '+')
            {
                string lexema = currentSymbol.character.ToString();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;

                currentSymbol = inputString.GetNextSymbol();
                
                return new Token(
                    TokenType.OP_SUM,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (currentSymbol.character == '-')
            {
                string lexema = currentSymbol.character.ToString();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;

                currentSymbol = inputString.GetNextSymbol();

                return new Token(
                    TokenType.OP_SUBSTRACT,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (currentSymbol.character == '*')
            {
                string lexema = currentSymbol.character.ToString();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;

                currentSymbol = inputString.GetNextSymbol();

                return new Token(
                    TokenType.OP_MULTIPLICATION,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (currentSymbol.character == '=')
            {
                string lexema = currentSymbol.character.ToString();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;

                currentSymbol = inputString.GetNextSymbol();

                return new Token(
                    TokenType.OP_ASSIGN,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (currentSymbol.character == '(')
            {
                var lexema = currentSymbol.character.ToString();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;
                currentSymbol = inputString.GetNextSymbol();

                return new Token(
                    TokenType.PAREN_OPEN,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (currentSymbol.character == ')')
            {
                var lexema = currentSymbol.character.ToString();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;
                currentSymbol = inputString.GetNextSymbol();

                return new Token(
                    TokenType.PAREN_CLOSE,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (currentSymbol.character == ';')
            {
                var lexema = currentSymbol.character.ToString();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;
                currentSymbol = inputString.GetNextSymbol();

                return new Token(
                    TokenType.END_STATEMENT,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (Char.IsDigit(currentSymbol.character))
            {
                var lexema = new StringBuilder();
                var lexemaRow = currentSymbol.rowCount;
                var lexemaColumn = currentSymbol.colCount;

                do
                {
                    lexema.Append(currentSymbol.character.ToString());
                    currentSymbol = inputString.GetNextSymbol();
                } while (Char.IsDigit(currentSymbol.character));

                return new Token(
                    TokenType.LIT_INT,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (currentSymbol.character == '\0')
            {
                return new Token(
                    TokenType.EOF,
                    "",
                    currentSymbol.rowCount,
                    currentSymbol.colCount
                    );
            }
            else
            {
                throw new LexicalException("Symbol not supported.");
            }
        }

        private void InitReservedWordsDictionary()
        {
            reservedWordsDict = new Dictionary<string, TokenType>();
            reservedWordsDict["print"] = TokenType.PRINT_CALL;
            reservedWordsDict["read"] = TokenType.READ_CALL;
        }
    }
}
