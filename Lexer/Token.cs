namespace Compiler
{
    public class Token
    {
        public TokenType type;
        public int column;
        public int row;
        public string lexeme;
        
        public Token(){}
        public Token(TokenType type, string lexeme, int row, int column)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.row = row;
            this.column = column;
        }

        public override string ToString()
        {
            return lexeme + " of type " + type + " line("+row+","+column+")";
        }
    }
}