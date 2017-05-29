using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.TreeNodes.Statements;

namespace Compiler
{
    public partial class Parser
    {
        /*optional-statement-list:
            | statement-list
            | EPSILON */
        private List<StatementNode> optional_statement_list()
        {
            printIfDebug("optional_statement_list");
            if (pass(typesOptions, varOption,embededOptions()))
            {
                return statement_list();
            }
            else
            {
                return new List<StatementNode>();
            }
        }

        /*statement-list: 
	        | statement optional-statement-list  */
        private List<StatementNode> statement_list()
        {
            printIfDebug("statement_list");
            var stmt = statement();
            var statements = optional_statement_list();
            statements.Insert(0,stmt);
            return statements;
        }

        /*statement:
            | local-variable-declaration ';'
            | embedded-statement */
        private StatementNode statement()
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
                var localVariable = local_variable_declaration();
                if (!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
                return localVariable;
            }else if(pass(embededOptions()))
            {
                return embedded_statement();
            }else{
                throwError("statement expected");
            }
            return null;
        }

        /*local-variable-declaration:
            | type-or-var variable-declarator-list */
        private LocalVariableDeclarationNode local_variable_declaration()
        {
            printIfDebug("local_variable_declaration");
            if (!pass(typesOptions, varOption))
                throwError("type-or-var expected");
            var type = type_or_var();
            var localVariableList = variable_declarator_list(type,null,false);
            return new LocalVariableDeclarationNode(localVariableList);
        }
    }
}