using RaimProgram.Common;
using System;
using RaimProgram.Base;
namespace RaimProgram.Base.Derivatives
{
    public class Student : Person
    {
        protected StudentType studentType;
        
        public Student()
        {

        }

        public Student(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public void SetStudentType(StudentType type)
        {
            StudentType = type;
        }

        public void fn(){
            base.fn();
            Console.WriteLine("Student");
        }

        public void SetStudentType(int type)
        {
            switch(type)
            {
                case 0:
                    studentType = StudentType.FRESHMAN;
                    break;
                case 1:
                    studentType = StudentType.SOPHOMORE;
                    break;
                case 2:
                    studentType = StudentType.JUNIOR;
                    break;
                case 3:
                    studentType = StudentType.SENIOR;
                    break;
                default:
                    Console.WriteLine("Invalid entry for StudentType");
                    break;
            }
        }

        public StudentType GetStudentType()
        {
            return StudentType;
        }

        public override void SortPersons(Person[] persons, int size)
        {
            QuickSort(persons, 0, size - 1);
        }

        public override string ToString()
        {
            return "Name: "+ this.Name +"\nAge: "+ this.Age;
        }

        public void QuickSort(Person[] persons, int left, int right)
        {
            int i = left;
            int j = right;
            var pivot = persons[(left + right) / 2];

            do
            {
                while ((persons[i].Age < pivot.Age) && (i < right)) i++;
                while ((pivot.Age < persons[j].Age) && (j > left)) j--;

                if(i <= j)
                {
                    var temp = persons[i];
                    persons[i] = persons[j];
                    persons[j] = temp;
                    i++;
                    j--;
                }
            } while (i <= j);

            if (left < j) QuickSort(persons, left, j);
            if (i < right) QuickSort(persons, i, right);
        }
    }
}
