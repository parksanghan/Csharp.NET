//App.cs
using System;


namespace _1006_상속기반관리프로그램
{
    internal class App
    {
		private Control con = Control.Singleton;

        #region 흐름
        public void Init()
        {
			Logo();
		}
		public void Run()
        {
			while (true)
			{
				WbLib.Clear();

				ConsoleKey key = WBMenu.MainMenu();
				switch (key)
				{
					case ConsoleKey.F1 :	RunSub1(); break;
					case ConsoleKey.F2:		RunSub2(); break;
					case ConsoleKey.F3:		RunSub3(); break;
					case ConsoleKey.Escape: return;
				}
			}
		}
		public void Exit()
        {
			Ending();
		}
        #endregion

        #region 서브 반복 흐름
  
		private void RunSub1()
        {
			while (true)
			{
				WbLib.Clear();
				con.MemberPrintall();
				ConsoleKey  key = WBMenu.Sub1Menu();
				switch (key)
				{
					case ConsoleKey.F1:		con.MemberInsert(); break;
					case ConsoleKey.F2:		con.MemberSelect(); break;
					case ConsoleKey.F3:		con.MemberUpdate(); break;
					case ConsoleKey.F4:		con.MemberErase(); break;
					case ConsoleKey.Escape: return;
				}
				WbLib.Pause();
			}
		}
		private void RunSub2()
        {
			while (true)
			{
				WbLib.Clear();
				con.AccountPrintall();
				ConsoleKey key = WBMenu.Sub2Menu();
				switch (key)
				{
					case ConsoleKey.F1: con.AccountInsert(); break;
					case ConsoleKey.F2: con.AccountErase(); break;
					case ConsoleKey.F3: con.Acc_PrintAll(); break;
					case ConsoleKey.F4: con.Faith_PrintAll(); break;
					case ConsoleKey.F5: con.Contri_PrintAll(); break;
					case ConsoleKey.Escape: return;
				}
				WbLib.Pause();
			}

		}
		private void RunSub3()
        {
			while (true)
			{
				WbLib.Clear();
				con.AccountPrintall();
				ConsoleKey key = WBMenu.Sub3Menu();
				switch (key)
				{
					case ConsoleKey.F1:	con.AccountSelectNumber();		break;
					case ConsoleKey.F2:	con.AccountSelectMemberID();	break;
					case ConsoleKey.F3:	con.InputMoney();				break;
					case ConsoleKey.F4:	con.OutputMoney();				break;
					case ConsoleKey.Escape: return;
				}
				WbLib.Pause();
			}
		}

        #endregion

        #region 로고 및 엔딩
        
		private void Logo()
        {
			WbLib.Clear();
			Console.WriteLine("************************************************************\n");
			Console.WriteLine(" 과정명 : 우송비트고급38기\n");
			Console.WriteLine(" 과목명 : C#언어\n");
			Console.WriteLine(" 주  제 : 계좌관리프로그램\n");
			Console.WriteLine(" 일  자 : 2023-10-06\n");
			Console.WriteLine(" 구  현 : 최창민\n");
			Console.WriteLine("************************************************************\n");
			WbLib.Pause();
		}
		private void Ending()
        {
			WbLib.Clear();
			WbLib.Pause();
		}        
		#endregion 
    }
}
