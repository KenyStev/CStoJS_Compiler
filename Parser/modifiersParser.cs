using System;
using Compiler.TreeNodes;

namespace Compiler
{
    public partial class Parser
    {
        /*encapsulation-modifier:
            | "public"
            | "protected"
            | "private"
            | EPSILON */
        private EncapsulationNode encapsulation_modifier()
        {
            printIfDebug("encapsulation_modifier");
            if(pass(encapsulationOptions))
            {
                var encapMod = new EncapsulationNode(token.type,token);
                consumeToken();
                return encapMod;
            }else
            {
                return new EncapsulationNode(TokenType.RW_PRIVATE,token);
            }
        }
    }
}