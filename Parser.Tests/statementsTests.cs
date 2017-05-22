using System;
using Compiler;
using Xunit;

namespace Parser.Tests
{
    public class StatemetTests
    {
        [Fact]
        public void StmtTest()
        {
            printHeader("StmtTest");
            var inputString = new InputFile(@"..\..\..\testFiles\statementsTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> Statements Test");
        }

        private void printHeader(string v)
        {
            Console.Out.WriteLine("----------------------------------------");
            Console.Out.WriteLine(v);
            Console.Out.WriteLine("----------------------------------------");
        }
    }
}
