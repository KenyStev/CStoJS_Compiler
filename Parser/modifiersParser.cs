using System;

namespace Compiler
{
    public partial class Parser
    {
        /*encapsulation-modifier:
            | "public"
            | "protected"
            | "private"
            | EPSILON */
        private void encapsulation_modifier()
        {
            printIfDebug("encapsulation_modifier");
            if(pass(encapsulationOptions))
            {
                consumeToken();
            }else
            {
                //EPSILON
            }
        }
    }
}