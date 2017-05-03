using System;
using System.Collections.Generic;
using Compiler;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //var inputString = new InputString(@"print IDTEST = 5595 + ( TRE ) - QUATTRO * UNO//Hola IDDUE/ALGO;");

            var inputString = new InputString(@"int hola string adios como_0101");

            var tokenGenerators = getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);

            Token token = lexer.GetNextToken();

            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = lexer.GetNextToken();
            }

            System.Console.Out.WriteLine(token);
            
            //Symbol currentSymbol = inputString.GetNextSymbol();

        
            /*while (currentSymbol.character != '\0')
            {
                Console.WriteLine("Row: " + currentSymbol.rowCount +
                    " Column: " + currentSymbol.colCount + 
                    " Char: " + currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }*/

            System.Console.ReadKey();
        }

        private static List<ITokenGenerator> getTokenGenerators()
        {
            var tokenGenerators = new List<ITokenGenerator>();
            
            tokenGenerators.Add(new IdTokenGenerator());
            tokenGenerators.Add(new EOFTokenGenerator());

            return tokenGenerators;
        }
    }
}
