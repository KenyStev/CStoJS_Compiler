using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        private void expression()
        {
            printIfDebug("expression");
            if(!pass(expressionOptions()))
                throwError(expressionOptions().ToString()+" expected");
            consumeToken();
        }
    }
}