using Figuras;
using N1.N2;

namespace N1{

	public class myClase : myClase2{
		static int inaccesibleNode = myClase2.inaccessible;
		static DIASDELASEMANA otroDia = DIASDELASEMANA.DOMINGO;
		static myClase NullableAssign = null;
		static int otronumero2;
		static int otronumero = numero;
		static int numero = 9;
		int denuevo = numero;
		static Circulo c = new Circulo();
		static Circulo clase = c as Circulo;
		DIASDELASEMANA t = DIASDELASEMANA.LUNES;
		public static int ca = (int)'a';
		int cat = ca;
		private static bool b = true;
		static bool f = false;
		bool r = b && f;
		float f1 = 1.2f;
		int i1 = 50;
		float f2 = ((float)1.5f);
		bool r2 = c == null;
		bool h = clase is Circulo;
		int count = 0;

        public bool H;

        public myClase(){
			
		}
		public void metodo(){
			// (count)++;
			// ((parent)child).print();
			base.basePrueba = 5;
		}

		public void getType(int index, bool maybe){

		}
		public myClase(int nuevo){
			// new myClase2(adios,5);
			// var d = c as null;
		}
		public void getType(Circulo index, Types t){

		}
	}

	public interface outsideInterface{
		void go();
	}
	public interface IName : outsideInterface
	{
		void go();
	}
	namespace N2{
        using System;
        using System.Buffers;
		public class myClase2: myClaseAlone{
			public static int inaccessible = 3;
			protected int protectedField = 3;
			public int publicField = 3;
		}

        namespace N3
        {
            using System.CodeDom;
            using System.Collections;
        }
		
	}
}
public class myClaseAlone{
	protected int basePrueba;
}