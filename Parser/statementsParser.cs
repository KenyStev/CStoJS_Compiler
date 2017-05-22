using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        /*optional-statement-list:
            | statement-list
            | EPSILON */
        private void optional_statement_list()
        {
            printIfDebug("optional_statement_list");
            if(pass(typesOptions,new TokenType[]{TokenType.RW_VAR},
            maybeEmptyBlockOptions,selectionsOptionsStatements
            ,iteratorsOptionsStatements,jumpsOptionsStatements,unaryOperatorOptions))
            {
                statement_list();
            }else{
                //EPSILON
            }
        }

        /*statement-list: 
	        | statement optional-statement-list  */
        private void statement_list()
        {
            printIfDebug("statement_list");
            statement();
            optional_statement_list();
        }

        /*statement:
            | local-variable-declaration ';'
            | embedded-statement */
        private void statement()
        {
            printIfDebug("statement");
            addLookAhead(lexer.GetNextToken()); 
            addLookAhead(lexer.GetNextToken()); 
            if(pass(typesOptions,new TokenType[]{TokenType.RW_VAR}) 
            && (look_ahead[0].type == TokenType.ID 
            || look_ahead[0].type == TokenType.PUNT_SQUARE_BRACKET_OPEN
            || look_ahead[0].type == TokenType.PUNT_ACCESOR)
            && !literalOptions.Contains(look_ahead[1].type))
            {
                local_variable_declaration();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
            }else if(pass(embededOptions()))
            {
                embedded_statement();
            }else{
                throwError("statement expected");
            }
        }

        /*local-variable-declaration:
	        | type-or-var variable-declarator-list */
        private void local_variable_declaration()
        {
            printIfDebug("local_variable_declaration");
            if(!pass(typesOptions,new TokenType[]{TokenType.RW_VAR}))
                throwError("type-or-var expected");
            type_or_var();
            variable_declarator_list();
        }
    }
}