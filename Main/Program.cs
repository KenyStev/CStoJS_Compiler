
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Compiler;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;
using Compiler.TreeNodes.Statements;
using Compiler.TreeNodes.Expressions;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*{var inputString = new InputFile(@"..\Lexer.Tests\TokenTypeTests.cs");
                var inputString = new InputFile(@"..\Parser\unaryExpression.cs");
                var inputString = new InputString(@"
                class MyClass
                {
                    MyClass(Nombre val)
                    {

                    }

                    public me()
                    {

                    }
                }
                ");}
            */
            var dir = @"..\Parser.Tests\testFiles\generationTree\";
            var TestingFile = @"statementsTests";
            // var inputString = new InputFile(@"..\Parser.Tests\testFiles\compiiiss1.txt");
            // var inputString = new InputFile(@"..\Parser.Tests\testFiles\generationTree\using_namespace_enum.txt");
            var inputString = new InputFile(dir+TestingFile+".txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Parser(lexer);
            try{
                var code = parser.parse();

                // Insert code to set properties and fields of the object.  
                XmlSerializer mySerializer = new XmlSerializer(typeof(CompilationUnitNode),types());
                // To write to a file, create a StreamWriter object.  
                StreamWriter myWriter = new StreamWriter(File.Create(dir+@"XMLs\"+TestingFile+".xml"));
                mySerializer.Serialize(myWriter, code);  
                // myWriter.Close();
                
                System.Console.Out.WriteLine("Success!");
            }catch(SyntaxTokenExpectedException ex){
                System.Console.Out.WriteLine(ex.Message);
            }

            /*
            //TRY LEXER
            Token token = lexer.GetNextToken();

            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = lexer.GetNextToken();
            }

            System.Console.Out.WriteLine(token);*/
        }

        private static Type[] types()
        {
            return new Type[]{
                typeof(ArrayInitializerNode),typeof(BinaryOperatorNode),typeof(ExpressionNode),
                typeof(LiteralIntNode),typeof(VariableInitializerNode),typeof(AssignNode),
                typeof(LocalVariableDeclarationNode),typeof(StatementNode),typeof(AbstractTypeNode),
                typeof(ArrayTypeNode),typeof(ClassTypeNode),typeof(EnumTypeNode),typeof(InterfaceTypeNode),
                typeof(MultidimensionArrayTypeNode),typeof(PrimitiveTypeNode),typeof(TypeNode),
                typeof(VarTypeNode),typeof(VoidTypeNode),typeof(ArgumentNode),typeof(CompilationUnitNode),
                typeof(ConstructorInitializerNode),typeof(ConstructorNode),typeof(EncapsulationNode),
                typeof(EnumNode),typeof(FieldNode),typeof(IdNode),typeof(MethodHeaderNode),
                typeof(MethodModifierNode),typeof(MethodNode),typeof(NamespaceNode),
                typeof(ParameterNode),typeof(ReturnTypeNode),typeof(UsingNode),typeof(StatementBlockNode),
                typeof(EmbeddedStatementNode),typeof(ElseStatementNode),typeof(IfStatementNode),
                typeof(SelectionStatementNode),typeof(SwitchStatementNode),typeof(SwitchBodyNode),
                typeof(SwitchSectionNode),typeof(CaseNode)};
        }
    }
}
