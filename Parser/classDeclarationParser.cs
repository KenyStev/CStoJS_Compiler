using System;

namespace Compiler
{
    public partial class Parser
    {
        /*class-declaration: ;
	        | class-modifier "class" identifier inheritance-base class-body optional-body-end */
        private void class_declaration()
        {
            printIfDebug("class_declaration");
            if(pass(TokenType.RW_ABSTRACT))
                consumeToken();
            if(!pass(TokenType.RW_CLASS))
                throwError("group-declaration 'class' expected");
            consumeToken();
            if(!pass(TokenType.ID))
                throwError("identifier expected");
            consumeToken();
            if(pass(TokenType.PUNT_COLON))
                inheritance_base();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            class_body();
            optional_body_end();
        }

        /*class-body:
	        | '{' optional-class-member-declaration-list '}' */
        private void class_body()
        {
            printIfDebug("class_body");
            if(!pass(TokenType.PUNT_CURLY_BRACKET_OPEN))
                throwError("'{' expected");
            consumeToken();
            optional_class_member_declaration_list();
            if(!pass(TokenType.PUNT_CURLY_BRACKET_CLOSE))
                throwError("'}' expected");
            consumeToken();
        }

        /*optional-class-member-declaration-list:
            | class-member-declaration optional-class-member-declaration-list
            | EPSILON */
        private void optional_class_member_declaration_list()
        {
            printIfDebug("optional_class_member_declaration_list");
            if(pass(encapsulationTypes,optionalModifierTypes,typesOptions,new TokenType[]{TokenType.RW_VOID}))
            {
                class_member_declaration();
            }else{
                //EPSILON
            }
        }

        /*class-member-declaration: 
	        | encapsulation-modifier class-member-declaration-options */
        private void class_member_declaration()
        {
            printIfDebug("class_member_declaration");
            throw new NotImplementedException();
        }

        /*inheritance-base:
            | ':' identifiers-list
            | EPSILON */
        private void inheritance_base()
        {
            printIfDebug("inheritance_base");
            if(pass(TokenType.PUNT_COLON))
            {
                consumeToken();
                identifiers_list();
            }else{
                //EPSILON
            }
        }
    }
}