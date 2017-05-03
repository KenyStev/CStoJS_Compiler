using System;
using Compiler;
using Xunit;

namespace Lexer.Tests
{
    public class TokenTypeTest
    {
        [Fact]
        public void ValidStartID()
        {
            var inputString = new InputString("abc");
            var idTokenGenerator = new IdTokenGenerator();
            var currentSymbol = inputString.GetNextSymbol();
            do{
                Assert.True(idTokenGenerator.validStart(currentSymbol));
                currentSymbol = inputString.GetNextSymbol();
            }while(currentSymbol.character != '\0');
        }
    }
}
