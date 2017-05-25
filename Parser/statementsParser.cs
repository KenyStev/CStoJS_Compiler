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
            if (pass(typesOptions, new TokenType[] { TokenType.RW_VAR },
            embededOptions(), unaryOperatorOptions,unaryExpressionOptions,literalOptions))
            {
                statement_list();
            }
            else
            {
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

            while (pass(typesOptions,selectionsOptionsStatements,maybeEmptyBlockOptions,jumpsOptionsStatements,
            iteratorsOptionsStatements,new TokenType[]{TokenType.RW_VAR}))
            {
                addLookAhead(lexer.GetNextToken());
                if (look_ahead[look_ahead.Count() - 1].type == TokenType.PUNT_ACCESOR)
                {
                    addLookAhead(lexer.GetNextToken());
                }
                else
                    break;
            }
            int index;
            int index2 = 0;
            Token placeholder = token;
            if (pass(typesOptions,new TokenType[]{TokenType.RW_VAR}))
            {
                index = look_ahead.Count() - 1;
                placeholder = look_ahead[index];
                addLookAhead(lexer.GetNextToken());
                index2 = look_ahead.Count() - 1;
                // DebugInfoMethod("PH: " + placeholder.type + " " + look_ahead[index2].type);
            }
            if (
                (pass(typesOptions,new TokenType[]{TokenType.RW_VAR}) &&
                (placeholder.type == TokenType.ID
                || placeholder.type == TokenType.OP_LESS_THAN
                ||
                (placeholder.type == TokenType.PUNT_SQUARE_BRACKET_OPEN
                && (look_ahead[index2].type == TokenType.PUNT_SQUARE_BRACKET_CLOSE
                || look_ahead[index2].type == TokenType.PUNT_COMMA))))
                )
            {
                local_variable_declaration();
                if (!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
            }else if(pass(embededOptions()))
            {
                embedded_statement();
            }else{
                throwError("statement expected");
            }

            /*addLookAhead(lexer.GetNextToken()); 
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
            }*/
        }

        /*local-variable-declaration:
            | type-or-var variable-declarator-list */
        private void local_variable_declaration()
        {
            printIfDebug("local_variable_declaration");
            if (!pass(typesOptions, new TokenType[] { TokenType.RW_VAR }))
                throwError("type-or-var expected");
            type_or_var();
            variable_declarator_list();
        }
    }
}