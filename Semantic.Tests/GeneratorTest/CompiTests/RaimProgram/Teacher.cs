using System;
using RaimProgram.Base;
namespace RaimProgram.Base.Derivatives
{
    public class Random{
        public float Nextfloat(){
            return 5.5f;
        }
        public Random(){}
    }
    public class Teacher : Person
    {
        private static Random random;

        public Teacher()
        {
            random = new Random();
        }

        public Teacher(string name, int age)
        {
            this.Name = name;
            this.Age = age;
            random = new Random();
        }

        public float GetRandomGrumpiness(float lowerLimit, float upperLimit)
        {
            float range = upperLimit - lowerLimit;
            float number =  random.Nextfloat();
            return (float)(number * range) + lowerLimit;
        }

        public override void SortPersons(Person[] persons, int size)
        {
            BubbleSort(persons, size);
        }

        public override string ToString()
        {
            return "Name5: "+ this.Name +"\nAg5: "+ this.Age;
        }

        public static void BubbleSort(Person[] persons, int size)
        {
            for (int pass = 1; pass < size; pass++)
            {
                for (int i = 0; i < size - pass; i++)
                {
                    if (persons[i].Age >= persons[i + 1].Age)
                    {
                        var temp = persons[i];
                        persons[i] = persons[i + 1];
                        persons[i + 1] = temp;
                    }
                }
            }
        }
    }
}
