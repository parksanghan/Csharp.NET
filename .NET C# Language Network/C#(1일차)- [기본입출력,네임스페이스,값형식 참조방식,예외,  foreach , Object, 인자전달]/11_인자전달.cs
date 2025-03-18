using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
함수의 인자 전달
*/ 
namespace _1004
{
    internal class Class9
    {
        static void Exam2(int n1, ref int n2, out int n3)
        {
            n1 = 11;
            n2 = 22;
            n3 = 33;
        }

        static void Exam1()
        {
            int num1 = 10;
            int num2 = 20;
            int num3;   //어차피 넘어가면 값이 변경됨!

            //값전달,
            //레퍼런스전달(원본의 값을 원하면 변경해 줘),
            //out전달(원본을 반드시 변경해 줘-강제)
            Exam2(num1, ref num2, out num3);

            //10, 22, 33
            Console.WriteLine(num1 + ", " + num2 + ", " + num3);

        }

        static void Main(string[] args)
        {
            Exam1();
        }
    }
}
