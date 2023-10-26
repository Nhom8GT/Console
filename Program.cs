using System;
using System.Text;

class Program
{
    static void PrintArray(int[] a, int n)
    {
        for (int i = 0; i < n; i++)
        {
            Console.Write(a[i] + " ");
        }
    }

    static void InsertionSort(int[] a, int n)
    {
        int index, value;
        for (int i = 1; i < n; i++)
        {
            index = i;
            value = a[i];
            while (index > 0 && a[index - 1] > value)
            {
                a[index] = a[index - 1];
                index--;
            }
            a[index] = value;
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        int[] a = { 5, 2, 4, 1, 3 }; // Dãy a ban đầu
        int n = a.Length;

        InsertionSort(a, n); // Sắp xếp dãy a
        Console.WriteLine("Dãy a trước khi sắp xếp: 5, 2, 4, 1, 3 ");
        Console.WriteLine("Dãy a sau khi sắp xếp:");
        PrintArray(a, n);
        Console.ReadKey();
    }
}