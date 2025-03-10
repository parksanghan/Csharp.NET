using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
static : 정적 맴버
         1) 객체 없이 클래스 명으로 사용 가능한 맴버.
         2) 정적 맴버 함수는 정적 맴버만 사용 가능.
*/ 

namespace _1004
{
    internal class Program
    {
        //출력객체(Write, 18개의 오버로딩)
        static void Exam1()
        {
            Console.Write(10);   Console.Write('\n');
            Console.Write('A');   Console.Write('\n');
            Console.Write('한');     //문자 : 한글도 가능(유니코드)
            Console.Write("문자열"); Console.Write('\n');

            //문자열 + 기본형타입 ==> 기본형타입을 문자열로 변환하여 연산
            int num = 10;
            char ch = 'A';
            Console.Write("문자열 : " + num + ch); Console.Write('\n');            
        }

        //출력객체(WriteLine, 19개의 오버로딩, 개행처리)
        static void Exam2()
        {
            Console.WriteLine(10); 
            Console.WriteLine('A'); 
            Console.WriteLine('한');     //문자 : 한글도 가능(유니코드)
            Console.WriteLine("문자열"); 

            //문자열 + 기본형타입 ==> 기본형타입을 문자열로 변환하여 연산
            int num = 10;
            char ch = 'A';
            Console.WriteLine("문자열 : " + num + ch);

            Console.WriteLine(num + num + "");  //20
            Console.WriteLine("" + num + num);  //1010
        }

        //변수 출력
        static void Exam3()
        {
            int age = 10;
            char gender = '남';
            string name = "홍길동";    //문자열 객체 제공
            float weight = 20.23f;

            //1) + 연산 활용
            Console.WriteLine("나이 : " + age);
            Console.WriteLine("성별 : " + gender);
            Console.WriteLine("이름 : " + name);
            Console.WriteLine("몸무게 : " + weight);

            //2) 인덱스 활용
            Console.WriteLine("나이 : {0}\n성별 : {1}", age, gender);
            Console.WriteLine("이름 : {0}", name);
            Console.WriteLine("몸무게 : {0}", weight);
        }

        static void Main(string[] args)
        {
            Exam3();
        }
    }
}
