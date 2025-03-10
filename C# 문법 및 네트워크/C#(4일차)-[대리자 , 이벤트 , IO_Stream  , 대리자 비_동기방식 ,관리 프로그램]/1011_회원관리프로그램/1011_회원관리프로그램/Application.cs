//Application.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1011_회원관리프로그램
{
    internal class Application : IDisposable
    {
        private MemberManager mm = MemberManager.Singleton;
        private MemberViewer viewer = new MemberViewer();

        #region 싱글톤 패턴

        public static Application Singleton { get; private set; }
        static Application()
        {
            Singleton = new Application();
        }
        private Application()
        {
            Logo();
        }

        #endregion


        #region 소멸자(생성자는 싱글톤 패턴 내부로)

        ~Application()
        {
            Dispose();
        }

        public void Dispose()
        {
            mm.Dispose();   //***********************
            Ending();
            GC.SuppressFinalize(this);
        }
        #endregion 
        
        public void Run()
        {
            while (true)
            {
                WbLib.Clear();
                mm.PrintMember();
                switch (WbMenu.MainMenu())
                {
                    case ConsoleKey.Escape: return; //함수 종료!
                    case ConsoleKey.F1: mm.AddMember();         break;
                    case ConsoleKey.F2: mm.SelectMember();      break;
                    case ConsoleKey.F3: mm.UpdateMember();      break;
                    case ConsoleKey.F4: mm.DeleteMember();      break;                    

                    default: Console.WriteLine("없는 메뉴 입니다."); break;
                }
                WbLib.Pause();
            }
        }

        private void Logo()
        {
            WbLib.Clear();
            Console.WriteLine("******************************************************");
            Console.WriteLine(" 우송비트 고급38기");
            Console.WriteLine(" [C#] 컬렉션 클래스를 활용한 회원 관리 프로그램");
            Console.WriteLine(" 홍길동");
            Console.WriteLine(" 2023-10-11");
            Console.WriteLine("******************************************************");
            WbLib.Pause();
        }

        private void Ending()
        {
            WbLib.Clear();
            Console.WriteLine("******************************************************");
            Console.WriteLine(" 프로그램을 종료합니다.");
            Console.WriteLine("******************************************************");
            WbLib.Pause();
        }
    }
}
