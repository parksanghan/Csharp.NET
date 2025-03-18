//App.cs
using System;

namespace _1012_CS평가
{
    internal class Application : IDisposable
    {
        private DrinkControl con = DrinkControl.Instance;

        #region 생성자 & 소멸자
        public Application()
        {
            Logo();
        }

        ~Application()
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
                con.Drink_PrintAll();                       
                switch (WbMenu.MainMenu())
                {
                    case ConsoleKey.Escape:                     return; //함수 종료!
                    case ConsoleKey.F1: con.Drink_Insert();   break;
                    case ConsoleKey.F2: con.Drink_Select();   break;
                    case ConsoleKey.F3: con.Drink_Update();   break;
                    case ConsoleKey.F4: con.Drink_Delete();   break;
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
            Console.WriteLine(" [C# 평가] 컬렉션 클래스를 활용한 음료수 관리 프로그램");
            Console.WriteLine(" 홍길동");
            Console.WriteLine(" 2023-10-12");
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
