using System;

namespace Compiler
{
    public partial class Parser
    {
        TokenType [] encapsulationTypes = {TokenType.RW_PUBLIC, TokenType.RW_PRIVATE, TokenType.RW_PROTECTED};
        TokenType [] typesdeclarationOptions = {TokenType.RW_ABSTRACT, TokenType.RW_CLASS, TokenType.RW_INTERFACE,TokenType.RW_ENUM};
        private bool pass(params TokenType [] types)
        {
            foreach(var type in types)
                if(token.type == type)
                    return true;
            return false;
        }

        private void throwError(string msj)
        {
            throw new SyntaxTokenExpectedException(msj+" before: "+token.lexeme+"'. line:"+token.row+","+token.column);
        }

        private void consumeToken()
        {
            token = lexer.GetNextToken();
        }
    }
}