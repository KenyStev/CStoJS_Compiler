using System;
public class sort{
    
    public static void Main(string[] args)
    {
        int c7 = 'a'+ 1;
        int c = 20 + 30;
        int c1 = ((20 + 30 - 10 * 50) / 5) * 10;
        int c2 = 1 << 0x1 ;
        int c3 = c2 ^ c1;
        int c4 = 1 & c;
        int c5 = 100 % 7;
        int c6 = c4 | c1;
        int c8 = 'b' * 'r';

        Console.WriteLine("Suma: " + c);
        Console.WriteLine("sumatoria complicada: " + c1);
        Console.WriteLine("left shifting: " + c2);
        Console.WriteLine("XOR operator: " + c3);
        Console.WriteLine("AND operator: " + c4);
        Console.WriteLine("MOD operator: " + c5);
        Console.WriteLine("OR operator: " + c6);
        Console.WriteLine("Suma char and int: " + c7);
        Console.WriteLine("Mult chars: " + c8);

        c8 += c5;
        Console.WriteLine("+= " + c8);
        c7 -= c5;
        Console.WriteLine("-= " + c7);
        c6 *= c5;
        Console.WriteLine("*= " + c6);
        c5 /= c5;
        Console.WriteLine("/= " + c5);
        c4 &= c5;
        Console.WriteLine("&= " + c4);
        c3 |= c5;
        Console.WriteLine("|= " + c3);
        c2 ^= c5;
        Console.WriteLine("^= " + c2);
        c1 %= ~c8;
        Console.WriteLine("%= " + c1);


        string s = "a" + 1;
        string s2 = "a" + 1.5;
        Console.WriteLine("Suma string-float " + s2);
        s2 += " fin";

        Console.WriteLine("Suma string-int " + s);
        Console.WriteLine("Suma assign " + s2);

        Console.WriteLine("Using selectionsort ");

        int[] array = { 7,50,20,40,90,6,4 };
        int size = 7;
        
        IntArraySelectionSort(array, size);

        for (int i = 0; i < size; i++) {
            Console.WriteLine(""+array[i]);
        }


        Console.WriteLine("Using HOLAAaaaaaaaaaaa ");
        int[] array2 = { 7,35,22,45,92,11,4 };
        int size2 = 7;
        IntArrayQuickSort(array2, size2);

        for (int i = 0; i < size2; i++) {
            Console.WriteLine(""+array2[i]);
        }
    }

    public static void IntArrayQuickSort(int[] data, int l, int r)
    {
        int i, j;
        int x;

        i = l;
        j = r;

        x = data[(l + r) / 2]; /* find pivot item */
        while (true)
        {
            while (data[i] < x)
                i++;
            while (x < data[j])
                j--;
            if (i <= j)
            {
                exchange(data, i, j);
                i++;
                j--;
            }
            if (i > j)
                break;
        }
        if (l < j)
            IntArrayQuickSort(data, l, j);
        if (i < r)
            IntArrayQuickSort(data, i, r);
    }

    public static void IntArrayQuickSort(int[] data, int size)
    {
        IntArrayQuickSort(data, 0, size - 1);
    }

    public static int IntArrayMin(int[] data, int start, int size)
    {
        int minPos = start;
        for (int pos = start + 1; pos < size; pos++)
            if (data[pos] < data[minPos])
                minPos = pos;
        return minPos;
    }

    public static void IntArraySelectionSort(int[] data, int size)
    {
        int i;
        int N = size;
        Console.WriteLine("Hola");
        int n = int.Parse("5");
        for (i = 0; i < N - 1; i++)
        {
            int k = this.IntArrayMin(data, i, size);
            if (i != k)
                exchange(data, i, k);
        }
    }
    public static void exchange(int[] data, int m, int n)
    {
        int temporary;

        temporary = data[m];
        data[m] = data[n];
        data[n] = temporary;
    }                  
}