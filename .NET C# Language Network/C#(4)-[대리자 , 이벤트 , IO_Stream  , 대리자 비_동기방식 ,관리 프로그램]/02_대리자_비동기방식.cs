using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1011_대리자와이벤트
{
    //1. 대리자 정의
    delegate int DemoDele(int s, int b);

    delegate int Demo(int a, int b);
    internal class Class1
    {
        //2. 레퍼런스 변수 선언
        DemoDele dele = null;
        Demo dlele = null;
        //3. 비동기 호출
        public void Exam1()
        {
            //비동기 호출
            dele = Sum;
            dele.BeginInvoke(1, 20, EndSum, "첫번째");
            dele.BeginInvoke(1, 50, EndSum, "두번째");
            dele.BeginInvoke(1, 30, EndSum, "세번째");
            dele.BeginInvoke(1, 100, EndSum, "네번째");

            //자신의 일을 수행
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Main:{0}", i);
                Thread.Sleep(100);
            }

            //종료 전 멈춤
            Console.ReadLine();
        }
 
        //4. 비동기 통지 수신
        public void EndSum(IAsyncResult iar)
        {
            async 
                .EndInvoke(iar);
            object obj = iar.AsyncState; // 문자열을 비동기로  담음
              
            AsyncResult ar = iar as AsyncResult;
            DemoDele del = ar.AsyncDelegate as DemoDele;
            Console.WriteLine("비동기통지 : {0}, {1}",obj, del.EndInvoke(ar));
        }
        //[연산 함수]
        public int Sum(int s, int b)
        {
            int sum = 0;
            for(; s <=b; s++)
            {
                sum = sum + s;
                Console.WriteLine("Sum:{0}", s);
                Thread.Sleep(100);
            }
            return sum;
        }

        static void Main(string[] args)
        {
            //이름이 없는 객체 생성
            //한번만 메서드 호출할 때
            new Class1().Exam1();
        }
    }
}
