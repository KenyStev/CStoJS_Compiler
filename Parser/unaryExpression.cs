using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        /*unary-expression:
            | expression-unary-operator unary-expression
            | '(' type ')' primary-expression
            | primary-expression */
        private void unary_expression()
        {
            TokenType[] nuevo = { TokenType.RW_NEW , TokenType.ID,
                TokenType.PUNT_PAREN_OPEN, TokenType.RW_THIS
            };
            printIfDebug("unary_expression");
            if(pass(unaryOperatorOptions))
            {
                consumeToken();
                unary_expression();
            }else if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                addLookAhead(lexer.GetNextToken());
                addLookAhead(lexer.GetNextToken());
                if (typesOptions.Contains(look_ahead[0].type) && look_ahead[1].type == TokenType.PUNT_PAREN_CLOSE)
                {
                    consumeToken();
                    if (!pass(typesOptions))
                        throwError("type expected");
                    consumeToken();

                    if (!pass(TokenType.PUNT_PAREN_CLOSE))
                        throwError("')' expected");
                    consumeToken();
                    primary_expression();
                }
                else
                {
                    primary_expression();
                }
            }else if(pass(nuevo,literalOptions))
            {
                primary_expression();
            }else{
                throwError("unary-operator, casting or primary-expression expected");
            }
        }

        /*primary-expression:
            | "new" instance-expression primary-expression-p
            | literal primary-expression-p
            | identifier primary-expression-p
            | '(' expression ')' primary-expression-p
            | "this" primary-expression-p */
        private void primary_expression()
        {
            printIfDebug("primary_expression");
            if(pass(TokenType.RW_NEW))
            {
                consumeToken();
                instance_expression();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(literalOptions))
            {
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(TokenType.ID))
            {
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                expression();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError(") expected");
                consumeToken();
            }else if(pass(TokenType.RW_THIS))
            {
                consumeToken();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else{
                throwError("new, literal, identifier, '(' or \"this\" expected");
            }
        }

        /*primary-expression-p:
            | '.' identifier optional-funct-or-array-call primary-expression-p
            | increment-decrement primary-expression-p 
            | EPSILON  */
        private void primary_expression_p()
        {
            printIfDebug("primary_expression_p");
            if(pass(TokenType.PUNT_ACCESOR))
            {
                consumeToken();
                if(!pass(TokenType.ID))
                    throwError("identifier expected");
                consumeToken();
                optional_funct_or_array_call();
                if(pass(primaryOptionsPrime))
                    primary_expression_p();
            }else if(pass(TokenType.OP_PLUS_PLUS,TokenType.OP_MINUS_MINUS))
            {
                consumeToken();
                primary_expression_p();
            }else{
                //EPSILON
            }
        }

        /*optional-funct-or-array-call:
            | '(' argument-list ')'
            | optional-array-access-list 
            | EPSILON */
        private void optional_funct_or_array_call()
        {
            printIfDebug("optional_funct_or_array_call");
            if(pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                argument_list();
                if(!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError("] expected");
                consumeToken();
            }else if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                optional_array_access_list();
            }else{
                //EPSILON
            }
        }

        /*optional-array-access-list:
            | '[' expression-list ']' optional-array-access-list
            | EPSILON */
        private void optional_array_access_list()
        {
            printIfDebug("optional_array_access_list");
            if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                consumeToken();
                expression_list();
                if(!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                    throwError("] expected");
                consumeToken();
                optional_array_access_list();
            }else{
                //EPSILON
            }
        }

        /*expression-list:
	        | expression optional-expression-list */
        private void expression_list()
        {
            printIfDebug("expression_list");
            expression();
            optional_expression_list();
        }

        /*optional-expression-list:
            | expression-list
            | EPSILON */
        private void optional_expression_list()
        {
            printIfDebug("optional_expression_list");
            if(pass(expressionOptions()))
            {
                expression_list();
            }else{
                //EPSILON
            }
        }

        /*instance-expression:
	        | type instance-expression-factorized */
        private void instance_expression()
        {
            printIfDebug("instance_expression");
            if(!pass(typesOptions))
                throwError("type expected");
            consumeToken();
            instance_expression_factorized();
        }

        /*instance-expression-factorized:
            | '[' instance-expression-factorized-p 
            | '(' argument-list ')' */
        private void instance_expression_factorized()
        {
            printIfDebug("instance_expression_factorized");
            if (pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                consumeToken();
                instance_expression_factorized_p();
            }else if (pass(TokenType.PUNT_PAREN_OPEN))
            {
                consumeToken();
                argument_list();

                if (!pass(TokenType.PUNT_PAREN_CLOSE))
                    throwError("')' expected");
                consumeToken();
            }
            else
            {
                throwError("'[' or '(' expected");
            }
        }

        /*instance-expression-factorized-p:
            | expression-list ']' optional-rank-specifier-list optional-array-initializer
            | rank-specifier-list optional-array-initializer */
        private void instance_expression_factorized_p()
        {
            printIfDebug("instance_expression_factorized_p");
            if(pass(expressionOptions()))
            {
                expression_list();
                if (!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                    throwError("']' expected");
                consumeToken();

                optional_rank_specifier_list();
                optional_array_initializer();
            }else if (pass(TokenType.PUNT_COMMA))
            {
                rank_specifier_list();
                optional_array_initializer();
            }
            else
            {
                throwError("expression or rank specifier ','");
            }
        }

        /*rank-specifier-list: 
	        | rank-specifier optional-rank-specifier-list */
        private void rank_specifier_list()
        {
            printIfDebug("rank_specifier_list");
            rank_specifier();
            optional_rank_specifier_list();
        }

        /*rank-specifier:
	        | optional-comma-list ']' */
        private void rank_specifier()
        {
            printIfDebug("rank_specifier");
            optional_comma_list();
            if(!pass(TokenType.PUNT_SQUARE_BRACKET_CLOSE))
                throwError("] expected");
            consumeToken();
        }

        /*optional-comma-list:
            | ',' optional-comma-list
            | EPSILON */
        private void optional_comma_list()
        {
            if(pass(TokenType.PUNT_COMMA))
            {
                consumeToken();
                optional_comma_list();
            }else{
                //EPSILON
            }
        }

        /*optional-array-initializer:
            | array-initializer
            | EPSILON */
        private void optional_array_initializer()
        {
            printIfDebug("optional_array_initializer");
            if(pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
            {
                array_initializer();
            }else{
                //EPSILON
            }
        }

        /*optional-rank-specifier-list:
            | '[' rank-specifier-list
            | EPSILON 	 */
        private void optional_rank_specifier_list()
        {
            printIfDebug("optional_rank_specifier_list");
            if(pass(TokenType.PUNT_SQUARE_BRACKET_OPEN))
            {
                consumeToken();
                rank_specifier_list();
            }else{
                //EPSILON
            }
        }
    }
}