using System;
using Compiler;
using Xunit;

namespace Parser.Tests
{
    public class LocalVariablesStatementes
    {
        [Fact]
        private void declaringLocaleVariablesTest()
        {
            printHeader("declaringLocaleVariablesTest");
            var inputString = new InputFile(@"..\..\..\testFiles\localVariablesTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> local Variables");
        }

        private void printHeader(string v)
        {
            Console.Out.WriteLine("----------------------------------------");
            Console.Out.WriteLine(v);
            Console.Out.WriteLine("----------------------------------------");
        }
    }
}