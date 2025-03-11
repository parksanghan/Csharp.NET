using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1025_계좌실습
{
    internal class Account
	{
		#region 프로퍼티
		public int Id		{	get; private set; }
		public string Name	{ get; private set; }

		public int Balance { get; private set; }

		#endregion

		#region 생성자
		public Account(int _id, string _name, int _balance)
		{
			Id		= _id;
			Name	= _name;
			Balance = _balance;
		}



		#endregion


		#region 메서드
		//public void AddMoney(int money)
		//{
		//	if (money <= 0)
		//		throw new Exception("잘못된 금액 : " + money + "원");

		//	Balance = Balance + money;
		//}

		//public void MinMoney(int money)
		//{
		//	if (money <= 0)
		//		throw new Exception("잘못된 금액 : " + money + "원");

		//	if (money > balance)
		//		throw new Exception("잔액 부족");

		//	Balance = Balance - money;
		//}


		#endregion
	}
}
