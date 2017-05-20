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
            printHeader("usingStmtTest");
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
            printHeader("namespacesStmtTest");
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
            printHeader("classDeclarationStmtTest");
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
            printHeader("enumDeclarationStmtTest");
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
            printHeader("interfaceDeclarationStmtTest");
            var inputString = new InputFile(@"..\..\..\testFiles\interfaceDeclarationTests.txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            parser.parse();
            Console.Out.WriteLine("Parsed Successfully -> enum");
        }

        private void printHeader(string v)
        {
            Console.Out.WriteLine("----------------------------------------");
            Console.Out.WriteLine(v);
            Console.Out.WriteLine("----------------------------------------");
        }
    }
}
