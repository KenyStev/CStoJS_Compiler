using System;
using System.Collections.Generic;
using Compiler;
using Compiler.TreeNodes;

namespace Compiler
{
    public class Semantic
    {
        private List<string> paths;
        private API api;
        private Dictionary<string, CompilationUnitNode> trees;

        public Semantic(List<string> paths)
        {
            this.paths = paths;
            this.trees = new Dictionary<string,CompilationUnitNode>();

            string currentFile = "";
            try{
                foreach(var csFile in paths)
                {
                    currentFile = csFile;
                    var lexer = new Lexer(new InputFile(csFile),Resources.getTokenGenerators());
                    var parser = new Parser(lexer);
                    // trees.Add(parser.parse());
                    trees[csFile] = parser.parse();
                    trees[csFile].setOriginFile(csFile);
                }
            }catch(LexicalException ex){
                throw new LexicalException(currentFile + ": "+ex.Message);
            }catch(SyntaxTokenExpectedException ex){
                throw new SyntaxTokenExpectedException(currentFile + ": "+ex.Message);
            }catch(SemanticException ex){
                throw new SemanticException(currentFile + ": "+ex.Message);
            }
            this.api = new API(trees);
            setUsingsParentForAllNamespaces();
        }

        private void setUsingsParentForAllNamespaces()
        {
            foreach (var tree in trees)
            {
                foreach (var ns in tree.Value.namespaceDeclared)
                {
                    if(ns.parentNamespace == null)
                    {
                        ns.addParentUsings(tree.Value.usingDirectives);
                    }else{
                        ns.addParentUsings(ns.parentNamespace.usingDirectives);
                    }
                }
            }
        }

        public Dictionary<string,CompilationUnitNode> evaluate()
        {
            foreach (var tree in trees)
            {
                api.setNamespaces(tree);
                api.setTypes(tree);
            }
            printNamespaceTable();
            printTypesTable();
            
            

            // string currentFile = "";
            // try{
            //     foreach(var csFile in paths)
            //     {
            //         currentFile = csFile;
            //         var lexer = new Lexer(new InputFile(csFile),Resources.getTokenGenerators());
            //         var parser = new Parser(lexer);
            //         // trees.Add(parser.parse());
            //         trees[csFile] = parser.parse();
            //     }
            // }catch(LexicalException ex){
            //     throw new LexicalException(currentFile + ": "+ex.Message);
            // }catch(SyntaxTokenExpectedException ex){
            //     throw new SyntaxTokenExpectedException(currentFile + ": "+ex.Message);
            // }catch(SemanticException ex){
            //     throw new SemanticException(currentFile + ": "+ex.Message);
            // }
            return trees;
        }

        private void printTypesTable()
        {
            Debug.print("Table of Types");
            Debug.print("--------------------------------------");
            foreach (var entry in Singleton.typesTable)
            {
                Debug.print(entry.Key + " | " + entry.Value.GetType());
            }
            Debug.print("--------------------------------------");
        }

        private void printNamespaceTable()
        {
            Debug.print("Table of Namespaces");
            Debug.print("--------------------------------------");
            foreach (var ns in Singleton.namespacesTable)
            {
                Debug.print(ns.Key + " | " + ns.Value.GetType());
            }
            Debug.print("--------------------------------------");
        }
    }
}
