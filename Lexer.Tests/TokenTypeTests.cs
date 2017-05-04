using System;
using System.Collections.Generic;
using Compiler;
using Xunit;

namespace Lexer.Tests
{
    public class TokenTypeTest
    {
        [Fact]
        public void ValidIDReservedWord()
        {
            var inputString = new InputString("abc");
            var idTokenGenerator = new IDReservedWordTokenGenerator();
            var currentSymbol = inputString.GetNextSymbol();
            do{
                Assert.True(idTokenGenerator.validStart(currentSymbol));
                currentSymbol = inputString.GetNextSymbol();
            }while(currentSymbol.character != '\0');

            inputString = new InputString("hola Hola abd142_ hola_ adios_1542 true false");
            var expectedTypes = new TokenType[]{TokenType.ID, TokenType.ID,TokenType.ID,TokenType.ID,TokenType.ID,TokenType.RW_TRUE,TokenType.RW_FALSE};
            var expectedLexemes = new string[]{"hola", "Hola", "abd142_", "hola_", "adios_1542", "true", "false"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == expectedTypes[i]);
                Assert.True(currentToken.lexeme == expectedLexemes[i]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+expectedTypes[i]);
                currentToken = lexer.GetNextToken();
                i++;
            }while(currentToken.type != TokenType.EOF);
        }
        
        [Fact]
        public void ValidLiteralInt()
        {
            var inputString = new InputString("7894561230");
            var literalIntTokenGenerator = new LiteralIntTokenGenerator();
            var currentSymbol = inputString.GetNextSymbol();
            do{
                Assert.True(literalIntTokenGenerator.validStart(currentSymbol));
                currentSymbol = inputString.GetNextSymbol();
            }while(currentSymbol.character != '\0');

            inputString = new InputString("123 12 0x2bc3a 0b0100 0X5d6a3 0B01110 8 0");
            var expectedLexemes = new string[]{"123", "12", "0x2bc3a", "0b0100", "0X5d6a3", "0B01110", "8", "0"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_INT);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_INT);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        }
    }
}
