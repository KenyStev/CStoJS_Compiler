using System;

namespace Figuras
{
    namespace OtrasFuguras
    {
        public class Rectangulo : Figura
        {
            public override int connect(int numero)
            {
                // throw new NotImplementedException();x    
                return 0;
            }

            public override void numero()
            {
                // throw new NotImplementedException();
            }
        }

        public class Elipse
        {
            public static int proyection = 5;

            public float getArea()
            {
                float a = proyection*(2.1416f*20);
                return a;
            }
        }

        public class Ellipsiode
        {
            public float getArea()
            {
                float a = Elipse.proyection*(2.1416f*20);
                return a;
            }
        }
    }
}