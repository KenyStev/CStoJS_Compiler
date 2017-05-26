using Compiler.TreeNodes.Expressions;

namespace Compiler.TreeNodes.Statements
{
    public class AssignNode : StatementNode
    {
        private IdNode currentId;
        private LiteralIntNode literalIntNode;

        public AssignNode(IdNode currentId, LiteralIntNode literalIntNode)
        {
            this.currentId = currentId;
            this.literalIntNode = literalIntNode;
        }
    }
}