
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
using Compiler.TreeNodes.Expressions.EqualityExpressions;
using Compiler.TreeNodes.Expressions.RelationalExpressions;
using Compiler.TreeNodes.Expressions.TypeTestingExpressions;
using Compiler.TreeNodes.Expressions.ShiftExpressions;
using Compiler.TreeNodes.Expressions.AdditiveExpressions;
using Compiler.TreeNodes.Expressions.MultipicativeExpressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions.Literals;
using Compiler.TreeNodes.Expressions.UnaryExpressions.ReferenceAccsess;
using Compiler.TreeNodes.Expressions.UnaryExpressions.InstanceExpressions;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            int projectCounter = 0;
            // string[] argumentos = { @"..\Semantic.Tests\testFiles\"};
            string[] argumentos = { @"..\Semantic.Tests\GeneratorTest\MergeProgram\",
                                    @"..\Semantic.Tests\GeneratorTest\RaimProgram\"};
            // string[] argumentos = { @"..\Semantic.Tests\GeneratorTest\RaimProgram\"};
            while(projectCounter<argumentos.Length)
            {
                Singleton.namespacesTable.Clear();
                Singleton.typesTable.Clear();

                string path = "./";
                if (argumentos.Length > 0)
                {
                    path = argumentos[projectCounter];
                }
                path = Path.GetDirectoryName(path);
                List<string> paths = new List<string>();
                if (Directory.Exists(path))
                {
                    ProcessDirectory(ref paths, path);
                }

                try{
                    var semantic = new Semantic(paths);
                    var trees = semantic.evaluate();
                    System.Console.Out.WriteLine("Success!");
                }catch(LexicalException ex){
                    System.Console.Out.WriteLine(ex.GetType().Name + " -> " + ex.Message);
                }catch(SyntaxTokenExpectedException ex){
                    System.Console.Out.WriteLine(ex.GetType().Name + " -> " + ex.Message);
                }catch(SemanticException ex){
                    System.Console.Out.WriteLine(ex.GetType().Name + " -> " + ex.Message);
                }
                projectCounter++;
            }

            // catch(Exception ex){
            //     System.Console.Out.WriteLine(ex.Message + ": " + ex.StackTrace);
            // }

            // var dir = @"..\Parser.Tests\testFiles\generationTree\";
            // var TestingFile = @"compiiisseada";
            // var inputString = new InputFile(dir+TestingFile+".txt");
            // var tokenGenerators = Resources.getTokenGenerators();

            // var lexer = new Lexer(inputString, tokenGenerators);
            // var parser = new Parser(lexer);
            // try{
            //     var code = parser.parse();
            //     serializeCode(code,dir+@"XMLs\"+TestingFile+".xml");
                
            //     System.Console.Out.WriteLine("Success!");
            // }catch(SyntaxTokenExpectedException ex){
            //     System.Console.Out.WriteLine(ex.Message);
            // }
        }

        public static void ProcessDirectory(ref List<string> paths, string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (isCsFile(fileName))
                    paths.Add(Path.GetFullPath(fileName));
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(ref paths, subdirectory);
        }
        public static bool isCsFile(string path)
        {
            if(Path.GetExtension(path) == ".cs")
            {
                return true;
            }
            return false;
        }

        private static void serializeCode(CompilationUnitNode code,string path)
        {
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new XmlSerializer(typeof(CompilationUnitNode),types());
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter(File.Create(path));
            mySerializer.Serialize(myWriter, code);  
            // myWriter.Close();
        }

        private static Type[] types()
        {
            return new Type[]{
                typeof(ArrayInitializerNode),typeof(BinaryOperatorNode),typeof(ExpressionNode),
                typeof(LiteralIntNode),typeof(VariableInitializerNode),typeof(AssignExpressionNode),
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
                typeof(SwitchSectionNode),typeof(CaseNode),typeof(ForStatementNode),typeof(ForInitializerNode),
                typeof(WhileStatementNode),typeof(DoWhileStatementNode),typeof(ForeachStatementNode),
                typeof(JumpStatementNode),typeof(EqualityExpressionNode),typeof(RelationalExpressionNode),
                typeof(TypeTestingExpressionNode),typeof(ShiftExpressionNode),typeof(AdditiveExpressionNode),
                typeof(MultipicativeExpressionNode),typeof(UnaryExpressionNode),typeof(PrimaryExpressionNode),
                typeof(SumExpressionNode),typeof(SubExpressionNode),typeof(EqualExpressionNode),
                typeof(DistinctExpressionNode),typeof(DivNode),typeof(MultNode),typeof(ModNode),
                typeof(GreaterThanExpressionNode),typeof(GreaterOrEqualThanExpressionNode),typeof(LessThanExpressionNode),
                typeof(LessOrEqualThanExpressionNode),typeof(ShiftLeftNode),typeof(ShiftRightNode),
                typeof(IsTypeTestNode),typeof(AsTypeTestNode),typeof(LiteralBoolNode),typeof(LiteralCharNode),
                typeof(LiteralFloatNode),typeof(LiteralStringNode),typeof(LiteralNode),typeof(PreExpressionNode),
                typeof(CastingExpressionNode),typeof(GroupedExpressionNode),typeof(ThisReferenceAccsessNode),
                typeof(BaseReferenceAccessNode),typeof(PostAdditiveExpressionNode),
                typeof(FunctionCallExpressionNode),typeof(ArrayAccessExpressionNode),typeof(InstanceExpressionNode),
                typeof(ClassInstantioationNode),typeof(ArrayInstantiationNode),typeof(ReferenceAccsessNode),
                typeof(TernaryExpressionNode),typeof(BitwiseAndExpressionNode),typeof(BitwiseOrExpressionNode),
                typeof(ConditionalAndExpressionNode),typeof(ConditionalOrExpressionNode),typeof(ExclusiveOrExpression),
                typeof(NullCoalescingExpressionNode),typeof(InlineExpressionNode),typeof(Token)};
        }
    }
}
