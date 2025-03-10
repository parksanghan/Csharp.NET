using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1011_대리자와이벤트
{
    #region Del 정의

    delegate void CalDel(int result); // 대리자 정의 
    delegate void MessageDel(string msg); // 대리자 정의

    #endregion

    internal class Cal
    {
        public CalDel _CalDel { get; set; } = null;   // 대리자 초기화 
        public MessageDel _MessageDel { get; set; } = null; // 대리자 초기화 

        #region 메서드
        public void Add(int n1, int n2)
        {
            int result = n1 + n2;
            _CalDel.Invoke(result); //_CalDel 에 등록된 콜백 함수 동기로 호출
        }

        public void Sub(int n1, int n2)
        {
            int result = n1 - n2;
            _CalDel.Invoke(result);//_CalDel 에 등록된 콜백 함수 동기로 호출
        }

        public void Message(string msg)
        {
            _MessageDel(msg); // _MessageDel에 등록된 콜백함수 동기로 호출 
        }
        #endregion

    }

    //사용자 
    internal class Program
    {
        private Cal cal = new Cal();

        public void Run()
        {
            cal._CalDel     = GetCal;   // 대리자 콜백 함수 추가 
            cal._MessageDel = GetMessag; // 대리자 콜백 함수 추가 

            cal.Add(10, 20);
            //Thread.Sleep(2000);

            cal.Sub(10, 20);
            //Thread.Sleep(2000);

            cal.Message("안녕하세요");
        }

        #region CallBack 메서드
        public void GetCal(int result)
        {
            Console.WriteLine("연산결과 : {0}", result);
        }
        public void GetMessag(string msg)
        {
            Console.WriteLine("Echo : {0}", msg);
        }
        #endregion 



        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
