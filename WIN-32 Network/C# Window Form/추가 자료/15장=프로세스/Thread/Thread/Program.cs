using System;
using System.Threading;

class CSTest
{
	// 작업 스레드
	static void ThreadProc()
	{
		for (int i = 0; i < 10; i++)
		{
			Console.WriteLine(i);
			Thread.Sleep(500);
		}
		Console.WriteLine("작업 스레드 종료");
	}
	// 주 스레드
	static void Main()
	{
		Thread T = new Thread(new ThreadStart(ThreadProc));
		T.Start();
		for (; ; )
		{
			ConsoleKeyInfo cki;
			cki = Console.ReadKey();
			if (cki.Key == ConsoleKey.A)
			{
				Console.Beep();
			}
			if (cki.Key == ConsoleKey.B)
			{
				break;
			}
		}
		Console.WriteLine("주 스레드 종료");
	}
}
