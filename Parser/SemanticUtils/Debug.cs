using System;

namespace Compiler
{
    public class Debug
    {
        public static bool printable = true;
        public static void print(string msg)
        {
            if(printable)
                Console.WriteLine(msg);
        }
    }
}