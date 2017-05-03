namespace Compiler
{
    public enum TokenType
    {
        ID,
        EOF,
        OP_SUM,
        PRINT_CALL,
        READ_CALL,
        OP_SUBSTRACT,
        OP_DIVISION,
        OP_MULTIPLICATION,
        OP_MODULO,
        OP_ASSIGN,
        LIT_INT,
        PAREN_OPEN,
        PAREN_CLOSE,
        END_STATEMENT
    }
}