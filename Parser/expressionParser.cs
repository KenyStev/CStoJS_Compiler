using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        /*expression:
            | conditional-expression */
        private void expression()
        {
            printIfDebug("expression");
            if(!pass(expressionOptions()))
                throwError("Operator, identifier or literal in expression expected");
            conditional_expression();
        }

        /*conditional-expression:
	        | null-coalescing-expression conditional-expression-p */
        private void conditional_expression()
        {
            printIfDebug("conditional_expression");
            null_coalescing_expression();
            if(pass(TokenType.OP_TERNARY))
            {
                conditional_expression_p();
            }
        }

        /*conditional-expression-p:
            | '?' expression ':' expression 
            | EPSILON */
        private void conditional_expression_p()
        {
            printIfDebug("conditional_expression_p");
            if(pass(TokenType.OP_TERNARY))
            {
                consumeToken();
                expression();
                if(!pass(TokenType.PUNT_COLON))
                    throwError(": expected");
                consumeToken();
                expression();
            }else{
                //EPSILON
            }
        }

        /*null-coalescing-expression:
	        | conditional-or-expression null-coalescing-expression-p */
        private void null_coalescing_expression()
        {
            printIfDebug("null_coalescing_expression");
            conditional_or_expression();
            if(pass(TokenType.OP_NULL_COALESCING))
            {
                null_coalescing_expression_p();
            }
        }

        /*null-coalescing-expression-p:
            | "??" null-coalescing-expression
            | EPSILON */
        private void null_coalescing_expression_p()
        {
            printIfDebug("null_coalescing_expression_p");
            if(pass(TokenType.OP_NULL_COALESCING))
            {
                consumeToken();
                null_coalescing_expression();
            }else{
                //EPSILON
            }
        }

        /*conditional-or-expression:
	        | conditional-and-expression conditional-or-expression-p */
        private void conditional_or_expression()
        {
            printIfDebug("conditional_or_expression");
            conditional_and_expression();
            if(pass(TokenType.OP_OR))
            {
                conditional_or_expression_p();
            }
        }

        /*conditional-or-expression-p:
            | "||" conditional-and-expression conditional-or-expression-p 
            | EPSILON */
        private void conditional_or_expression_p()
        {
            printIfDebug("conditional_or_expression_p");
            if(pass(TokenType.OP_OR))
            {
                consumeToken();
                conditional_and_expression();
                if(pass(TokenType.OP_OR))
                    conditional_or_expression_p();
            }else{
                //EPSILON
            }
        }

        /*conditional-and-expression:
	        | inclusive-or-expression conditional-and-expression-p */
        private void conditional_and_expression()
        {
            printIfDebug("conditional_and_expression");
            inclusive_or_expression();
            if(pass(TokenType.OP_AND))
            {
                conditional_and_expression_p();
            }
        }

        /*conditional-and-expression-p:
            | "&&" inclusive-or-expression conditional-and-expression-p
            | EPSILON  */
        private void conditional_and_expression_p()
        {
            printIfDebug("conditional_and_expression_p");
            if(pass(TokenType.OP_AND))
            {
                consumeToken();
                inclusive_or_expression();
                if(pass(TokenType.OP_AND))
                    conditional_and_expression_p();
            }else{
                //EPSILON
            }
        }

        /*inclusive-or-expression:
	        | exclusive-or-expression inclusive-or-expression-p */
        private void inclusive_or_expression()
        {
            printIfDebug("inclusive_or_expression");
            exclusive_or_expression();
            if(pass(TokenType.OP_BITWISE_OR))
            {
                inclusive_or_expression_p();
            }
        }

        /*inclusive-or-expression-p:
            | "|" exclusive-or-expression inclusive-or-expression-p
            | EPSILON */
        private void inclusive_or_expression_p()
        {
            printIfDebug("inclusive_or_expression_p");
            if(pass(TokenType.OP_BITWISE_OR))
            {
                consumeToken();
                exclusive_or_expression();
                if(pass(TokenType.OP_BITWISE_OR))
                    inclusive_or_expression_p();
            }
        }

        /*exclusive-or-expression:
	        | and-expression exclusive-or-expression-p */
        private void exclusive_or_expression()
        {
            printIfDebug("exclusive_or_expression");
            and_expression();
            if(pass(TokenType.OP_XOR))
            {
                exclusive_or_expression_p();
            }
        }

        /*exclusive-or-expression-p:
            | "^" and-expression exclusive-or-expression-p
            | EPSILON  */
        private void exclusive_or_expression_p()
        {
            printIfDebug("exclusive_or_expression_p");
            if(pass(TokenType.OP_XOR))
            {
                consumeToken();
                and_expression();
                if(pass(TokenType.OP_XOR))
                    exclusive_or_expression_p();
            }else{
                //EPSILON
            }
        }

        /*and-expression:
	        | equality-expression and-expression-p */
        private void and_expression()
        {
            printIfDebug("and_expression");
            equality_expression();
            if(pass(TokenType.OP_BITWISE_AND))
            {
                and_expression_p();
            }
        }

        /*and-expression-p:
            | "&" equality-expression and-expression-p
            | EPSILON */
        private void and_expression_p()
        {
            printIfDebug("and_expression_p");
            if(pass(TokenType.OP_BITWISE_AND))
            {
                consumeToken();
                equality_expression();
                if(pass(TokenType.OP_BITWISE_AND))
                    and_expression_p();
            }else{
                //EPSILON
            }
        }

        /*equality-expression:
	        | relational-expression equality-expression-p */
        private void equality_expression()
        {
            printIfDebug("equality_expression");
            relational_expression();
            if(pass(equalityOperatorOptions))
            {
                equality_expression_p();
            }
        }

        /*equality-expression-p:
            | expression-equality-operator relational-expression equality-expression-p
            | EPSILON */
        private void equality_expression_p()
        {
            printIfDebug("equality_expression_p");
            if(pass(equalityOperatorOptions))
            {
                consumeToken();
                relational_expression();
                if(pass(equalityOperatorOptions))
                    equality_expression_p();
            }else{
                //EPSILON
            }
        }

        /*relational-expression:
	        | shift-expression relational-expression-p */
        private void relational_expression()
        {
            printIfDebug("relational_expression");
            shift_expression();
            if(pass(relationalOperatorOptions,Is_AsOperatorOptions))
            {
                relational_expression_p();
            }
        }

        /*relational-expression-p:
            | expression-relational-operator shift-expression relational-expression-p
            | is-as-operators type relational-expression-p
            | EPSILON  */
        private void relational_expression_p()
        {
            printIfDebug("relational_expression_p");
            if(pass(relationalOperatorOptions))
            {
                consumeToken();
                shift_expression();
                if(pass(relationalOperatorOptions,Is_AsOperatorOptions))
                    relational_expression_p();
            }else if(pass(Is_AsOperatorOptions))
            {
                consumeToken();
                if(!pass(typesOptions))
                    throwError("type expected");
                if(pass(relationalOperatorOptions,Is_AsOperatorOptions))
                    relational_expression_p();
            }else{
                //EPSILON
            }
        }

        /*shift-expression:
	        | additive-expression shift-expression-p */
        private void shift_expression()
        {
            printIfDebug("shift_expression");
            additive_expression();
            if(pass(shiftOperatorOptions))
                shift_expression_p();
        }

        /* shift-expression-p:
            | expression-shift-operator additive-expression shift-expression-p
            | EPSILON */
        private void shift_expression_p()
        {
            printIfDebug("shift_expression_p");
            if(pass(shiftOperatorOptions))
            {
                consumeToken();
                additive_expression();
                if(pass(shiftOperatorOptions))
                    shift_expression_p();
            }else{
                //EPSILON
            }
        }

        /*additive-expression:
	        | multiplicative-expression additive-expression-p */
        private void additive_expression()
        {
            printIfDebug("additive_expression");
            multiplicative_expression();
            if(pass(additiveOperatorOptions))
                additive_expression_p();
        }

        /*additive-expression-p:
            | additive-operators multiplicative-expression additive-expression-p
            | EPSILON */
        private void additive_expression_p()
        {
            printIfDebug("additive_expression_p");
            if(pass(additiveOperatorOptions))
            {
                consumeToken();
                multiplicative_expression();
                if(pass(additiveOperatorOptions))
                    additive_expression_p();
            }
        }

        /*multiplicative-expression:
	        | unary-expression multiplicative-expression-factorized */
        private void multiplicative_expression()
        {
            printIfDebug("multiplicative_expression");
            unary_expression();
            multiplicative_expression_factorized();
        }

        /*multiplicative-expression-factorized:
            | assignment-operator expression multiplicative-expression-p
            | multiplicative-expression-p */
        private void multiplicative_expression_factorized()
        {
            printIfDebug("multiplicative_expression_factorized");
            if(pass(assignmentOperatorOptions))
            {
                consumeToken();
                expression();
                if(pass(multiplicativeOperatorOptions))
                    multiplicative_expression_p();
            }else{
                multiplicative_expression_p();
            }
        }

        /*multiplicative-expression-p:
            | multiplicative-operators unary-expression multiplicative-expression-p
            | EPSILON */
        private void multiplicative_expression_p()
        {
            printIfDebug("multiplicative_expression_p");
            if(pass(multiplicativeOperatorOptions))
            {
                consumeToken();
                unary_expression();
                if(pass(multiplicativeOperatorOptions))
                    multiplicative_expression_p();
            }
        }
    }
}