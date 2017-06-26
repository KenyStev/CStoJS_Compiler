using System;
using System.Collections.Generic;
using System.IO;
using Compiler;
using Compiler.SemanticAPI;
using Compiler.TreeNodes;
using Compiler.Writer;

namespace Compiler.CodeGenerator
{
    public class CodeGenerator
    {
        private API api;
        Writer.Writer writer;
        private List<string> paths;
        private Dictionary<string, CompilationUnitNode> trees;

        public CodeGenerator(List<string> paths)
        {
            this.paths = paths;
            this.trees = new Dictionary<string,CompilationUnitNode>();
            var lexer = new Lexer(new InputString(Utils.txtIncludes),Resources.getTokenGenerators());
            var parser = new Parser(lexer);
            // trees.Add(parser.parse());
            trees["IncludesDefault"] = parser.parse();
            trees["IncludesDefault"].setOriginFile("IncludesDefault");


            string currentFile = "";

            var semantic = new Semantic(paths);
            try{
                var trees = semantic.evaluate();
                System.Console.Out.WriteLine("Success!");
            }catch(LexicalException ex){
                System.Console.Out.WriteLine(ex.GetType().Name + " -> " + ex.Message);
            }catch(SyntaxTokenExpectedException ex){
                System.Console.Out.WriteLine(ex.GetType().Name + " -> " + ex.Message);
            }catch(SemanticException ex){
                System.Console.Out.WriteLine(ex.GetType().Name + " -> " + ex.Message);
            }
            this.api = new API(trees);
            // var _path = @"C:\Users\jobar\Documents\git\CStoJS_Compiler\GeneratedJs\generated.js";
            var _path = @"..\GeneratedJs\generated.js";
            writer =  new Writer.Writer(_path);
        }

        public static void ProcessDirectory(ref List<string> paths, string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                    paths.Add(Path.GetFullPath(fileName));
            }
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(ref paths, subdirectory);
        }

        public void GenerateCode()
        {
            api.initContext();
            string currentFile = "";
            //this.writer.WriteString(Utils.JsSystem);

            foreach(var nsp in Singleton.namespacesTable) {
                this.writer.WriteString($"GeneratedNamespace.{nsp.Key} = {{}}; \n");
            }

            try{
                foreach (var tree in api.trees)
                {
                    currentFile = tree.Value.origin;
                    if(currentFile!="IncludesDefault")
                    {
                        tree.Value.defaultNamespace.GenerateCode(writer, api);
                        foreach (var ns in tree.Value.namespaceDeclared)
                        {
                            ns.GenerateCode(writer, api);
                        }
                    }
                }
            }catch(SemanticException ex){
                throw new SemanticException(currentFile + ": "+ex.Message);
            }
            writer.Finish();
        }
    }
}