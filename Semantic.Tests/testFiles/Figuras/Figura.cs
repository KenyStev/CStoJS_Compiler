using System;
using N1.N2;

namespace Figuras{
	public interface connectable
	{
		int connect(int numero);
	}
	public abstract class Figura : connectable{
        public abstract int connect(int numero);

        public abstract void numero();
	}
	public abstract class Figura2 : Figura{

        public override void numero(){}
		protected abstract void numero2();
	}
	public abstract class Figura3 : Figura2{
		// protected override void numero();
	}

	enum DIASDELASEMANA{
		LUNES = 5,
		MARTES = 5,
		MIERCOLES,
		JUEVES,
		VIERNES,
		SABADO,
		DOMINGO
	}
}

public enum DIASDELASEMANA2{
		LUNES = 5,
		MARTES = 5,
		MIERCOLES,
		JUEVES = 52,
		VIERNES,
		SABADO=10,
		DOMINGO
	}

public class myClaseAloneFiguras{
	
}