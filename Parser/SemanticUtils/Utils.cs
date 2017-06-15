using System;
using Compiler.TreeNodes;

namespace Compiler
{
    public class Utils
    {
        public static void ThrowError(string message)
        {
            throw new SemanticException(message);
        }

        public static bool isValidEncapsulation(EncapsulationNode encapsulation, TokenType encapsulationType)
        {
            if(encapsulation == null || (encapsulation.token == null && encapsulationType == TokenType.RW_PUBLIC))
                return true;
            return encapsulation.type == encapsulationType;
        }

        public static bool isValidEncapsulationForClass(EncapsulationNode encapsulation, TokenType encapsulationType)
        {
            if(encapsulation == null)
                return true;
            return encapsulation.type == encapsulationType;
        }

        public static string txtIncludes = @"
            namespace System {
                namespace IO{
                    public class TextWriter{
                        public static void WriteLine(string message){}
                    }
            
                    public class TextReader{
                        public static string ReadLine(){}
                    }
                }
                public class Console{
                    public static System.IO.TextWriter Out;
                    public static System.IO.TextReader In;
                    public static void WriteLine(string message){}
                    public static string ReadLine(){}
                }
            }
            public class Object{
                public virtual string ToString(){}
            }
            
            public class IntType{
                public override string ToString(){}
                public static int Parse(string s){}
                public static int TryParse(string s, int out){}
            
            }
                
            public class CharType{
                public override string ToString(){}
                public static int Parse(string s){}
                public static int TryParse(string s, char out){}
            }
            
            public class DictionaryTypeNode{
                public override string ToString(){}
            }
            public class FloatType{
                public override string ToString(){}
                public static int Parse(string s){}
                public static int TryParse(string s, float out){}
            }
            public class StringType{
                public override string ToString(){}
            }
            public class VarType{
                public override string ToString(){}
            }
            
            public class VoidType{
            }
            public class BoolType{
                public override string ToString(){}
            }
            ";
    }
}