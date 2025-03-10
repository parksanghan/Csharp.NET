//Account.cs
using System;

namespace _1005_계좌관리프로그램
{
	internal class Account
	{
		//맴버 필드
		private int id;
		private string name;
		private int balance;

		#region 프로퍼티
		public int Id
		{
			get { return id; }
			private set { id = value; }
		}

		public string Name
		{
			get { return name; }
			private set { name = value; }
		}

		public int Balance
		{
			get { return balance; }
			private set { balance = value; }
		}
		#endregion

		#region 생성자  & 소멸자
		public Account(int _id, string _name, int _balance)
		{
			Id = _id;
			Name = _name;
			Balance = _balance;
		}

		public Account(int _id, string _name)
		{
			Id = _id;
			Name = _name;
			Balance = 0;
		}

		~Account()  //필요 없다면 만들지 말자!
		{

		}

		#endregion

		#region 메서드
		public void AddMoney(int money)
		{
			if (money <= 0)
				throw new Exception("잘못된 금액 : " + money + "원");

			Balance = Balance + money;
		}

		public void MinMoney(int money)
		{
			if (money <= 0)
				throw new Exception("잘못된 금액 : " + money + "원");

			if (money > balance)
				throw new Exception("잔액 부족");

			Balance = Balance - money;
		}

		public void Print()
		{
			Console.Write(Id + ", ");
			Console.Write(Name + ", ");
			Console.WriteLine(Balance);
		}

		public void Println()
        {
			Console.WriteLine("[번호] " + Id);
			Console.WriteLine("[이름] " + Name);
			Console.WriteLine("[잔액] " + Balance + "원");
		}

		#endregion
	}
}
