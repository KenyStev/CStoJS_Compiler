namespace Compiler.TreeNodes.Expressions
{
    public abstract class ExpressionNode : VariableInitializerNode
    {
        public ExpressionNode(){}
        public ExpressionNode(Token token) : base(token){}
    }
}