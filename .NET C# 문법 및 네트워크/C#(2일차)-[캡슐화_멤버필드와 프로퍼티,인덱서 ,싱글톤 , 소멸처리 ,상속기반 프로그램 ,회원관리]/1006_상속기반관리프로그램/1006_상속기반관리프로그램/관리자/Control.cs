//control.cs
using System;


namespace _1006_상속기반관리프로그램
{
    internal class Control
	{
		#region 싱글톤
		public static Control Singleton { get; private set; }

		static Control()
		{
			Singleton = new Control();
		}
		private Control()
		{
		}
		#endregion

		private MemberControl membercon		= MemberControl.Singleton;
		private AccountControl accountcon = AccountControl.Singleton;

		#region Member관련 기능
		public void MemberInsert()
        {
			membercon.Insert();
		}

		public void MemberSelect()
        {
			membercon.Select();

		}
		public void MemberUpdate()
        {
			membercon.Update();

		}
		public void MemberErase()
        {
			membercon.Erase();

		}
		public void MemberPrintall()
        {
			membercon.PrintAll();

		}
        #endregion

		# region Account관련 기능
        
		public void AccountInsert()
        {
			accountcon.Insert();

		}

		public void AccountErase()
        {
			accountcon.Erase();

		}		
		public void AccountPrintall()
        {
			accountcon.AccountPrintAll();
		}

		public void Acc_PrintAll()
		{
			accountcon.Acc_printall();
		}

		public void Faith_PrintAll()
        {
			accountcon.Faith_printall();
        }

		public void Contri_PrintAll()
		{
			accountcon.Contri_printall();
		}


		#endregion

		#region AccountIO관련 기능
		public void AccountSelectNumber()
		{
			accountcon.AccountSelectNumber();
		}

		public void AccountSelectMemberID()
		{
			accountcon.AccountSelectMemberID();
		}

		public void InputMoney()
		{
			accountcon.InputMoney();
		}

		public void OutputMoney()
		{
			accountcon.OutputMoney();
		}

		#endregion
	}
}
