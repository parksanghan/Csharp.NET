using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1004
{
    internal class Class6
    {
        static void Exam1()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6 };

            for (int i = 0; i < arr.Length; i++) //요소의 개수를 가지고 있다.
            {
                Console.Write(arr[i] + "\t");
            }
            Console.WriteLine();

            for (int i = 1; i < arr.Length-1; i++) //요소의 개수를 가지고 있다.
            {
                Console.Write(arr[i] + "\t");
            }
            Console.WriteLine();


            foreach(int value in arr)
            {
                Console.Write(value + "\t");
            }
            Console.WriteLine();

        }

        static void Main(string[] args)
        {
            Exam1();
        }
    }
}
