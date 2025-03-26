//App.cs
using System;

namespace _1005_계좌관리프로그램
{
    internal class App : IDisposable
    {
		private	AccControl con = new AccControl();

        #region 생성자 & 소멸자
        public App()
        {
            Logo();
        }

        ~App()
        {
            Dispose();
        }

        public void Dispose()
        {
            Ending();
            GC.SuppressFinalize(this);
        }
        #endregion 


        public void Run()
        {
            while (true)
            {
                WbLib.Clear();
                con.Printall();
                MenuPrint();                
                switch (WbLib.InputKey())
                {
                    case ConsoleKey.Escape:                 return; //함수 종료!
                    case ConsoleKey.F1: con.Insert();       break;
                    case ConsoleKey.F2: con.Delete();       break;
                    case ConsoleKey.F3: con.UpdateInput();  break;
                    case ConsoleKey.F4: con.UpdateOutput(); break;
                    case ConsoleKey.F5: con.Select();       break;
                    default: Console.WriteLine("없는 메뉴 입니다.");   break;
                }
                WbLib.Pause();
            }
        }

		private void Logo()
        {
            WbLib.Clear();
            Console.WriteLine("******************************************************");
            Console.WriteLine(" 우송비트 고급38기");
            Console.WriteLine(" [C#] 배열 이용한 계좌 관리 프로그램");
            Console.WriteLine(" 홍길동");
            Console.WriteLine(" 2023-10-05");
            Console.WriteLine("******************************************************");
            WbLib.Pause();
        }
		private void MenuPrint()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine(" [ESC] 프로그램 종료");
            Console.WriteLine(" [F1 ] 저장(insert)");
            Console.WriteLine(" [F2 ] 삭제(delete)");
            Console.WriteLine(" [F3 ] 수정_입금(update)");
            Console.WriteLine(" [F4 ] 수정_출금(update)");
            Console.WriteLine(" [F5 ] 검색(select)");
            Console.WriteLine("******************************************************");
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
