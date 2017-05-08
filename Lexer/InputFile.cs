using System;
using System.IO;
using System.Text;

namespace Compiler
{
    public class InputFile : IInput
    {
        private int colCount;
        private int rowCount;
        private char currentChar;
        private BinaryReader file;

        public InputFile(String path)
        {
            file = new BinaryReader(File.Open(path,FileMode.Open,FileAccess.Read) ,Encoding.UTF8,true);
            rowCount = 1;
            colCount = 1;
        }

        public Symbol GetNextSymbol()
        {
            if(file.BaseStream.Position < file.BaseStream.Length)
            {
                currentChar = file.ReadChar();

                if (currentChar == '\n')
                {
                    ++rowCount;
                    colCount = 0;
                }
                var returnSymbol = new Symbol(
                    currentChar,
                    rowCount,
                    colCount++);
                return returnSymbol;
            }

            return new Symbol('\0', rowCount, colCount);
        }

        public Symbol LookAheadSymbol(int offset)
        {
            if(file.BaseStream.Position + offset < file.BaseStream.Length)
            {
                var currentPos = file.BaseStream.Position;
                file.BaseStream.Seek(currentPos + offset, SeekOrigin.Begin);
                var symbolToRetorn = new Symbol((char)file.PeekChar(), rowCount, colCount);
                file.BaseStream.Seek(currentPos, SeekOrigin.Begin);
                return symbolToRetorn;
            }
            return new Symbol('\0', rowCount, colCount);
        }

        public Symbol LookAheadSymbol()
        {
            return LookAheadSymbol(0);
        }
    }
}