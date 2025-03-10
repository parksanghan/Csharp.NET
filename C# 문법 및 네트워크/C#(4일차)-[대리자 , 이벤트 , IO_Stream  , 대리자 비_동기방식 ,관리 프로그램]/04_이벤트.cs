using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//이벤트 처리
namespace _1011_대리자와이벤트
{
    // 범용 이벤트 ARg 정의 , 대리자 정의 
    #region 범용[이벤트Arg 정의, 대리자 정의]
    public class CalEventArgs : EventArgs
    {
        public int  Num1    { get; private set; }
        public int  Num2    { get; private set; }
        public char Oper    { get; private set; }
        public int  Result { get; private set;  }

        public CalEventArgs(int num1, int num2, char oper, int result)
        {
            Num1 = num1;
            Num2 = num2;
            Oper = oper;
            Result = result;
        }
    }

    public class MsgEventArgs : EventArgs
    {
        public string Msg { get; private set; }
        public MsgEventArgs(string msg)
        {
            Msg = msg;
        }
    }

    public delegate void CalEvent(object obj, CalEventArgs e); // 대리자 정의 (인자 1 : 오브잭트  ,CalEventArgs 타입)
    public delegate void MsgEvent(object obj, MsgEventArgs e); // 대리자 정의 

    #endregion

    //게시자
    internal class Cal
    {
        #region 이벤트 선언(생성)

        public event CalEvent CalEventHandler = null;
        public event MsgEvent MsgEventHandler = null;

        #endregion 


        #region 메서드
        public void Add(int n1, int n2)
        {
            int result = n1 + n2;

            //이벤트 발생
            if (CalEventHandler != null)
            {
                CalEventHandler(this,new CalEventArgs(n1, n2, '+', result ));
            }
        }

        public void Sub(int n1, int n2)
        {
            int result = n1 - n2;
            
            //이벤트 발생
            if (CalEventHandler != null)
            {
                CalEventHandler(this, new CalEventArgs(n1, n2, '-', result));
            }
        }

        public void Message(string msg)
        {
            //이벤트 발생
            if (MsgEventHandler != null)
            {
                MsgEventHandler(this, new MsgEventArgs(msg));
            }
        }
        #endregion

    }

    //구독자
    internal class Program
    {
        private Cal cal = new Cal();

        public void Run()
        {
            //이벤트 등록
            cal.CalEventHandler += new CalEvent(Getcal); // cal 클래스안에 애초에 있음 add가 우리가필요한건 콜백함수  !
            cal.CalEventHandler += new CalEvent(GetCal);
            cal.MsgEventHandler += new MsgEvent(GetMessag);

            cal.Add(10, 20);
            //Thread.Sleep(2000);

            cal.Sub(10, 20);
            //Thread.Sleep(2000);

            cal.Message("안녕하세요");
        }

        #region CallBack 메서드(이벤트 핸들러 함수 : 대리자 형식과 일치)

        public void GetCal(object obj, CalEventArgs e)
        {
            Console.WriteLine("연산결과 통지");
            Console.WriteLine("{0} {1} {2} = {3}", e.Num1, e.Oper, e.Num2, e.Result);
        }

        public void GetMessag(object obj, MsgEventArgs e)
        {
            Console.WriteLine("Echo메시지 통지");
            Console.WriteLine("{0}", e.Msg);
        }
        #endregion 



        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
