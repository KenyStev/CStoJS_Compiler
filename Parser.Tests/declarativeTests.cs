using System;
using Compiler;
using Xunit;

namespace Parser.Tests
{
    public class DeclarativeTests
    {
        [Fact]
        public void usingStmtTest()
        {
            var inputString = new InputFile(@"..\..\..\testFiles\usingTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully");
        }

        [Fact]
        public void namespacesStmtTest()
        {
            var inputString = new InputFile(@"..\..\..\testFiles\namespaceTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully");
        }
    }
}
