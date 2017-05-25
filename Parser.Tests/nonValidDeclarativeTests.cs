using System;
using Compiler;
using Xunit;

namespace Parser.Tests
{
    public class nonValidDeclarativeTests
    {
        [Fact]
        public void TestUsingExpressionWithoutEndStatement()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada
using jamones;");
            var tokenGenerators = Resources.getTokenGenerators();
            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestUsingExpressionWithoutIdentifier()
        {
            var inputString = new InputString(@"using System;
using
using jamones;");
            var tokenGenerators = Resources.getTokenGenerators();
            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestUsingAfterANamespaceExpression()
        {
            var inputString = new InputString(@"using System;
using Otro;
using jamones;
namespace A{
    using prueba;
    namespace B
    {
        using prueba2.prueba3.prueba4;
    }
    using system;
}");
            var tokenGenerators = Resources.getTokenGenerators();
            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestEnumExpressionWrongDeclaration()
        {
                var inputString = new InputString(@"using System;
    using Compiler.Parser.Nada;
    using jamones;
    public enum DIASDELASEMANA{
        LUNES,MARTES,MIERCOLES,JUEVES,VIERNES,,
    }");
                var tokenGenerators = Resources.getTokenGenerators();
                var lexer = new Lexer(inputString, tokenGenerators);
                var parser = new Compiler.Parser(lexer); 
                
                Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestVariousAssignOperatorsInEnumExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public enum DIASDELASEMANA{
    LUNES=1,MARTES=2,MIERCOLES==3,JUEVES=4,VIERNES=5,
}");
            var tokenGenerators = Resources.getTokenGenerators();
                var lexer = new Lexer(inputString, tokenGenerators);
                var parser = new Compiler.Parser(lexer); 
                
                Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestBadArgumentsListInInterfaceMethodExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public interface DIASDELASEMANA{
    void metodo(int argument, float argument argument);
}");
            var tokenGenerators = Resources.getTokenGenerators();
                var lexer = new Lexer(inputString, tokenGenerators);
                var parser = new Compiler.Parser(lexer); 
                
                Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestVariousCommasInArgumentListInInterfaceMethodExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public interface DIASDELASEMANA{
    void metodo(int argument, float argument,,,int argument);
}");
            var tokenGenerators = Resources.getTokenGenerators();
                var lexer = new Lexer(inputString, tokenGenerators);
                var parser = new Compiler.Parser(lexer); 
                
                Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestInterfaceExpressionWithouteCloseCurlyBracket()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
interface DIASDELASEMANA{
    void metodo(int argument, float argument);
;");
            var tokenGenerators = Resources.getTokenGenerators();
                var lexer = new Lexer(inputString, tokenGenerators);
                var parser = new Compiler.Parser(lexer); 
                
                Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestInterfaceExpressionWrongInheritanceExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
interface DIASDELASEMANA : Padre,, Tia{
    void metodo(int argument, float argument);
};");
            var tokenGenerators = Resources.getTokenGenerators();
            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestClassWithWrongInheritanceExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer, Javier,{
    public kevin();
}
");
            var tokenGenerators = Resources.getTokenGenerators();
            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestClassWithWrongConstructorExpression()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer, Javier{
    public ();
}
");
            var tokenGenerators = Resources.getTokenGenerators();
            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }

        [Fact]
        public void TestClassCantHaveAbstractConstructor()
        {
            var inputString = new InputString(@"using System;
using Compiler.Parser.Nada;
using jamones;
public class kevin : Nexer, Javier{
    public abstract  kevin(){}
}
");
            var tokenGenerators = Resources.getTokenGenerators();
            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Compiler.Parser(lexer); 
            
            Assert.Throws<SyntaxTokenExpectedException>(() => parser.parse());
        }
    }
}