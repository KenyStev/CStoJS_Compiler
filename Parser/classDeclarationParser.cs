using System;

namespace Compiler
{
    public partial class Parser
    {
        /*class-declaration: ;
	        | class-modifier "class" identifier inheritance-base class-body optional-body-end */
        private void class_declaration()
        {
            printIfDebug("class_declaration");
            if(pass(TokenType.RW_ABSTRACT))
                consumeToken();
            if(!pass(TokenType.RW_CLASS))
                throwError("group-declaration 'class' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            if(pass(TokenType.PUNT_COLON))
                inheritance_base();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            class_body();
            if(pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                optional_body_end();
        }

        private void class_body()
        {
            printIfDebug("class_body");
            throw new NotImplementedException();
        }

        private void inheritance_base()
        {
            printIfDebug("inheritance_base");
            throw new NotImplementedException();
        }
    }
}