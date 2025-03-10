//App.cs
using System;

namespace _1010_실습관리프로그램
{
    internal class App : IDisposable
    {
		private	Control con = new Control();

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
                con.Student_Printall();
                con.Subject_Printall();         
                switch (WbMenu.MainMenu())
                {
                    case ConsoleKey.Escape:                     return; //함수 종료!
                    case ConsoleKey.F1: con.Student_Insert();   break;
                    case ConsoleKey.F2: con.Student_Select();   break;
                    case ConsoleKey.F3: con.Student_Update();   break;
                    case ConsoleKey.F4: con.Student_Delete();   break;

                    case ConsoleKey.F5: con.Subject_Insert();   break;
                    case ConsoleKey.F6: con.Subject_Delete();   break;

                    case ConsoleKey.F7: con.Grade_Insert();     break;
                    case ConsoleKey.F8: con.Grade_Delete();     break;
                    case ConsoleKey.F9: con.Grade_Select();     break;
                    case ConsoleKey.F10: con.Grade_Select1();   break;
                    case ConsoleKey.F11: con.Grade_Update();    break;
                    case ConsoleKey.F12: con.Grade_PrintAll();  break;

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
            Console.WriteLine(" [C#] 컬렉션 클래스를 활용한 성적 관리 프로그램");
            Console.WriteLine(" 홍길동");
            Console.WriteLine(" 2023-10-10");
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
