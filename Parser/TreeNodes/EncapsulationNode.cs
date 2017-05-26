namespace Compiler.TreeNodes
{
    public class EncapsulationNode
    {
        private TokenType encapsulationType;

        public EncapsulationNode(TokenType type)
        {
            this.encapsulationType = type;
        }
    }
}