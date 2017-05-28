
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Compiler;
using Compiler.TreeNodes;

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
            var TestingFile = @"memberClass";
            // var inputString = new InputFile(@"..\Parser.Tests\testFiles\compiiiss1.txt");
            // var inputString = new InputFile(@"..\Parser.Tests\testFiles\generationTree\using_namespace_enum.txt");
            var inputString = new InputFile(dir+TestingFile+".txt");
            var tokenGenerators = Resources.getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);
            var parser = new Parser(lexer);
            try{
                var code = parser.parse();

                // Insert code to set properties and fields of the object.  
                XmlSerializer mySerializer = new XmlSerializer(typeof(CompilationUnitNode));  
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
    }
}
