using System;
/*
입력
ReadLine() : 가장 많이 사용!
ReadKey()  : 특수키 입력시 사용!
Read() : 문자 전용(잘 사용 안함)
*/

namespace _1004
{
    internal class Class4
    {
        //Read : 문자 전용 읽기
        static void Exam1()
        {
            Console.Write("문자 입력 : ");
            char ch1 = (char)Console.Read();  //A 엔터
            Console.ReadLine(); //엔터 제거!

            Console.Write("문자 입력 : ");
            int ch2 = Console.Read();
            Console.ReadLine(); //엔터 제거!

            Console.WriteLine(ch1 + ", " + (char)ch2);
        }

        //ReadLine : 문자열 전용 읽기 ( 엔터를 누를때 까지 )
        static void Exam2()
        {
            Console.Write("문자열 입력 : ");
            string str1 =  Console.ReadLine();

            Console.Write("문자열 입력 : ");
            string str2 = Console.ReadLine();

            Console.WriteLine(str1 + ", " + str2);
        }

        //Readkey : 문자 제외 특수키(Fun) 읽기
        static void Exam3()
        {
            Console.Write("키 입력 : ");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.F1)
                Console.WriteLine("F1키가 눌림");
        }

        //기본형 타입들을 읽을 때는 문자열로 읽고 parsing해서 획득함
        static void Exam4()
        {          
            Console.Write("이름 : ");
            string name = Console.ReadLine();

            Console.Write("나이 : ");
            string temp = Console.ReadLine();
            int age = int.Parse(temp);          //*****

            Console.Write("몸무게 : ");
            float weight = float.Parse( Console.ReadLine());

            Console.Write("성별 : ");
            char gender = char.Parse(Console.ReadLine());

            Console.WriteLine("\n\n결과확인");
            Console.WriteLine("이름 : " + name);
            Console.WriteLine("나이 : {0}", age);
            Console.WriteLine("몸무게 : {0}", weight);
            Console.WriteLine("성별 : " + gender);
        }

        static void Main(string[] args)
        {
            Exam4();
        }
    }
}
