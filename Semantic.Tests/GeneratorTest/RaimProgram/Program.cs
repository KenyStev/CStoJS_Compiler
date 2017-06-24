using RaimProgram.Base;
using RaimProgram.Base.Derivatives;
using System;

namespace RaimProgram
{
    public class Program
    {
        public static void Main(string[] args)
        {

            int[,] array2 = { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            for (int i = 0; i < 4; i++)
            {
                for(int x=0; x < 2; x++)
                {
                    array2[i, x] *= 2;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Arreglo: "+i);
                for (int x = 0; x < 2; x++)
                {
                    Console.WriteLine("val :"+array2[i, x]);
                }
            }

            Student student = new Student();
            Person[] students = new Student[4];
            students[0] = new Student("D", 50);
            students[1] = new Student("C", 22);
            students[2] = new Student("B", 40);
            students[3] = new Student("A", 35);

            student.SortPersons(students, 4);
            PrintPersonsInfo(students);

            Console.WriteLine("");

            Teacher teacher = new Teacher();
            Person[] teachers = new Teacher[4];
            teachers[0] = new Teacher("Za", 50);
            teachers[1] = new Teacher("Yb", 22);
            teachers[2] = new Teacher("Xc", 40);
            teachers[3] = new Teacher("Wd", 35);

            teacher.SortPersons(teachers, 4);
            PrintPersonsInfo(teachers);
        }

        public static void PrintPersonsInfo(Person[] persons)
        {
            foreach(var p in persons)
            {
                p.print();
                if(p is Teacher)
                {
                    var t = p as Teacher;
                    Console.WriteLine(t.Name + " is a teacher.");
                    Console.WriteLine(t.ToString());
                    t.PrintClassTypeWithNumberCall();
                    Console.WriteLine("Grumpiness: " + t.GetRandomGrumpiness(1f, 100f) + "\n");
                }
                else
                {
                    var s = p as Student;
                    s.fn();
                    Console.WriteLine(s.Name + " is not a teacher.");
                    //Console.WriteLine("Current StudentType: " + s.GetStudentType());
                    //Console.WriteLine("Select a new StudentType.\n0 - FRESHMAN\n1 - SOPHOMORE\n2 - JUNIOR\n3 - SENIOR");
                    //int newStudentType = 4;//int.Parse(Console.ReadLine());
                    //s.SetStudentType(newStudentType);
                    //Console.WriteLine(p.ToString() + "\nStudentType: " + s.GetStudentType());
                    s.PrintClassTypeWithNumberCall();
                } 
            }
        }
    }
}
