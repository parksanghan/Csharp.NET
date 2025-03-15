using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
대리자
1) 대리자 객체 정의[대리자 == 클래스]
   컴파일러에 의해 클래스가 정의 됨
2) 생성
   - new 연산자 활용
   - 대입 연산
3) 사용
   - 동기
     생성된 객체를 함수처럼 호출
     생성된 객체의 맴버함수 invoke 호출
   - 비동기
 */ 
namespace _1011_대리자와이벤트
{
    //1. del정의 -> 클래스화
    delegate int DemoDel(int a, int b); //  대리자 생성 
    delegate int Demo(int a, int b);
    internal class Program
    {
        //2. 레퍼런스 변수 선언
        private DemoDel del = null;
        private Demo dele= null;
        //3.1 동기방식 사용
        public void Exam1()
        {
            //FM 방식
            del = new DemoDel(Add);
            Console.WriteLine("덧셈 : " +  del.Invoke(10, 20));
            dele = new Demo(Add);

            //편리성 제공 방식
            del = Sub;
            Console.WriteLine("뺄셈 : " + del(10, 20));

            dele = Sub;
        }

        //3.2 여러개 등록 가능
        public void Exam2()
        {
            del = Add;
            del = del + Sub;
            Console.WriteLine("결과 : " +  del(10, 20));  //-10

            del = del - Sub;
            Console.WriteLine("결과 : " + del(10, 20));  //30
        }


        public int Add(int n1, int n2) { Console.WriteLine("Add");  return n1 + n2; }
        public int Sub(int n1, int n2) { Console.WriteLine("Sub"); return n1 - n2;  }  

        static void Main(string[] args)
        {
            //이름이 없는 객체 생성
            //한번만 메서드 호출할 때
            new Program().Exam2();
        }
    }
}
