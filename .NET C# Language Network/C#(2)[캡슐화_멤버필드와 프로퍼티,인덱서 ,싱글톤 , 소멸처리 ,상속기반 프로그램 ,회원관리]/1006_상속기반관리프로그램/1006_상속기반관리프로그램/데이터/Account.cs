//Account.cs
using System;

namespace _1006_상속기반관리프로그램
{
	internal class Account
	{
        #region 속성
        public int AccNumber	{ get; private set; }
		public int MemNumber	{get; private set; }
		public int Balance		{ get; private set; }
		public DateTime Date { get; private set; }
		
		static int s_num = 1000;
        #endregion

        #region 생성자 
        public Account(int mem_num)
        {
			AccNumber	= s_num;
			MemNumber	= mem_num;
			Balance		= 0;
			Date		= DateTime.Now;

			s_num = s_num + 10;
		}

		public Account(int mem_num, int balance)
        {
			AccNumber = s_num;
			MemNumber = mem_num;
			Balance = balance;
			Date = DateTime.Now;

			s_num = s_num + 10;
		}

        #endregion

        #region 메서드 
        public virtual void InputMoney(int money)
        {
			if (money <= 0)
				throw new Exception("잘못된 금액");

			Balance = Balance + money;			
		}

		public void OutputMoney(int money)
        {
			if (money <= 0)
				throw new Exception("잘못된 금액");

			if (Balance < money)
				throw new Exception("잔액 부족");

			Balance = Balance - money;
		}

		public virtual void Print()
        {
			Console.Write(AccNumber + "\t");
			Console.Write(MemNumber + "\t");
			Console.Write(Balance + "원\t");
			Console.Write(Date.ToString() + "\t");
			Console.Write(Date.ToShortDateString() + "\t");
			Console.Write(Date.ToShortTimeString() + "\t");
		}
		public virtual void Println()
        {
			Console.WriteLine("[계좌번호] " + AccNumber);
			Console.WriteLine("[회원번호] " + MemNumber);
			Console.WriteLine("[잔    액] " + Balance + "원");
			Console.WriteLine("[일    시] " + Date.ToString());
			Console.WriteLine("[날    짜] " + Date.ToShortDateString());
			Console.WriteLine("[시    간] " + Date.ToShortTimeString());
		}
        #endregion 
    }
}
