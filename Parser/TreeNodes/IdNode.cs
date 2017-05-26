using System.Collections.Generic;

namespace Compiler.TreeNodes
{
    public class IdNode
    {
        private string id;
        private List<IdNode> attributes;

        public IdNode(string idValue)
        {
            this.id = idValue;
            this.attributes = new List<IdNode>();
        }

        public IdNode(string id, List<IdNode> attr)
        {
            this.id = id;
            this.attributes = attr;
        }
    }
}