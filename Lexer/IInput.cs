namespace Compiler
{
    public interface IInput
    {
        Symbol GetNextSymbol();
        Symbol LookAheadSymbol();
    }
}