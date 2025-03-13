using System;
using System.Threading;
using System.Diagnostics;

class CSTest
{
	static void Main()
	{
		Process Proc = Process.Start("notepad.exe");
		Thread.Sleep(5000);
		Proc.Kill();
	}
}
