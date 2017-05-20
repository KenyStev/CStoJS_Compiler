using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        TokenType [] encapsulationTypes = {TokenType.RW_PUBLIC, TokenType.RW_PRIVATE, TokenType.RW_PROTECTED};
        TokenType [] typesdeclarationOptions = {TokenType.RW_ABSTRACT, TokenType.RW_CLASS, TokenType.RW_INTERFACE,TokenType.RW_ENUM};
        TokenType [] optionalModifierTypes = {TokenType.RW_ABSTRACT,TokenType.RW_STATIC,TokenType.RW_OVERRIDE,TokenType.RW_VIRTUAL};
        TokenType [] typesOptions = {TokenType.RW_INT,TokenType.RW_CHAR,TokenType.RW_STRING,TokenType.RW_BOOL,TokenType.RW_FLOAT,TokenType.ID};
        TokenType [] expressionOptions = {TokenType.LIT_INT};
        private bool pass(params TokenType [] types)
        {
            return types.Contains(token.type);
        }

        private bool pass(params TokenType [][] types)
        {
            foreach(var type in types)
                if(type.Contains(token.type))
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