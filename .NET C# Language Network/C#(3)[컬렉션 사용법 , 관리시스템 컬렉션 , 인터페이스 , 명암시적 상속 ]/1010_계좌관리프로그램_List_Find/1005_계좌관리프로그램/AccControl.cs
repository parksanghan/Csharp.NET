//AccControl.cs
using System;
using System.Collections.Generic;

namespace _1005_계좌관리프로그램
{
    internal class AccControl
    {
		//private WBArray accounts;     //계좌 리스트
		//private WBArray accountinfos; //거래 내역(저장-계좌생성,입금,출금, 출력-검색)
		List<Account> accounts = new List<Account>();
		List<AccInfo> accountinfos = new List<AccInfo>();

		#region 생성자와 sample 초기화 처리
		public AccControl()
        {
			Temp_InitData();
		}

		private void Temp_InitData()
        {
			accounts.Add(new Account(100, "홍길동", 1000));
			accountinfos.Add(new AccInfo(100, 1000, 0, 1000));

			accounts.Add(new Account(101, "이길동", 2000));
			accountinfos.Add(new AccInfo(101, 2000, 0, 2000));

			accounts.Add(new Account(102, "김길동", 3000));
			accountinfos.Add(new AccInfo(102, 3000, 0, 3000));
		}
        #endregion

        #region 메서드
        public void Printall()
        {
			Console.WriteLine("저장개수 : {0}개\n", accounts.Count);

			for(int i = 0; i < accounts.Count; i++)
			{
				Account acc = accounts[i];
				Console.Write("[{0,2}] ", i);
				acc.Print();
			}
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
				accounts.Add(acc);

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

				//반환된 객체가 null이라면?
				Account account = accounts.Find(acc => acc.Id == id);
				accounts.Remove(account);

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

				Account account = accounts.Find(acc => acc.Id == id);
				account.AddMoney(money);

                AccInfo info = new AccInfo(id, money, 0, account.Balance);
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

				Account account = accounts.Find(acc => acc.Id == id);
				account.MinMoney(money);

				AccInfo info = new AccInfo(id, 0, money, account.Balance);
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

			Account account = accounts.Find(acc => acc.Id == id);
			account.Println();

			//계좌번호에 해당되는 거래 내역 출력!
			List<AccInfo> find_p = accountinfos.FindAll(n => n.Id == id);
			Console.WriteLine("-------------------------------------------");
			Console.WriteLine("계좌번호\t입금액\t출금액\t잔액");
			Console.WriteLine("-------------------------------------------");
			foreach(AccInfo ainfo in find_p)
			{
				ainfo.Print();
			}
			Console.WriteLine("-------------------------------------------");
		}
		#endregion
	}
}
