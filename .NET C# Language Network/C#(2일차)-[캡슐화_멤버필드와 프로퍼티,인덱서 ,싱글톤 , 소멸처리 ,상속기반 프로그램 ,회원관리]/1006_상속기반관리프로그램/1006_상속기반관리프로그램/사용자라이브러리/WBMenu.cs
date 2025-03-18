//wbmenu.cs
using System;

namespace _1006_상속기반관리프로그램
{ 
    internal static class WBMenu
    {
        #region 메뉴
        public static ConsoleKey MainMenu()
        {
            Console.WriteLine("************************************************************");
            Console.WriteLine("[F1] 회원관리 기능");
            Console.WriteLine("[F2] 계좌관리 기능");
            Console.WriteLine("[F3] 입출금관리 기능");
            Console.WriteLine("[ESC] exit(프로그램종료)");
            Console.WriteLine("************************************************************");
            return WbLib.InputKey();
        }
        public static ConsoleKey Sub1Menu()
        {
            Console.WriteLine("************************************************************");
            Console.WriteLine("[F1] 회원 저장");
            Console.WriteLine("[F2] 회원 검색");
            Console.WriteLine("[F3] 회원 수정");
            Console.WriteLine("[F4] 회원 삭제");
            Console.WriteLine("[ESC] 이전 메뉴로");
            Console.WriteLine("************************************************************");
            return WbLib.InputKey();
        }
        public static ConsoleKey Sub2Menu()
        {
            Console.WriteLine("************************************************************");
            Console.WriteLine("[F1] 계좌개설");
            Console.WriteLine("[F2] 계좌삭제");
            Console.WriteLine("[F3] 일반계좌리스트출력");
            Console.WriteLine("[F4] 신용계좌리스트출력");
            Console.WriteLine("[F5] 기부계좌리스트출력");
            Console.WriteLine("[ESC] 이전 메뉴로");
            Console.WriteLine("************************************************************");
            return WbLib.InputKey();
        }
        public static ConsoleKey Sub3Menu()
        {
            Console.WriteLine("************************************************************");
            Console.WriteLine("[F1] 계좌 검색(계좌번호로)");
            Console.WriteLine("[F2] 계좌 검색(회원번호로)");
            Console.WriteLine("[F3] 입금 기능");
            Console.WriteLine("[F4] 출금 기능");
            Console.WriteLine("[ESC] 이전 메뉴로");
            Console.WriteLine("************************************************************");
            return WbLib.InputKey();
        }

        #endregion 
    }
}
