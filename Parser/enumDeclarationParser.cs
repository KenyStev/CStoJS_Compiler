using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        /*enum-declaration: 
	        | "enum" identifier enum-body optional-body-end */
        private void enum_declaration()
        {
            printIfDebug("enum_declaration");
            if(!pass(TokenType.RW_ENUM))
                throwError("'enum' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            enum_body();
            optional_body_end();
        }

        /*enum-body:
	        | '{' optional-assignable-identifiers-list '}' */
        private void enum_body()
        {
            printIfDebug("enum_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            optional_assignable_identifiers_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }

        /*optional-assignable-identifiers-list:
            | identifier assignment-options
            | EPSILON */
        private void optional_assignable_identifiers_list()
        {
            printIfDebug("optional_assignable_identifiers_list");
            if(pass(TokenType.ID))
            {
                consumeToken();
                assignment_options();
            }else{
                //EPSILON
            }
        }

        /*assignment-options:
            | optional-assignable-identifiers-list-p
            | '=' expression optional-assignable-identifiers-list-p
            | EPSILON */
        private void assignment_options()
        {
            printIfDebug("assignment_options");
            if(pass(TokenType.PUNT_COMMA))
            {
                optional_assignable_identifiers_list_p();
            }else if(pass(TokenType.OP_ASSIGN))
            {
                consumeToken();
                expression();
                optional_assignable_identifiers_list_p();
            }else{
                //EPSILON
            }
        }

        /*optional-assignable-identifiers-list-p:
            | ',' optional-assignable-identifiers-list
            | EPSILON */
        private void optional_assignable_identifiers_list_p()
        {
            printIfDebug("optional_assignable_identifiers_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                optional_assignable_identifiers_list();
            }else{
                //EPSILON
            }
        }
    }
}