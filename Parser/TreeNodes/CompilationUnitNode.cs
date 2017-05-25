using System;
using System.Collections.Generic;

namespace Compiler.TreeNodes
{
    public class CompilationUnitNode
    {
        private List<UsingDeclarationStatement> usingDirectives;

        internal void setUsings(List<UsingDeclarationStatement> usingList)
        {
            this.usingDirectives = usingList;
        }
    }
}