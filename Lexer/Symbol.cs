namespace Compiler
{
    public class Symbol
    {
        public readonly int colCount;
        public readonly int rowCount;
        public readonly char character;

        public Symbol(char character, int rowCount, int colCount)
        {
            this.character = character;
            this.rowCount = rowCount;
            this.colCount = colCount;
        }
    }
}