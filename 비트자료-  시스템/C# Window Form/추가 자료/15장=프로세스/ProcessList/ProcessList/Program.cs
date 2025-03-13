using System;
using System.Diagnostics;

class CSTest
{
	static void Main()
	{
		Process[] Procs = Process.GetProcesses();
		foreach (Process p in Procs)
		{
			Console.WriteLine("ID = {0,5}, 이름 = {1}", p.Id, p.ProcessName);
		}
	}
}
