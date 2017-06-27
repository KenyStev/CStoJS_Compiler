  using System;
  using lol;
  using otronamespace;
  class Program
  {
    static void Main()  
    {  
        Triangle t1 = new Triangle();  
        Triangle t2 =new Triangle();  
        t1.Width =4.0f;  
        t1.Height =4.0f;  
        t1.Style =otronamespace.Style.Isoceles;  
        t2.Width =8.0f;  
        t2.Height =12.0f;  
        t2.Style =otronamespace.Style.Right;  
        Console.WriteLine("Info for t1: ");  
        t1.ShowStyle();  
        t1.ShowDim();  
        Console.WriteLine("Area is " + t1.Area());  
        Console.WriteLine("");  
        Console.WriteLine("Info for t2: ");  
        t2.ShowStyle();  
        t2.ShowDim();  
        Console.WriteLine("Area is " + t2.Area());  
		
		
		var valor1 = 5;
		var valor2 = 10;
		var suma = valor1+valor2;
		Console.WriteLine("suma :" +suma);
		var resta = valor1-valor2;
		Console.WriteLine("resta :" +resta);
		var multiplicacion = valor1*valor2;
		Console.WriteLine("multiplicacion :" +multiplicacion);
		var division = valor1/valor2;
		Console.WriteLine("division :" +division);
		var mod = valor1 % valor2;
		Console.WriteLine("mod :" +mod);
		var incremento = valor1++;
		Console.WriteLine("incremento :" +suma);
		bool equal = valor1 == valor2;
		Console.WriteLine("equal :" +equal);
		bool distinto = valor1!=valor2;
		Console.WriteLine("distinto :" +distinto);
		bool mayorque = valor1>valor2;
		Console.WriteLine("mayor que:" +mayorque);
		bool menorque = valor1<valor2;
		Console.WriteLine("menor que:" +menorque);
		bool mayorIgualQue = valor1>=valor2;
		Console.WriteLine("mayor igual que:" +mayorIgualQue);
		bool menorIgualQue = valor1<=valor2;
		Console.WriteLine("menor que:" +menorIgualQue);
		bool and = true & false;
		Console.WriteLine("and que:" +and);
		bool or = true || false;
		Console.WriteLine("or que:" +or);
		bool negacion = !(and && or);
		Console.WriteLine("negacion que:" +negacion);
		var A = 60;
		Console.WriteLine("A:" +A);
		var B = 13;
		Console.WriteLine("B:" +B);
		
		var bitwiseAnd = (A & B);
		Console.WriteLine("A & B:" + bitwiseAnd);
		var bitwiseOr = (A | B);
		Console.WriteLine("A | B:" +bitwiseOr);
		var bitwiseXor = (A ^ B);
		Console.WriteLine("A ^ B:" +bitwiseXor);
		var bitwiseCompliment = (~A );
		Console.WriteLine("~A:" +bitwiseCompliment);
		var leftShift = A << 2;
		Console.WriteLine("A << 2:" +leftShift);
		var rightShift = A >> 2;
		Console.WriteLine("A >> 2:" +rightShift);
		
		var assignation = A + B;
		Console.WriteLine("A + B:" +assignation);
	    assignation += A;
		Console.WriteLine("assignation += A:" +assignation);
		assignation -= A;
		Console.WriteLine("assignation -= A:" +assignation);
		assignation *= A;
		Console.WriteLine("assignation *= A:" +assignation);
		assignation /= A;
		Console.WriteLine("assignation /= A:" +assignation);
		assignation &= A;
		Console.WriteLine("assignation &= A:" +assignation);
		assignation ^= A;
		Console.WriteLine("assignation ^= A:" +assignation);
		assignation |= A;
		Console.WriteLine("assignation |= A:" +assignation);
		
		var res = assignation > 3 ? 2 : 5;
		Console.WriteLine("assignation > 3 ? 2 : 5"+res);
		
		
    }  
  }