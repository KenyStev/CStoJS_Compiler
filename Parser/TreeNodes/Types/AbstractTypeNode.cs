namespace Compiler.TreeNodes.Types
{
    public class AbstractTypeNode : TypeNode
    {
        private IdNode typeName;

        public AbstractTypeNode(IdNode typeName)
        {
            this.typeName = typeName;
        }
    }
}