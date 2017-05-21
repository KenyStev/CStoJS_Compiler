using System;

namespace Compiler
{
    public partial class Parser
    {
        /*embedded-statement:
            | maybe-empty-block
            | statement-expression ';'
            | selection-statement
            | iteration-statement
            | jump-statement ';' */
        private void embedded_statement()
        {
            printIfDebug("embedded_statement");
            if(!pass(embededOptions()))
                throwError("statement expected");
            if(pass(maybeEmptyBlockOptions))
            {
                maybe_empty_block();
            }else if(pass(StatementsOptions))
            {
                statement_expression();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
            }else if(pass(selectionsOptionsStatements))
            {
                selection_statement();
            }else if(pass(iteratorsOptionsStatements))
            {
                iteration_statement();
            }else if(pass(jumpsOptionsStatements))
            {
                jump_statement();
                if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                    throwError("; expected");
                consumeToken();
            }
        }

        /*maybe-empty-block:
            | '{' optional-statement-list '}'
            | ';' */
        private void maybe_empty_block()
        {
            printIfDebug("maybe_empty_block");
            if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                consumeToken();
                optional_statement_list();
                if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                    throwError("'}' expected");
                consumeToken();
            }else if(pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
            {
                consumeToken();
            }else{
                throwError("'{' or ';' expected");
            }
        }

        /*jump-statement:
            | "break"
            | "continue"
            | "return" optional-expression */
        private void jump_statement()
        {
            printIfDebug("jump_statement");
            if(!pass(jumpsOptionsStatements))
                throwError("break, continue or return expected");
            Token old = token;
            consumeToken();
            if(old.type==TokenType.RW_RETURN)
                optional_expression();
        }

        /*optional-expression: 
            | expression
            | EPSILON */
        private void optional_expression()
        {
            printIfDebug("optional_expression");
            if(pass(expressionOptions()))
            {
                expression();
            }else{
                //EPSILON
            }
        }

        /*iteration-statement:
            | while-statement
            | do-statement
            | for-statement
            | foreach-statement */
        private void iteration_statement()
        {
            printIfDebug("iteration_statement");
            if(pass(TokenType.RW_WHILE))
            {
                while_statement();
            }else if(pass(TokenType.RW_DO))
            {
                do_statement();
            }else if(pass(TokenType.RW_FOR))
            {
                for_statement();
            }else if(pass(TokenType.RW_FOREACH))
            {
                foreach_statement();
            }else{
                throwError("while, do-while, for or foreach conditional statement expected");
            }
        }

        /*while-statement:
	        | "while" '(' expression ')' embedded-statement */
        private void while_statement()
        {
            printIfDebug("while_statement");
            if(!pass(TokenType.RW_WHILE))
                throwError("'while' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            embedded_statement();
        }

        /*do-statement: 
	        | "do" embedded-statement "while" '(' expression ')' ';' */
        private void do_statement()
        {
            printIfDebug("do_statement");
            if(!pass(TokenType.RW_DO))
                throwError("'do' reserved word expected");
            consumeToken();
            embedded_statement();
            if(!pass(TokenType.RW_WHILE))
                throwError("'while' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("; expected");
            consumeToken();
        }

        /*for-statement:
	        | "for" '(' optional-for-initializer ';' optional-expression ';' optional-statement-expression-list ')' embedded-statement */
        private void for_statement()
        {
            printIfDebug("for_statement");
            if(!pass(TokenType.RW_FOR))
                throwError("'for' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            //initializer
            optional_for_initializer();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("';' expected");
            consumeToken();
            //expresion
            optional_expression();
            if(!pass(TokenType.PUNT_END_STATEMENT_SEMICOLON))
                throwError("';' expected");
            consumeToken();
            //statementlist
            optional_statement_expression_list();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            embedded_statement();
        }

        /*optional-for-initializer:
            | local-variable-declaration
            | statement-expression-list
            | EPSILON */
        private void optional_for_initializer()
        {
            printIfDebug("optional_for_initializer");
            if(pass(typesOptions,new TokenType[]{TokenType.RW_VAR}))
            {
                local_variable_declaration();
            }else if(pass(selectionsOptionsStatements))
            {
                statement_expression_list();
            }else{
                //EPSILON
            }
        }

        /*optional-statement-expression-list:
            | statement-expression-list
            | EPSILON */
        private void optional_statement_expression_list()
        {
            printIfDebug("optional_statement_expression_list");
            if(pass(StatementsOptions))
            {
                statement_expression_list();
            }else{
                //EPSILON
            }
        }

        /*statement-expression-list:
	        | statement-expression statement-expression-list-p */
        private void statement_expression_list()
        {
            printIfDebug("statement_expression_list");
            statement_expression();
            statement_expression_list_p();
        }

        /*statement-expression-list-p:
            | ',' statement-expression statement-expression-list-p
            | EPSILON */
        private void statement_expression_list_p()
        {
            printIfDebug("statement_expression_list_p");
            if(pass(TokenType.PUNT_COMMA))
            {
                statement_expression_list_p();
            }else{
                //EPSILON
            }
        }

        /*foreach-statement:
	        | "foreach" '(' type identifier "in" expression ')' embedded-statement */
        private void foreach_statement()
        {
            printIfDebug("foreach_statement");
            if(!pass(TokenType.RW_FOREACH))
                throwError("'foreach' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            if(!pass(typesOptions))
                throwError("type expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier  expected");
            consumeToken();
            if(!pass(TokenType.RW_IN))
                throwError("'in' reserved word expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            embedded_statement();
        }

        /*selection-statement:
            | if-statement
            | switch-statement */
        private void selection_statement()
        {
            printIfDebug("selection_statement");
            if(pass(TokenType.RW_IF))
            {
                if_statement();
            }else if(pass(TokenType.RW_SWITCH))
            {
                switch_statement();
            }else{
                throwError("'if' or 'switch' statement expected");
            }
        }

        /*if-statement:
	        | "if" '(' expression ')' embedded-statement optional-else-part */
        private void if_statement()
        {
            printIfDebug("if_statement");
            if(!pass(TokenType.RW_IF))
                throwError("'if' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            embedded_statement();
            optional_else_part();
        }

        /*optional-else-part:
            | else-part
            | EPSILON */
        private void optional_else_part()
        {
            printIfDebug("optional_else_part");
            if(pass(TokenType.RW_ELSE))
            {
                else_part();
            }else{
                //EPSILON
            }
        }

        /*else-part:
	        | "else" embedded-statement	 */
        private void else_part()
        {
            if(!pass(TokenType.RW_ELSE))
                throwError("'else' reserved word expected");
            consumeToken();
            embedded_statement();
        }

        /*switch-statement:
	        | "switch" '(' expression ')' '{' optional-switch-section-list '}' */
        private void switch_statement()
        {
            printIfDebug("switch_statement");
            if(!pass(TokenType.RW_SWITCH))
                throwError("'switch' reserved word expected");
            consumeToken();
            if(!pass(TokenType.PUNT_PAREN_OPEN))
                throwError("'(' expected");
            consumeToken();
            expression();
            if(!pass(TokenType.PUNT_PAREN_CLOSE))
                throwError("')' expected");
            consumeToken();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            optional_switch_section_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }

        /*optional-switch-section-list:
            | switch-label-list optional-statement-list optional-switch-section-list
            | EPSILON */
        private void optional_switch_section_list()
        {
            printIfDebug("optional_switch_section_list");
            if(pass(switchLabelOptions))
            {
                switch_label_list();
                optional_statement_list();
                optional_switch_section_list();
            }else{
                //EPSILON
            }
        }

        /*switch-label-list:
	        | switch-label switch-label-list-p */
        private void switch_label_list()
        {
            printIfDebug("switch_label_list");
            if(!pass(switchLabelOptions))
                throwError("'case' or 'default' expected");
            switch_label();
            switch_label_list_p();
        }

        /*switch-label-list-p:
            | switch-label-list
            | EPSILON */
        private void switch_label_list_p()
        {
            printIfDebug("switch_label_list_p");
            if(pass(switchLabelOptions))
            {
                switch_label_list();
            }else{
                //EPSILON
            }
        }

        /*switch-label:
            | "case" expression ':'
            | "default" ':' */
        private void switch_label()
        {
            printIfDebug("switch_label");
            if(pass(TokenType.RW_CASE))
            {
                consumeToken();
                expression();
            }else if(pass(TokenType.RW_DEFAULT))
            {
                consumeToken();
            }else{
                throwError("'case' or 'default' expected");
            }
            if(!pass(TokenType.PUNT_COLON))
                throwError("':' expected");
            consumeToken();
        }

        /*statement-expression:
	        | optional-unary-expression primary-expression statement-expression-p */
        private void statement_expression()
        {
            printIfDebug("statement_expression");
            optional_unary_expression();
            primary_expression();
            statement_expression_p();
        }

        /*statement-expression-p:
            | '(' argument-list ')'
            | increment-decrement
            | EPSILON */
        private void statement_expression_p()
        {
            printIfDebug("statement_expression_p");
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                argument_list();
                if(!pass(TokenType.PUNT_PAREN_CLOSE));
                    throwError(") expected");
                consumeToken();
            }else if(pass(TokenType.OP_PLUS_PLUS,TokenType.OP_MINUS_MINUS))
            {
                consumeToken();
            }else{
                //EPSILON
            }
        }

        /*optional-unary-expression:
            | expression-unary-operator unary-expression
            | '(' type ')'
            | EPSILON */
        private void optional_unary_expression()
        {
            printIfDebug("optional_unary_expression");
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                if(!pass(typesOptions))
                    throwError("type expected");
                consumeToken();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError("type expected");
                consumeToken();
            }else if(pass(StatementsOptions))
            {
                if(!pass(unaryOperatorOptions))
                    throwError("unary operator expected");
                unary_expression();
            }else{
                //EPSILON
            }
        }
    }
}