namespace Compiler
{
    public class Token
    {
        public TokenType type;
        private int column;
        private int row;
        private string lexema;
        
        public Token(TokenType type, string lexema, int row, int column)
        {
            this.type = type;
            this.lexema = lexema;
            this.row = row;
            this.column = column;
        }

        public override string ToString()
        {
            return lexema + " of type " + type;
        }
    }
}