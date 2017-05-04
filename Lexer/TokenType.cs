namespace Compiler
{
    public enum TokenType
    {
        ID,
        EOF,
        OP_SUM,
        OP_SUBSTRACT,
        OP_DIVISION,
        OP_MULTIPLICATION,
        OP_MODULO,
        OP_ASSIGN,
        LIT_INT,
        PAREN_OPEN,
        PAREN_CLOSE,
        END_STATEMENT,
        RW_INT,
        RW_STRING,
        RW_TRUE,
        RW_FALSE,
        RW_FLOAT,
        RW_CHAR,
        RW_BOOL
    }
}