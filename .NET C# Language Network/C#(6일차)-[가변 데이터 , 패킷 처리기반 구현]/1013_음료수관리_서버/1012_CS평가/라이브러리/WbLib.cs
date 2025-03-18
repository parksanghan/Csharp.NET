//WbLib.cs
using System;

namespace 서버
{
    internal static class WbLib
    {
        #region 입력 기능
        public static int InputInteger(string msg)
        {
            Console.Write(msg + " >> ");
            return int.Parse(Console.ReadLine());
        }

        public static float InputFloat(string msg)
        {
            Console.Write(msg + " >> ");
            return float.Parse(Console.ReadLine());
        }

        public static char InputChar(string msg)
        {
            Console.Write(msg + " >> ");
            return char.Parse(Console.ReadLine());
        }

        public static string InputString(string msg)
        {
            Console.Write(msg + " >> ");
            return Console.ReadLine();
        }

        public static ConsoleKey InputKey()
        {
            return Console.ReadKey().Key;
        }

        #endregion

        #region 콘솔 화면 제어
        public static void Clear()
        {
            Console.Clear();
        }

        public static void Pause()
        {
            Console.WriteLine("\n\n엔터키를 누르세요..........");
            Console.ReadLine();
        }

        #endregion 
    }
}
