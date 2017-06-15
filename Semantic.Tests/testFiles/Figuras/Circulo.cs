using N1;

namespace Figuras{
	public class Circulo : insideFiguras.Otra{
		public int field1;
		public string field2;
		public static float field3;
		public bool field4;
		private Types t;
		myClase[,][][] field5 = new myClase[3,2][][]{{new myClase[8][],new myClase[2][]}, {new myClase[3][],new myClase[7][]}, {new myClase[4][],new myClase[5][]}};

	}
	namespace insideFiguras{
		public interface Types{
			
		}

		public class Otra
		{

		}
		public class Otra2
		{

		}
	}
}
public interface myInterface : Types, outsideInterface{
	void getNombre(int index);
	void getNompre(string name);
	void getNombre(int[] index);
	void getNombre(int[,,][] index);
	int setIndex(int index);
}

public interface Types{
			
}