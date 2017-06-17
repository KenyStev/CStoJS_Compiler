using System;
using Compiler.SemanticAPI.ContextUtils;
using Compiler.TreeNodes;
using Compiler.TreeNodes.Types;

namespace Compiler
{
    public class Utils
    {
        public const string Bool = "BoolType";
        public const string Char = "CharType";
        public const string Float = "FloatType";
        public const string Int = "IntType";

        public const string String = "StringType";
        public const string Class = "ClassType";
        public const string Interface = "InterfaceType";
        public const string Enum = "EnumType";
        public const string Null = "NullType";
        public static string Void = "VoidType";
        public static string Array = "ArrayType";
        public static string Var = "VarType";
        public static string[] primitives = {Bool, Char, Float,Int};
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

        public static string getNameForType(TypeNode type)
        {
            return (type is ArrayTypeNode)?((ArrayTypeNode)type).DataType.ToString():type.ToString();
        }
    }
}