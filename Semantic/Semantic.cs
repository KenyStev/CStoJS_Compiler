using System;
using System.Collections.Generic;
using Compiler.TreeNodes;

namespace Compiler
{
    public class Semantic
    {
        private List<string> paths;

        public Semantic(List<string> paths)
        {
            this.paths = paths;
        }

        public List<CompilationUnitNode> evaluate()
        {
            List<CompilationUnitNode> trees = new List<CompilationUnitNode>();
            string currentFile = "";
            try{
                foreach (var csFile in paths)
                {
                    currentFile = csFile;
                    var lexer = new Lexer(new InputFile(csFile),Resources.getTokenGenerators());
                    var parser = new Parser(lexer);
                    trees.Add(parser.parse());
                }
            }catch(LexicalException ex){
                throw new LexicalException(currentFile + ": "+ex.Message);
            }catch(SyntaxTokenExpectedException ex){
                throw new SyntaxTokenExpectedException(currentFile + ": "+ex.Message);
            }catch(SemanticException ex){
                throw new SemanticException(currentFile + ": "+ex.Message);
            }
            return trees;
        }
    }
}
