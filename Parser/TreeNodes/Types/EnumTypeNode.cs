using System.Collections.Generic;
using Compiler.TreeNodes.Statements;

namespace Compiler.TreeNodes.Types
{
    public class EnumTypeNode : TypeNode
    {
        private List<EnumNode> enumerableList;
        private IdNode idnode;

        public EnumTypeNode(IdNode idnode, List<EnumNode> enumerableList)
        {
            this.idnode = idnode;
            this.enumerableList = enumerableList;
        }
    }
}