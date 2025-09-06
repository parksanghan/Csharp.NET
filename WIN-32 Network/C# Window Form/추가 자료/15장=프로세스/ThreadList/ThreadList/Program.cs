using System;
using System.Diagnostics;

class CSTest
{
	static void Main()
	{
		Process Proc = Process.GetProcessById(432);
		ProcessThreadCollection col = Proc.Threads;
		foreach (ProcessThread T in col)
		{
			Console.WriteLine("ID = {0} 우선순위 = {1}", T.Id, T.PriorityLevel);
		}
	}
}
