using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//string 사용법
namespace _1004
{
    internal class Class3
    {
        //string 사용법
        //참조 형식이지만 값 형식처럼 사용 가능!
        static void Exam1()
        {
            char[] arr = { 'a', 'b', 'c', '\0' };

            //참조형식!
            //str1 : 주소 저장 공간
            //new  : 힙 메모리에 값을 저장할 공간 생성(생성된 주소 반환)
            string str1 = new string(arr);
            Console.WriteLine(str1);

            string str2 = "abc";
            Console.WriteLine(str2);
        }

        //대입연산 : 주소가 복사되는게 아니다.
        //값형식처럼 값이 복사된다.
        static void Exam2()
        {
            string str1 = "abc";
            string str2 = "ABBCDEF";
            Console.WriteLine(str1 + "\t" + str2);

            str1 = str2;
            Console.WriteLine(str1 + "\t" + str2);

            str1 = str1 + "문자열 추가";
            Console.WriteLine(str1 + "\t" + str2);
        }

        //비교연산 : 주소를 비교하는게 아니다.
        //값형식처럼 값을 비교한다.
        static void Exam3()
        {
            string str1 = "ABC";
            string str2 = "ABC";
            string str3 = "abc";

            if (str1 == str2)
                Console.WriteLine("동일1");

            if (str1 == str3)
                Console.WriteLine("동일2");
        }

        static void Main(string[] args)
        {
            Exam3();
        }
    }
}
