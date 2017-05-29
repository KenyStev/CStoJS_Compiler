using System;
using System.Linq;
using Compiler.TreeNodes.Expressions;
using Compiler.TreeNodes.Expressions.EqualityExpressions;
using Compiler.TreeNodes.Expressions.RelationalExpressions;
using Compiler.TreeNodes.Expressions.TypeTestingExpressions;
using Compiler.TreeNodes.Expressions.ShiftExpressions;
using Compiler.TreeNodes.Expressions.AdditiveExpressions;
using Compiler.TreeNodes.Expressions.MultipicativeExpressions;
using Compiler.TreeNodes.Expressions.UnaryExpressions;

namespace Compiler
{
    public partial class Parser
    {
        /*expression:
            | conditional-expression */
        private ExpressionNode expression()
        {
            printIfDebug("expression");
            if(!pass(expressionOptions()))
                throwError("Operator, identifier or literal in expression expected");
            return conditional_expression();
        }

        /*conditional-expression:
	        | null-coalescing-expression conditional-expression-p */
        private ExpressionNode conditional_expression()
        {
            printIfDebug("conditional_expression");
            var exp = null_coalescing_expression();
            return conditional_expression_p(exp);
        }

        /*conditional-expression-p:
            | '?' expression ':' expression 
            | EPSILON */
        private ExpressionNode conditional_expression_p(ExpressionNode conditionalExpression)
        {
            printIfDebug("conditional_expression_p");
            if(pass(TokenType.OP_TERNARY))
            {
                consumeToken();
                var trueExpression = expression();
                if(!pass(TokenType.PUNT_COLON))
                    throwError(": expected");
                consumeToken();
                var falseExpression = expression();
                return new TernaryExpressionNode(conditionalExpression,trueExpression,falseExpression);
            }else{
                return conditionalExpression;
            }
        }

        /*null-coalescing-expression:
	        | conditional-or-expression null-coalescing-expression-p */
        private ExpressionNode null_coalescing_expression()
        {
            printIfDebug("null_coalescing_expression");
            var expCondOr = conditional_or_expression();
            return null_coalescing_expression_p(expCondOr);
        }

        /*null-coalescing-expression-p:
            | "??" null-coalescing-expression
            | EPSILON */
        private ExpressionNode null_coalescing_expression_p(ExpressionNode nullableExpression)
        {
            printIfDebug("null_coalescing_expression_p");
            if(pass(TokenType.OP_NULL_COALESCING))
            {
                consumeToken();
                var rightExpression = null_coalescing_expression();
                return new NullCoalescingExpressionNode(nullableExpression,rightExpression);
            }else{
                return nullableExpression;
            }
        }

        /*conditional-or-expression:
	        | conditional-and-expression conditional-or-expression-p */
        private ExpressionNode conditional_or_expression()
        {
            printIfDebug("conditional_or_expression");
            var ConditionOr = conditional_and_expression();
            return conditional_or_expression_p(ConditionOr);
        }

        /*conditional-or-expression-p:
            | "||" conditional-and-expression conditional-or-expression-p 
            | EPSILON */
        private ExpressionNode conditional_or_expression_p(ExpressionNode OrExpression)
        {
            printIfDebug("conditional_or_expression_p");
            if(pass(TokenType.OP_OR))
            {
                consumeToken();
                var AndExpression = conditional_and_expression();
                return conditional_or_expression_p(new ConditionalOrExpressionNode(OrExpression,AndExpression));
            }else{
                return OrExpression;
            }
        }

        /*conditional-and-expression:
	        | inclusive-or-expression conditional-and-expression-p */
        private ExpressionNode conditional_and_expression()
        {
            printIfDebug("conditional_and_expression");
            var bitsOr = inclusive_or_expression();
            return conditional_and_expression_p(bitsOr);
        }

        /*conditional-and-expression-p:
            | "&&" inclusive-or-expression conditional-and-expression-p
            | EPSILON  */
        private ExpressionNode conditional_and_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("conditional_and_expression_p");
            if(pass(TokenType.OP_AND))
            {
                consumeToken();
                var bitsOt = inclusive_or_expression();
                return conditional_and_expression_p(new ConditionalAndExpressionNode(leftExpression,bitsOt));
            }else{
                return leftExpression;
            }
        }

        /*inclusive-or-expression:
	        | exclusive-or-expression inclusive-or-expression-p */
        private ExpressionNode inclusive_or_expression()
        {
            printIfDebug("inclusive_or_expression");
            var exclusiveOrExpression = exclusive_or_expression();
            return inclusive_or_expression_p(exclusiveOrExpression);
        }

        /*inclusive-or-expression-p:
            | "|" exclusive-or-expression inclusive-or-expression-p
            | EPSILON */
        private ExpressionNode inclusive_or_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("inclusive_or_expression_p");
            if(pass(TokenType.OP_BITWISE_OR))
            {
                consumeToken();
                var exclusiveOrExpression = exclusive_or_expression();
                return inclusive_or_expression_p(new BitwiseOrExpressionNode(leftExpression,exclusiveOrExpression));
            }else{
                return leftExpression;
            }
        }

        /*exclusive-or-expression:
	        | and-expression exclusive-or-expression-p */
        private ExpressionNode exclusive_or_expression()
        {
            printIfDebug("exclusive_or_expression");
            var bitsAnd = and_expression();
            return exclusive_or_expression_p(bitsAnd);
        }

        /*exclusive-or-expression-p:
            | "^" and-expression exclusive-or-expression-p
            | EPSILON  */
        private ExpressionNode exclusive_or_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("exclusive_or_expression_p");
            if(pass(TokenType.OP_XOR))
            {
                consumeToken();
                var bitsAnd = and_expression();
                return exclusive_or_expression_p(new ExclusiveOrExpression(leftExpression,bitsAnd));
            }else{
                return leftExpression;
            }
        }

        /*and-expression:
	        | equality-expression and-expression-p */
        private ExpressionNode and_expression()
        {
            printIfDebug("and_expression");
            var equalityExpression = equality_expression();
            return and_expression_p(equalityExpression);
        }

        /*and-expression-p:
            | "&" equality-expression and-expression-p
            | EPSILON */
        private ExpressionNode and_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("and_expression_p");
            if(pass(TokenType.OP_BITWISE_AND))
            {
                consumeToken();
                var equalityExpression = equality_expression();
                return and_expression_p(new BitwiseAndExpressionNode(leftExpression,equalityExpression));
            }else{
                return leftExpression;
            }
        }

        /*equality-expression:
	        | relational-expression equality-expression-p */
        private ExpressionNode equality_expression()
        {
            printIfDebug("equality_expression");
            var relationalExpression = relational_expression();
            return equality_expression_p(relationalExpression);
        }

        /*equality-expression-p:
            | expression-equality-operator relational-expression equality-expression-p
            | EPSILON */
        private ExpressionNode equality_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("equality_expression_p");
            if(pass(equalityOperatorOptions))
            {
                Token equalityOperation = token;
                consumeToken();
                var relationalExpression = relational_expression();
                ExpressionNode resultExpression = null;
                if(equalityOperation.type==TokenType.OP_EQUAL)
                    resultExpression = new EqualExpressionNode(leftExpression,relationalExpression);
                else
                    resultExpression = new DistinctExpressionNode(leftExpression,relationalExpression);

                return equality_expression_p(resultExpression);
            }else{
                return leftExpression;
            }
        }

        /*relational-expression:
	        | shift-expression relational-expression-p */
        private ExpressionNode relational_expression()
        {
            printIfDebug("relational_expression");
            var shiftExpression = shift_expression();
            return relational_expression_p(shiftExpression);
        }

        /*relational-expression-p:
            | expression-relational-operator shift-expression relational-expression-p
            | is-as-operators type relational-expression-p
            | EPSILON  */
        private ExpressionNode relational_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("relational_expression_p");
            if(pass(relationalOperatorOptions))
            {
                Token relationalOperator = token;
                consumeToken();
                var shiftExpression = shift_expression();
                ExpressionNode resultExpression = null;
                if(relationalOperator.type == TokenType.OP_LESS_THAN)
                    resultExpression = new LessThanExpressionNode(leftExpression,shiftExpression);
                else if(relationalOperator.type == TokenType.OP_MORE_THAN)
                    resultExpression = new GreaterThanExpressionNode(leftExpression,shiftExpression);
                else if(relationalOperator.type == TokenType.OP_LESS_AND_EQUAL_THAN)
                    resultExpression = new LessOrEqualThanExpressionNode(leftExpression,shiftExpression);
                else
                    resultExpression = new GreaterOrEqualThanExpressionNode(leftExpression,shiftExpression);
                return relational_expression_p(resultExpression);
            }else if(pass(Is_AsOperatorOptions))
            {
                Token typeTestOperator = token;
                consumeToken();
                if(!pass(typesOptions))
                    throwError("type expected");
                var type = types();
                TypeTestingExpressionNode resultExpression = null;
                if(typeTestOperator.type==TokenType.OP_IS)
                    resultExpression = new IsTypeTestNode(leftExpression,type);
                else
                    resultExpression = new AsTypeTestNode(leftExpression,type);
                return relational_expression_p(resultExpression);
            }else{
                return leftExpression;
            }
        }

        /*shift-expression:
	        | additive-expression shift-expression-p */
        private ExpressionNode shift_expression()
        {
            printIfDebug("shift_expression");
            var additiveExpression = additive_expression();
            return shift_expression_p(additiveExpression);
        }

        /* shift-expression-p:
            | expression-shift-operator additive-expression shift-expression-p
            | EPSILON */
        private ExpressionNode shift_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("shift_expression_p");
            if(pass(shiftOperatorOptions))
            {
                Token shiftOperator = token;
                consumeToken();
                var additiveExpression = additive_expression();
                ExpressionNode resultExpression = null;
                if(shiftOperator.type==TokenType.OP_SHIFT_LEFT)
                    resultExpression = new ShiftLeftNode(leftExpression,additiveExpression);
                else
                    resultExpression = new ShiftRightNode(leftExpression,additiveExpression);
                return shift_expression_p(resultExpression);
            }else{
                return leftExpression;
            }
        }

        /*additive-expression:
	        | multiplicative-expression additive-expression-p */
        private ExpressionNode additive_expression()
        {
            printIfDebug("additive_expression");
            var multExpression = multiplicative_expression();
            return additive_expression_p(multExpression);
        }

        /*additive-expression-p:
            | additive-operators multiplicative-expression additive-expression-p
            | EPSILON */
        private ExpressionNode additive_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("additive_expression_p");
            if(pass(additiveOperatorOptions))
            {
                Token additiveOperatior = token;
                consumeToken();
                var multExpression = multiplicative_expression();
                ExpressionNode resultExpression = null;
                if(additiveOperatior.type==TokenType.OP_SUM)
                    resultExpression = new SumExpressionNode(leftExpression,multExpression);
                else
                    resultExpression = new SubExpressionNode(leftExpression,multExpression);

                return additive_expression_p(resultExpression);
            }else{
                return leftExpression;
            }
        }

        /*multiplicative-expression:
	        | unary-expression multiplicative-expression-factorized */
        private ExpressionNode multiplicative_expression()
        {
            printIfDebug("multiplicative_expression");
            var unaryExpression = unary_expression();
            return multiplicative_expression_factorized(unaryExpression);
        }

        /*multiplicative-expression-factorized:
            | assignment-operator expression multiplicative-expression-p
            | multiplicative-expression-p */
        private ExpressionNode multiplicative_expression_factorized(ExpressionNode unaryExpression)
        {
            printIfDebug("multiplicative_expression_factorized");
            if(pass(assignmentOperatorOptions))
            {
                TokenType assignType = token.type;
                consumeToken();
                var assignExpression = expression();
                return multiplicative_expression_p(new AssignExpressionNode(unaryExpression,assignType,assignExpression));
            }else{
                return multiplicative_expression_p(unaryExpression);
            }
        }

        /*multiplicative-expression-p:
            | multiplicative-operators unary-expression multiplicative-expression-p
            | EPSILON */
        private ExpressionNode multiplicative_expression_p(ExpressionNode leftExpression)
        {
            printIfDebug("multiplicative_expression_p");
            if(pass(multiplicativeOperatorOptions))
            {
                Token multiplicativeOperator = token;
                consumeToken();
                var unaryExpression = unary_expression();
                ExpressionNode resultExpression = null;
                if(multiplicativeOperator.type==TokenType.OP_MULTIPLICATION)
                    resultExpression = new MultNode(leftExpression,unaryExpression);
                else if(multiplicativeOperator.type==TokenType.OP_DIVISION)
                    resultExpression = new DivNode(leftExpression,unaryExpression);
                else
                    resultExpression = new ModNode(leftExpression,unaryExpression);
                return multiplicative_expression_p(resultExpression);
            }
            return leftExpression;
        }
    }
}