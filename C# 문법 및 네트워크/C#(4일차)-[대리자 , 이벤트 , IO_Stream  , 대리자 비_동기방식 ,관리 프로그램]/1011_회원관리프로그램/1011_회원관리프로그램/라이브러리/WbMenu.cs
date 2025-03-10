//WbMenu.cs
using System;

namespace _1011_회원관리프로그램
{ 
    internal static class WbMenu
    {
        #region 메뉴
        public static ConsoleKey MainMenu()
        {
            Console.WriteLine("************************************************************");
            Console.WriteLine("[F1] 회원 저장");
            Console.WriteLine("[F2] 회원 검색");
            Console.WriteLine("[F3] 회원 수정");
            Console.WriteLine("[F4] 회원 삭제");
            Console.WriteLine("[ESC] 프로그램 종료");
            Console.WriteLine("************************************************************");

            return WbLib.InputKey();
        }
       
        #endregion 
    }
}
