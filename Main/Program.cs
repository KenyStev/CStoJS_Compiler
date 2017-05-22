
using System.Collections.Generic;
using System.IO;
using System.Text;
using Compiler;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            // var inputString = new InputFile(@"..\Lexer.Tests\TokenTypeTests.cs");
            var inputString = new InputFile(@"..\Parser\unaryExpression.cs");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Parser(lexer);
            parser.parse();

            /*
            //TRY LEXER
            Token token = lexer.GetNextToken();

            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = lexer.GetNextToken();
            }

            System.Console.Out.WriteLine(token);*/

            // System.Console.ReadKey();
        }
    }
}
