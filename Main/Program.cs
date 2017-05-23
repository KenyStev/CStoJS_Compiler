
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
            /*{var inputString = new InputFile(@"..\Lexer.Tests\TokenTypeTests.cs");
                var inputString = new InputFile(@"..\Parser\unaryExpression.cs");
                var inputString = new InputString(@"
                class MyClass
                {
                    MyClass(Nombre val)
                    {

                    }

                    public me()
                    {

                    }
                }
                ");}
            */
            var inputString = new InputFile(@"..\Parser.Tests\testFiles\compiiiss1.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Parser(lexer);
            try{
                parser.parse();
                System.Console.Out.WriteLine("Success!");
            }catch(SyntaxTokenExpectedException ex){
                System.Console.Out.WriteLine(ex.Message);
            }
            

            /*
            //TRY LEXER
            Token token = lexer.GetNextToken();

            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = lexer.GetNextToken();
            }

            System.Console.Out.WriteLine(token);*/
        }
    }
}
