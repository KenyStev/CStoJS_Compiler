using System;
using RaimProgram.Common.Sorting;

namespace RaimProgram.Base
{
    public abstract class Person : ISortable, IPrintable
    {
        public string Name;
        public int Age;
        private int _numberCall=0;

        public Person()
        {

        }

        public void print(){
            Console.WriteLine(this.ToString());
        }
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
        public void PrintClassTypeWithNumberCall()
        {
            int numberCall = GetAndIncrementNumberCall();
            Console.WriteLine("Person" + " " + _numberCall);
        }
        public void fn(){
            Console.WriteLine("Person");
        }
        private void metodoPrivado(){
            Console.WriteLine("Soy el metodo Privado");
        }
        private int GetAndIncrementNumberCall()
        {
            _numberCall++;
            return _numberCall;
        }

        public abstract void SortPersons(Person[] persons, int size);
    }

    
}
