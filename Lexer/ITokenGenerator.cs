namespace Compiler
{
    public abstract class ITokenGenerator
    {
        protected Symbol currentSymbol;
        protected IInput inputString;
        public abstract bool validStart(Symbol currentSymbol);
        public abstract Token getToken();
        public Symbol getCurrentSymbol()
        {
            return currentSymbol;
        }

        public IInput getInputString()
        {
            return inputString;
        }

        public void setCurrentSymbol(Symbol currentSymbol)
        {
            this.currentSymbol = currentSymbol;
        }

        public void setInputString(IInput inputString)
        {
            this.inputString = inputString;
        }
    }
}