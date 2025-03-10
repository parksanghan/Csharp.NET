//WbMenu.cs
using System;

namespace 서버
{ 
    internal static class WbMenu
    {
        #region 메뉴
        public static ConsoleKey MainMenu()
        {
            Console.WriteLine("************************************************************");
            Console.WriteLine("[F1] 음료수 저장");
            Console.WriteLine("[F2] 음료수 검색");
            Console.WriteLine("[F3] 음료수 가격 변경");
            Console.WriteLine("[F4] 음료수 삭제");
            Console.WriteLine("[ESC] 프로그램 종료");
            Console.WriteLine("************************************************************");

            return WbLib.InputKey();
        }
       
        #endregion 
    }
}
