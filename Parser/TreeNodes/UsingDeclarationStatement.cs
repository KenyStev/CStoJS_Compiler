namespace Compiler.TreeNodes
{
    public class UsingDeclarationStatement : DeclarationStement
    {
        private IdNode idNode;

        public UsingDeclarationStatement(IdNode val)
        {
            this.idNode = val;
        }
    }
}