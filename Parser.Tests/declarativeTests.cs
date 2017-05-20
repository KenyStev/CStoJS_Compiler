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
            Console.Out.WriteLine("Parsed Successfully -> usings");
        }

        [Fact]
        public void namespacesStmtTest()
        {
            var inputString = new InputFile(@"..\..\..\testFiles\namespaceTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> namespace");
        }

        [Fact]
        public void classDeclarationStmtTest()
        {
            var inputString = new InputFile(@"..\..\..\testFiles\classDeclarationTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> class");
        }

        [Fact]
        public void enumDeclarationStmtTest()
        {
            var inputString = new InputFile(@"..\..\..\testFiles\enumDeclarationTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> enum");
        }

        [Fact]
        public void interfaceDeclarationStmtTest()
        {
            var inputString = new InputFile(@"..\..\..\testFiles\interfaceDeclarationTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> enum");
        }
    }
}
