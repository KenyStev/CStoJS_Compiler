using System;

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
            if(pass(typesOptions,new TokenType[]{TokenType.RW_VAR},maybeEmptyBlockOptions,selectionsOptionsStatements
            ,iteratorsOptionsStatements,jumpsOptionsStatements,StatementsOptions))
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
            if(pass(typesOptions,new TokenType[]{TokenType.RW_VAR}))
            {
                local_variable_declaration();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
            }else if(pass(maybeEmptyBlockOptions,selectionsOptionsStatements,iteratorsOptionsStatements,jumpsOptionsStatements,StatementsOptions))
            {
                embedded_statement();
            }else{
                throwError("statement expected");
            }
        }

        /*embedded-statement:
            | maybe-empty-block
            | statement-expression ';'
            | selection-statement
            | iteration-statement
            | jump-statement ';' */
        private void embedded_statement()
        {
            printIfDebug("embedded_statement");
            throw new NotImplementedException();
        }

        /*local-variable-declaration:
	        | type-or-var variable-declarator-list */
        private void local_variable_declaration()
        {
            printIfDebug("local_variable_declaration");
            if(!pass(typesOptions,new TokenType[]{TokenType.RW_VAR}))
                throwError("type-or-var expected");
            consumeToken();
            variable_declarator_list();
        }
    }
}