using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        TokenType [] encapsulationOptions = {TokenType.RW_PUBLIC, TokenType.RW_PRIVATE, TokenType.RW_PROTECTED};
        TokenType [] typesDeclarationOptions = {TokenType.RW_ABSTRACT, TokenType.RW_CLASS, TokenType.RW_INTERFACE,TokenType.RW_ENUM};
        TokenType [] optionalModifiersOptions = {TokenType.RW_ABSTRACT,TokenType.RW_STATIC,TokenType.RW_OVERRIDE,TokenType.RW_VIRTUAL};
        TokenType [] typesOptions = {TokenType.RW_INT,TokenType.RW_CHAR,TokenType.RW_STRING,TokenType.RW_BOOL,TokenType.RW_FLOAT,TokenType.ID};
        TokenType [] maybeEmptyBlockOptions = {TokenType.PUNT_CURLY_BRACKET_OPEN,TokenType.PUNT_END_STATEMENT_SEMICOLON};
        TokenType [] selectionsOptionsStatements = {TokenType.RW_IF,TokenType.RW_SWITCH};
        TokenType [] switchLabelOptions = {TokenType.RW_CASE,TokenType.RW_DEFAULT};
        TokenType [] iteratorsOptionsStatements = {TokenType.RW_FOR,TokenType.RW_FOREACH,TokenType.RW_DO,TokenType.RW_WHILE};
        TokenType [] jumpsOptionsStatements = {TokenType.RW_BREAK,TokenType.RW_CONTINUE,TokenType.RW_RETURN};
        TokenType[] primaryOptionsPrime = {TokenType.OP_PLUS_PLUS,TokenType.OP_MINUS_MINUS,TokenType.PUNT_ACCESOR};
        TokenType [] StatementsOptions = {
            TokenType.PUNT_PAREN_OPEN,
            TokenType.OP_SUM,
            TokenType.OP_SUBSTRACT,
            TokenType.OP_PLUS_PLUS,
            TokenType.OP_MINUS_MINUS,
            TokenType.OP_NOT,
            TokenType.OP_BITWISE_NOT,
            TokenType.OP_MULTIPLICATION};
        
        TokenType[] literalOptions = {
            TokenType.LIT_INT,
            TokenType.LIT_CHAR,
            TokenType.LIT_FLOAT,
            TokenType.LIT_STRING,
            TokenType.LIT_BOOL
        };
        TokenType[] unaryOperatorOptions = {
            TokenType.OP_SUM,
            TokenType.OP_SUBSTRACT,
            TokenType.OP_PLUS_PLUS,
            TokenType.OP_MINUS_MINUS,
            TokenType.OP_NOT,
            TokenType.OP_BITWISE_NOT,
            TokenType.OP_MULTIPLICATION
        };
        TokenType[] assignmentOperatorOptions = {
            TokenType.OP_ASSIGN,
            TokenType.OP_ASSIGN_SUM,
            TokenType.OP_ASSIGN_SUBSTRACT,
            TokenType.OP_ASSIGN_MULTIPLICATION,
            TokenType.OP_ASSIGN_DIVISION,
            TokenType.OP_ASSIGN_MODULO,
            TokenType.OP_ASSIGN_BITWISE_AND,
            TokenType.OP_ASSIGN_BITWISE_OR,
            TokenType.OP_ASSIGN_XOR,
            TokenType.OP_ASSIGN_SHIFT_LEFT,
            TokenType.OP_ASSIGN_SHIFT_RIGHT,
        };
        TokenType[] relationalOperatorOptions = {
            TokenType.OP_LESS_THAN,
            TokenType.OP_MORE_THAN,
            TokenType.OP_LESS_AND_EQUAL_THAN,
            TokenType.OP_MORE_AND_EQUAL_THAN
        };

        TokenType[] equalityOperatorOptions = {
            TokenType.OP_EQUAL,
            TokenType.OP_DISTINCT
        };

        TokenType[] shiftOperatorOptions = {
            TokenType.OP_SHIFT_LEFT,
            TokenType.OP_SHIFT_RIGHT
        };

        TokenType[] additiveOperatorOptions = {
            TokenType.OP_SUM,
            TokenType.OP_SUBSTRACT
        };

        TokenType[] Is_AsOperatorOptions = {
            TokenType.OP_IS,
            TokenType.OP_AS
        };

        TokenType[] multiplicativeOperatorOptions = {
            TokenType.OP_MULTIPLICATION,
            TokenType.OP_DIVISION,
            TokenType.OP_MODULO
        };

        TokenType[] unaryExpressionOptions = {
            TokenType.PUNT_PAREN_OPEN,TokenType.RW_NEW, TokenType.ID,
            TokenType.RW_THIS
        };
                                                
        private TokenType [] expressionOptions()
        {
            TokenType[] nuevo = {TokenType.OP_TERNARY, TokenType.PUNT_COLON,
                TokenType.OP_NULL_COALESCING, TokenType.OP_OR,
                TokenType.OP_AND, TokenType.OP_BITWISE_OR,
                TokenType.OP_XOR, TokenType.OP_BITWISE_AND,
                TokenType.PUNT_PAREN_OPEN, TokenType.RW_NEW,
                TokenType.ID, TokenType.RW_THIS};

            return nuevo.Concat(equalityOperatorOptions).Concat(relationalOperatorOptions).
                Concat(Is_AsOperatorOptions).Concat(shiftOperatorOptions).Concat(additiveOperatorOptions).
                Concat(multiplicativeOperatorOptions).Concat(assignmentOperatorOptions).Concat(unaryOperatorOptions)
                .Concat(literalOptions).ToArray();
        }

        private TokenType[] embededOptions()
        {
            return maybeEmptyBlockOptions.Concat(selectionsOptionsStatements).Concat(iteratorsOptionsStatements)
            .Concat(jumpsOptionsStatements).Concat(StatementsOptions).Concat(unaryOperatorOptions)
            .Concat(literalOptions).Concat(unaryExpressionOptions).ToArray();
        }
        
        private bool pass(params TokenType [] types)
        {
            return types.Contains(token.type);
        }

        private bool pass(params TokenType [][] types)
        {
            foreach(var type in types)
                if(type.Contains(token.type))
                    return true;
            return false;
        }

        private void throwError(string msj)
        {
            throw new SyntaxTokenExpectedException(msj+" before: "+token.lexeme+"'. line:"+token.row+","+token.column);
        }

        private void consumeToken()
        {
            if (look_ahead.Count > 0)
            {
                token = look_ahead[0];
                removeLookAhead(0);
            }
            else
            {
                token = lexer.GetNextToken();
            }
        }

        private void removeLookAhead(int index)
        {
            if(look_ahead.Count >0)
                look_ahead.RemoveAt(index);
        }
        
        public void addLookAhead(Token token)
        {
            look_ahead.Add(token);
        }
    }
}