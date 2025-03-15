using System;
using System.Threading;

namespace _1012_Server
{
    internal class Program
    {
        #region 스레드 함수 
        //ThreadStart
        void ThreadFun1()
        {
            for(int i=0; i<100; i++)
            {
                Console.Write(i + "\t");
                Thread.Sleep(100);
            }
            Console.WriteLine();
        }

        //ParameterizedThreadStart
        void ThreadFun2(object obj)
        {
            int num = (int)obj; 
            for(int i=0; i<num; i++)
            {
                Console.Write(i + "\t");
            }
            Console.WriteLine();
        }
        #endregion

        //가장 기본적인 스레드 생성, Foreground Thread 속성을 가진다.
        //Main Thread 종료 여부와 관계없이 해당 Thread는 계속 동작한다.
        //사용되는 대리자 : ThreadStart --> void .. (void);
        void exam1()
        {
            Thread th = new Thread(new ThreadStart(ThreadFun1));
            //th.IsBackground = true; 
            th.Start();
        }

        //사용되는 대리자 : ParameterizedThreadStart  --> void .. (object );
        void exam2()
        {
            Thread th = new Thread(new ParameterizedThreadStart(ThreadFun2));
            //th.IsBackground = true; 
            th.Start(100);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.exam2();
        }
    }
}
