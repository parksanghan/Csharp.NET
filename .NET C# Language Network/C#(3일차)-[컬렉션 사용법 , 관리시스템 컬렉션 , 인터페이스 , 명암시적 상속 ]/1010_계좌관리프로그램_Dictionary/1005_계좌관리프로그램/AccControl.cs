//AccControl.cs
using System;
using System.Collections.Generic;

namespace _1005_계좌관리프로그램
{
    internal class AccControl
    {
		//Account가 갖고 있는 맴버 중 uniq한 맴버를 key로 설정!
		//KEY : 계좌번호
		//VALUE : 계좌
		Dictionary<int, Account> accounts = new Dictionary<int, Account>();
		//List<Account> accounts = new List<Account>();
		List<AccInfo> accountinfos = new List<AccInfo>();

		#region 생성자와 sample 초기화 처리
		public AccControl()
        {
			Temp_InitData();
		}

		private void Temp_InitData()
        {
			accounts.Add(100, new Account(100, "홍길동", 1000));
			accountinfos.Add(new AccInfo(100, 1000, 0, 1000));

			accounts.Add(101, new Account(101, "이길동", 2000));
			accountinfos.Add(new AccInfo(101, 2000, 0, 2000));

			accounts[102] = new Account(102, "김길동", 3000);
			accountinfos.Add(new AccInfo(102, 3000, 0, 3000));
		}
        #endregion

        #region 메서드
        public void Printall()
        {
			Console.WriteLine("저장개수 : {0}개\n", accounts.Count);

			//foreach (Account acc in accounts.Values)
			foreach (KeyValuePair<int, Account> pair in accounts)
			{
				Account account = pair.Value;
				account.Print();
			}
			Console.WriteLine();
		}

		public void Insert()
        {			
			Console.WriteLine("[계좌 저장]\n");

			try
			{
				int id		= WbLib.InputInteger("계좌번호");
				string name = WbLib.InputString("이름");
				int balance = WbLib.InputInteger("입금액");

				Account acc = new Account(id, name, balance);
				accounts.Add(id, acc);

				AccInfo info = new AccInfo(id, balance, 0, balance);
				accountinfos.Add(info);

				Console.WriteLine("계좌 저장 성공");
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.ToString());
            }
		}

		public void Delete()        
		{
			Console.WriteLine("[계좌 삭제]\n");

			try
			{
				int id = WbLib.InputInteger("계좌번호 입력");				
				accounts.Remove(id);

				Console.WriteLine("계좌 삭제 성공");
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
		}

		public void UpdateInput()
        {
			Console.WriteLine("[입금]\n");

            try
            {
                int id = WbLib.InputInteger("계좌번호");
                int money = WbLib.InputInteger("입금액");

                Account acc = accounts[id];
                acc.AddMoney(money);

                AccInfo info = new AccInfo(id, money, 0, acc.Balance);
                accountinfos.Add(info);

                Console.WriteLine("입금 성공");
            }
			catch(Exception e)
            {
				Console.WriteLine (e.Message);
            }
        }

        public void UpdateOutput()
        {
			Console.WriteLine("[출금]\n");

			try
			{
				int id = WbLib.InputInteger("계좌번호");
				int money = WbLib.InputInteger("출금액");

				Account acc = accounts[id];
				acc.MinMoney(money);

				AccInfo info = new AccInfo(id, 0, money, acc.Balance);
				accountinfos.Add(info);

				Console.WriteLine("출금 성공");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void Select()
        {
			Console.WriteLine("[계좌 검색]\n");

			int id = WbLib.InputInteger("계좌번호 입력");

			Account acc = accounts[id];
			acc.Println();

			//계좌번호에 해당되는 거래 내역 출력!
			Console.WriteLine("-------------------------------------------");
			Console.WriteLine("계좌번호\t입금액\t출금액\t잔액");
			Console.WriteLine("-------------------------------------------");
			for(int i=0; i< accountinfos.Count; i++)
			{
				AccInfo info = accountinfos[i];
				if (info.Id == id)
					info.Print();
			}
			Console.WriteLine("-------------------------------------------");
		}
		#endregion
	}
}
