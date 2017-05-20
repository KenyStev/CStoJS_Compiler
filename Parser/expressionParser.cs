using System;
using System.Linq;

namespace Compiler
{
    public partial class Parser
    {
        private void expression()
        {
            printIfDebug("expression");
            if(!pass(TokenType.LIT_INT))
                throwError("LIT_INT expected");
            consumeToken();
        }
    }
}