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

            public static string JsSystem = @"
var ToString = function (target) {
let temp = target;
return temp.toString();
};

var CharToInt = function (target) {
	return target.charCodeAt(0);
};

var ToIntPrecision = function (target) {
	return Math.floor(target);
};

var GeneratedCode = {};
GeneratedCode.System = {};
GeneratedCode.System.IO = {};

GeneratedCode.Object = class {
	Object() { }
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	ToString() {
		return json.stringify(this);
	}
}

GeneratedCode.IntType = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	ToString() {
		return json.stringify(this);
	}
	static Parse_StringType(s) {
		return +(s);
	}
}

GeneratedCode.CharType = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	ToString() {
		return json.stringify(this);
	}
	static Parse_StringType(s) {
		return s;
	}
}

GeneratedCode.FloatType = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	ToString() {
		return json.stringify(this);
	}
	static Parse_StringType(s) {
		return +(s);
	}
}

GeneratedCode.StringType = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	ToString() {
		return json.stringify(this);
	}
}

GeneratedCode.BoolType = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	ToString() {
		return json.stringify(this);
	}
}

GeneratedCode.System.Console = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	static WriteLine_StringType(message) {
		console.log(message);
	}
	static ReadLine() { }
}

GeneratedCode.System.IO.TextWriter = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	static WriteLine_StringType(message) {
		console.log(message);
	}
}

GeneratedCode.System.Console.Out = new GeneratedCode.System.IO.TextWriter();
GeneratedCode.System.IO.TextReader = class {
	constructor() {
		let argumentos = Array.from(arguments);
		let argus = argumentos.slice(1);
		if (argumentos.length >= 1) this[arguments[0]](...argus);
	}
	static ReadLine() { }
}
            ";
        public static EncapsulationNode privateLevel = new EncapsulationNode(TokenType.RW_PRIVATE,null);
        public static EncapsulationNode protectedLevel = new EncapsulationNode(TokenType.RW_PROTECTED,null);
        public static EncapsulationNode publicLevel = new EncapsulationNode(TokenType.RW_PUBLIC,null);

        public static string getNameForType(TypeNode type)
        {
            return (type is ArrayTypeNode)?((ArrayTypeNode)type).DataType.ToString():type.ToString();
        }

        public static TokenType[] assignmentOperatorOptions = {
            TokenType.OP_ASSIGN,
            TokenType.OP_ASSIGN_SUM,
            TokenType.OP_ASSIGN_SUBSTRACT,
            TokenType.OP_ASSIGN_MULTIPLICATION,
            TokenType.OP_ASSIGN_DIVISION,
            TokenType.OP_ASSIGN_MODULO,
            TokenType.OP_ASSIGN_BITWISE_AND,
            TokenType.OP_ASSIGN_BITWISE_OR,
            TokenType.OP_ASSIGN_XOR,
            TokenType.OP_ASSIGN_SHIFT_LEFT,
            TokenType.OP_ASSIGN_SHIFT_RIGHT,
        };

        public static bool passAssignExpression(Token token)
        {
            foreach (var item in assignmentOperatorOptions)
            {
                if(item == token.type)
                    return true;
            }
            return false;
        }
    }
}