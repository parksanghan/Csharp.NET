using System;
using System.Threading;

class Site
{
	public string name;
	public Site(string aname) { name = aname; }
}

class CSTest
{
	private static Site site = new Site("www.winapi.co.kr");
	static void ThreadProc()
	{
		for (int i = 0; i <= 100; i += 10)
		{
			Console.SetCursorPosition(0, 0);
			Console.WriteLine("{0} 에서 {1}% 다운로드 중", site.name, i);
			Thread.Sleep(1000);
		}
	}
	static void DoSomething()
	{
		string old = site.name;
		site.name = "www.loseapi.co.kr";
		for (int i = 0; i <= 100; i += 10)
		{
			Console.SetCursorPosition(0, 1);
			Console.WriteLine("{0} 에서 {1}% 다운로드 중", site.name, i);
			Thread.Sleep(500);
		}
		site.name = old;
	}
	static void Main()
	{
		Thread T = new Thread(new ThreadStart(ThreadProc));
		T.Start();
		Thread.Sleep(2000);
		DoSomething();
	}
}
