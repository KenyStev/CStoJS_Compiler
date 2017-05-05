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
            var expectedTypes = new TokenType[]{TokenType.ID, TokenType.ID,TokenType.ID,TokenType.ID,TokenType.ID,TokenType.LIT_BOOL,TokenType.LIT_BOOL};
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
        public void ValidLiteralIntDecimal()
        {
            var inputString = new InputString("7894561230");
            var literalIntTokenGenerator = new LiteralIntTokenGenerator();
            var currentSymbol = inputString.GetNextSymbol();
            do{
                Assert.True(literalIntTokenGenerator.validStart(currentSymbol));
                currentSymbol = inputString.GetNextSymbol();
            }while(currentSymbol.character != '\0');

            inputString = new InputString("123 12 8 0");
            var expectedLexemes = new string[]{"123", "12", "8", "0"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_INT);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_INT);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        }

        [Fact]
        public void ValidLiteralIntHex()
        {
            var inputString = new InputString("0x2bc3a 0X5d6a3");
            var expectedLexemes = new string[]{"0x2bc3a", "0X5d6a3"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_INT);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_INT);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        }

        [Fact]
        public void ValidLiteralIntBinary()
        {
            var inputString = new InputString("0b0100 0B01110");
            var expectedLexemes = new string[]{"0b0100", "0B01110"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_INT);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_INT);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        }

        [Fact]
        public void ValidLiteralFloat()
        {
            var inputString = new InputString("123f 12F 8.58f 0.05F");
            var expectedLexemes = new string[]{"123f", "12F", "8.58f", "0.05F"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_FLOAT);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_FLOAT);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        }

        [Fact]
        public void ValidLiteralBool()
        {
            var inputString = new InputString("true false");
            var expectedLexemes = new string[]{"true", "false"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_BOOL);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_FLOAT);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        }

        [Fact]
        public void ValidLiteralChar()
        {
            var inputString = new InputString("'a' 'b' '\\a' '\\b'");
            var expectedLexemes = new string[]{"'a'", "'b'","'\\a'","'\\b'"};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_CHAR);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_CHAR);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        }

        [Fact]
        public void ValidLiteralString()
        {
            var inputString = new InputString("\"hola 15245\" \"adios dsd sa \\t \\v sta\"");
            var expectedLexemes = new string[]{"\"hola 15245\"", "\"adios dsd sa \\t \\v sta\""};
            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_STRING);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_STRING);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        } 

        [Fact]
        public void ValidLiteralVerbatimString()
        {
            var inputString = new InputString(@"@""Hola como """"estan todos""""   
            nada \mas\ que 'hacer',....."" @""otra ves{](0)""");
            var expectedLexemes = new string[]{@"@""Hola como """"estan todos""""   
            nada \mas\ que 'hacer',.....""",@"@""otra ves{](0)"""};

            // var inputString = new InputString("@\"Hola como \"\"estan todos\"\" nada \\mas\\ que 'hacer',.....\" @\"otra ves{](0)\"");
            // var expectedLexemes = new string[]{"@\"Hola como \"\"estan todos\"\" nada \\mas\\ que 'hacer',.....\""
            // ,"@\"otra ves{](0)\""};

            var lexer = new Compiler.Lexer(inputString,Resources.getTokenGenerators());
            var currentToken = lexer.GetNextToken();
            int i =0;
            do{
                Assert.True(currentToken.type == TokenType.LIT_STRING);
                Assert.True(currentToken.lexeme == expectedLexemes[i++]);
                Console.WriteLine("lexeme: "+currentToken.lexeme+" | TokenType: "+TokenType.LIT_STRING);
                currentToken = lexer.GetNextToken();
            }while(currentToken.type != TokenType.EOF);
        } 
    }
}
