using System;
using Compiler;
using Xunit;

namespace Parser.Tests
{
    public class InvalidExpressionesTests
    {
        [Fact]
        public void TestWrongInlineExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int prueba = (!(isVisible && iden) || noEsta);
    int LUNES = ((5 + 3) + (4  3));
    int MARTES = ((5 * 9 / 3) - 7 + (2 * 7 + 4) / ( (128 >> 5 * 5) - (1 << 7 * 46) / 3 )) + 15;
    int MIERCOLES = 0;
    int x = y = z = w = MARTES / 2;
    Kevin kevin = new persona();
}");
            printHeader("TestWrongInlineExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestWrongInlineExpression");
        }

        [Fact]
        public void TestWrongLogicalExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = (!(isVisible && isHere) ||);
    bool boleano = isNull ?? true;
}");
            printHeader("TestWrongLogicalExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestWrongLogicalExpression");
        }

        [Fact]
        public void TestClassWWrongTernaryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = x==0? y=(5+3):z/2;
    bool boleano = y>5? k--: (k>4? y++k++);
}");
            printHeader("TestClassWWrongTernaryOperatorsInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWWrongTernaryOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongNullCoalescingOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = x ?? ( y ?? (t??5) );
    bool boleano = j ?? );
}");
            printHeader("TestClassWrongNullCoalescingOperatorsInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongNullCoalescingOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongBinaryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  1 << 5;
    bool boleano = 5;
    int otro = 10; 
    float f = (1 | (0 &) (100^100));
}");
            printHeader("TestClassWrongBinaryOperatorsInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongBinaryOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongEqualityOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 == 5);
    bool boleano = (x!=5);
    int otro = 10 != ( ==mama); 
    float f = (mama == (0!=5));
}");
            printHeader("TestClassWrongEqualityOperatorsInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongEqualityOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongShiftOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 << 5);
    bool boleano = (x>>5);
    int otro = 10 != (otro>>); 
    float f = (mama <= (0<<5));
}");
            printHeader("TestClassWrongShiftOperatorsInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongShiftOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongAdittiveOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 + 5);
    bool boleano = (x-5+ +);
    int otro = 10 + (otro-mama); 
    float f = (mama + (0-5));
}");
            printHeader("expressioTestClassWrongAdittiveOperatorsInExpressionnsTest");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongAdittiveOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongMultiplicativeOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES =  (1 * 5);
    bool boleano = (x/5);
    int otro = 10 * (otro%mama) /; 
    float f = (mama / (0*5));
}");
            printHeader("expreTestClassWrongMultiplicativeOperatorsInExpressionssionsTest");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongMultiplicativeOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongUnaryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int LUNES = (x += 5);
    bool boleano = (y >>= 5);
    int otro = ((x++));
    int nuevo = + ++y;
    float f = (~nada!);
    bool c = (~(nada && isVisible) || !isVisible);
    int t = (int)nada;
    float mana = (float)(n.atributo);
}");
            printHeader("TestClassWrongUnaryOperatorsInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> exprTestClassWrongUnaryOperatorsInExpressionessions");
        }

        [Fact]
        public void TestClassWrongAccesMemoryOperatorsInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    float mana = (float)(n.atributo);
    public int funcion = persona.methodo(x=3).metodo;
    float f = persona.tryParse(x);
    Persona persona = this.atributo;
    Persona persona = this.method(x,y,r,,nada);
}");
            printHeader("TestClassWrongAccesMemoryOperatorsInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongAccesMemoryOperatorsInExpression");
        }

        [Fact]
        public void TestClassWrongArraysInExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    float mana = new int[2][][][];
    private int arreglo =  new float[]{ 5,3,5}; 
    int arreglo = new int[,,];
    int arreglo = new int[2][]{ new int[5],new int[8]};
    int arreglo = { new int[5], new int[4], array };
    int value = new Persona(x,y,w).array[2];
    int[,,,] x = new int[5,5,3,4];
}");
            printHeader("TestClassWrongArraysInExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestClassWrongArraysInExpression");
        }

        [Fact]
        public void TestWrongAsExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int x = j as !;
}");
            printHeader("TestWrongAsExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestWrongAsExpression");
        }

        [Fact]
        public void TestWrongIsExpression()
        {
            var inputString = new InputString(@"public class kevin : Javier
{
    int x = is persona carlos;
}");
            printHeader("TestWrongIsExpression");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
            Console.Out.WriteLine("Parsed Successfully -> TestWrongIsExpression");
        }

        private void printHeader(string v)
        {
            Console.Out.WriteLine("----------------------------------------");
            Console.Out.WriteLine(v);
            Console.Out.WriteLine("----------------------------------------");
        }
    }
}