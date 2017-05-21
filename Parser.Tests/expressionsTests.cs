using System;
using Compiler;
using Xunit;

namespace Parser.Tests
{
    public class ExpressionesTests
    {
        [Fact]
        public void expressionsTest()
        {
            printHeader("expressionsTest");
            var inputString = new InputFile(@"..\..\..\testFiles\expressionsTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> expressions");
        }

        private void printHeader(string v)
        {
            Console.Out.WriteLine("----------------------------------------");
            Console.Out.WriteLine(v);
            Console.Out.WriteLine("----------------------------------------");
        }
    }
}