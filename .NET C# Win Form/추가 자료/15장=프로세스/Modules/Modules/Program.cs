using System;
using System.Diagnostics;

class CSTest
{
	static void Main()
	{
		Process Proc = Process.GetProcessById(432);
		ProcessModuleCollection col = Proc.Modules;
		foreach (ProcessModule M in col)
		{
			Console.WriteLine("이름 = {0}", M.ModuleName);
		}
	}
}
