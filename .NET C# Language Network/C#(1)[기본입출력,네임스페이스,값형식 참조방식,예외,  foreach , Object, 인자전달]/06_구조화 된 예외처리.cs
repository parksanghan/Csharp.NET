using System;
/*
[구조화된 예외처리]
*/ 

namespace _1004
{
    internal class Class5
    {
        //정수 입력 과정에서 문자를 입력한다면?
        //int.Parse 함수내에서 예외를 던짐!!!
        //System.FormatException: 입력 문자열의 형식이 잘못되었습니다
        static void Exam1()
        {
            try
            {
                Console.Write("정수 입력 : ");
                int num = int.Parse(Console.ReadLine());
                Console.WriteLine(num);

                throw new Exception("다른 예외...");
            }
            catch(FormatException e)    //특정 예외 수신
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)          //모든 예외를 받을 수 있다.
            {   
                Console.WriteLine(e.Message);
            }
        }

         static void Main(string[] args)
        {
            Exam1();
        }
    }
}
