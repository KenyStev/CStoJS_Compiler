using System;

namespace Compiler
{
    public class InputString : IInput
    {
        private int colCount;
        private int rowCount;
        private int currentChar;

        public string initialInput { get; set; }

        public InputString(string input)
        {
            this.initialInput = input;
            this.rowCount = 1;
            this.colCount = 1;
            this.currentChar = 0;
        }

        public Symbol GetNextSymbol()
        {
            if (currentChar < initialInput.Length)
            {
                if (initialInput[currentChar] == '\n')
                {
                    ++rowCount;
                    colCount = 1;
                }

                var returnSymbol = new Symbol(
                    initialInput[currentChar++],
                    rowCount,
                    colCount++);

                return returnSymbol;
            }

            return new Symbol('\0', rowCount, colCount);
        }

        public Symbol LookAheadSymbol(int offset)
        {
            if (currentChar + offset < initialInput.Length)
            {
                var returnSymbol = new Symbol(
                    initialInput[currentChar + offset],
                    rowCount,
                    colCount);

                return returnSymbol;
            }

            return new Symbol('\0', rowCount, colCount);
        }

        public Symbol LookAheadSymbol()
        {
            return LookAheadSymbol(0);
        }
    }
}