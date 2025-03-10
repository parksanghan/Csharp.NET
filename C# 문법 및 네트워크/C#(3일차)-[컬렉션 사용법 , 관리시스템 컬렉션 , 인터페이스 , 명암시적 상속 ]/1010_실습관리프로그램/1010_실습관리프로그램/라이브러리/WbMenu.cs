//WbMenu.cs
using System;

namespace _1010_실습관리프로그램
{ 
    internal static class WbMenu
    {
        #region 메뉴
        public static ConsoleKey MainMenu()
        {
            Console.WriteLine("************************************************************");
            Console.Write("[F1] 학생저장" + "\t");
            Console.Write("[F2] 학생검색" + "\t");
            Console.Write("[F3] 학생정보 수정(학과명 변경)" + "\t");
            Console.WriteLine("[F4] 학생정보 삭제\n");

            Console.Write("[F5] 과목저장" + "\t");
            Console.WriteLine("[F6] 과목삭제\n");

            Console.WriteLine("[F7] 성적 등록" + "\t");
            Console.WriteLine("[F8] 성적 삭제(학생번호와 과목번호를 입력받아 해당 성적 삭제)");
            Console.WriteLine("[F9] 성적 검색(학생번호와 과목번호를 입력받아 검색 결과 출력)");
            Console.WriteLine("[F10] 성적 검색(성적 검색(학생번호를 입력받아 해당 학생의 모든 과목 성적 출력)");
            Console.WriteLine("[F11] 성적 수정(학생번호와 과목번호를 입력받아 해당 과목의 성적 수정");
            Console.WriteLine("[F12] 성적 전체 리스트 출력\n");

            Console.WriteLine("[ESC] 프로그램 종료");
            Console.WriteLine("************************************************************");

            return WbLib.InputKey();
        }
       
        #endregion 
    }
}
