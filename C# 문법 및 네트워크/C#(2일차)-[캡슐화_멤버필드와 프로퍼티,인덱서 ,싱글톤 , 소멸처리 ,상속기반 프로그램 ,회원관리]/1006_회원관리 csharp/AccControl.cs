//AccControl.cs
using System;


namespace _1005_계좌관리프로그램
{
    internal class AccControl
    {
		private WBArray accounts;     //계좌 리스트
		private WBArray accountinfos; //거래 내역(저장-계좌생성,입금,출금, 출력-검색)

        #region 생성자와 sample 초기화 처리
        public AccControl()
        {
			int max = WbLib.InputInteger("계좌 저장 개수");
			accounts = new WBArray(max);
			accountinfos = new WBArray(100);

			Temp_InitData();
		}

		private void Temp_InitData()
        {
			accounts.PushBack(new Account(100, "홍길동", 1000));
			accountinfos.PushBack(new AccInfo(100, 1000, 0, 1000));

			accounts.PushBack(new Account(101, "이길동", 2000));
			accountinfos.PushBack(new AccInfo(101, 2000, 0, 2000));

			accounts.PushBack(new Account(102, "김길동", 3000));
			accountinfos.PushBack(new AccInfo(102, 3000, 0, 3000));
		}
        #endregion

        #region 메서드
        public void Printall()
        {
			Console.WriteLine("저장개수 : {0}개\n", accounts.Size);

			for(int i = 0; i < accounts.Size; i++)
			{
				Account acc = (Account)accounts[i];
				Console.Write("[{0,2}] ", i);
				acc.Print();
			}
		}

		public void Insert()
        {			
			Console.WriteLine("[계좌 저장]\n");

			try
			{
				int id = WbLib.InputInteger("계좌번호");
				string name = WbLib.InputString("이름");
				int balance = WbLib.InputInteger("입금액");

				Account acc = new Account(id, name, balance);
				accounts.PushBack(acc);

				AccInfo info = new AccInfo(id, balance, 0, balance);
				accountinfos.PushBack(info);

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

				int idx = ValueToIdx(id);
				accounts.Erase(idx);

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

                int idx = ValueToIdx(id);
                Account acc = (Account)accounts[idx];
                acc.AddMoney(money);

                AccInfo info = new AccInfo(id, money, 0, acc.Balance);
                accountinfos.PushBack(info);

                Console.WriteLine("입금 성공");
            }
			catch(Exception e)
            {
				Console.WriteLine (e.ToString());
            }
        }

        public void UpdateOutput()
        {
			Console.WriteLine("[출금]\n");

			try
			{
				int id = WbLib.InputInteger("계좌번호");
				int money = WbLib.InputInteger("출금액");

				int idx = ValueToIdx(id);
				Account acc = (Account)accounts[idx];
				acc.MinMoney(money);

				AccInfo info = new AccInfo(id, 0, money, acc.Balance);
				accountinfos.PushBack(info);

				Console.WriteLine("출금 성공");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public void Select()
        {
			Console.WriteLine("[계좌 검색]\n");

			int id = WbLib.InputInteger("계좌번호 입력");

			int idx = ValueToIdx(id);
			Account acc = (Account)accounts[idx];
			acc.Println();

			//계좌번호에 해당되는 거래 내역 출력!
			Console.WriteLine("-------------------------------------------");
			Console.WriteLine("계좌번호\t입금액\t출금액\t잔액");
			Console.WriteLine("-------------------------------------------");
			for(int i=0; i< accountinfos.Size; i++)
			{
				AccInfo info = (AccInfo)accountinfos[i];
				if (info.Id == id)
					info.Print();
			}
			Console.WriteLine("-------------------------------------------");
		}

		private int ValueToIdx(int id)
		{
			for (int i = 0; i < accounts.Size; i++)
			{
				Account acc = (Account)accounts[i];
				if (acc.Id == id)
					return i;  //찾은 인덱스
			}
			throw new Exception("없는 계좌");
		}
		#endregion
	}
}
