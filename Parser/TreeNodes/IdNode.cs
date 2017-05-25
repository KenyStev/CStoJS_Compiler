using System.Collections.Generic;

namespace Compiler.TreeNodes
{
    public class IdNode
    {
        private string id;
        private List<IdNode> attr;

        public IdNode(string idValue)
        {
            this.id = idValue;
            attr = new List<IdNode>();
        }

        public IdNode(string id, List<IdNode> attr)
        {
            this.id = id;
            this.attr = attr;
        }
    }
}