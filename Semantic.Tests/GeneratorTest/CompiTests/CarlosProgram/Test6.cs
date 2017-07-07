using System;
namespace otronamespace{
public enum Style 
{
	Isoceles,
	Right
}
public interface ITriangle
{
	float Area();
}

class Triangle : lol.Shape, ITriangle
{  
    public Style Style; 
	
    public float Area()  
    {  
        return base.Width * base.Height / 2;  
    }  
	
	public int Prueba()
	{
		return 0;
	}
	
      
    public void ShowStyle()  
    {  
		if(this.Style == 0){
			Console.WriteLine("Triangle is " + this.Style + " Isoceles");
		}else if(this.Style == 1){
			Console.WriteLine("Triangle is " + this.Style + " Right");
		}
          
    }  
}  
}