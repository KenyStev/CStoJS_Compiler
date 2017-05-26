using System;

namespace Compiler.TreeNodes.Types
{
    public class TypeNode
    {
        private EncapsulationNode encapsulationType;

        public void setEncapsulationMode(EncapsulationNode encapMod)
        {
            this.encapsulationType = encapMod;
        }
    }
}