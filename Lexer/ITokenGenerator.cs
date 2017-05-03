namespace Compiler
{
    public abstract class ITokenGenerator
    {
        protected Symbol currentSymbol;
        protected InputString inputString;
        public abstract bool validStart(Symbol currentSymbol);
        public abstract Token getToken();
        public Symbol getCurrentSymbol()
        {
            return currentSymbol;
        }

        public InputString getInputString()
        {
            return inputString;
        }

        public void setCurrentSymbol(Symbol currentSymbol)
        {
            this.currentSymbol = currentSymbol;
        }

        public void setInputString(InputString inputString)
        {
            this.inputString = inputString;
        }
    }
}