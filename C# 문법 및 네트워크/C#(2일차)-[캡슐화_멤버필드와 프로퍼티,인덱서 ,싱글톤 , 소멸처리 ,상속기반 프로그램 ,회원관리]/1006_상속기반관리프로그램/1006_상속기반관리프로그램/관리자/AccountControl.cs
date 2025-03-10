//AccountControl.cs
using System;

/*
계좌개설, 계좌삭제, [계좌리스트 전체출력 / 회원리스트 전체출력]
					 신용계좌 리스트출력, 기부계좌리스트출력, 일반계좌리스트출력
*/
namespace _1006_상속기반관리프로그램
{
    internal class AccountControl
    {
		#region 싱글톤(생성자 초기화 처리 추가)
		public static AccountControl Singleton { get; private set; }

		static AccountControl()
		{
			Singleton = new AccountControl();
		}
		private AccountControl()
		{
			int max = WbLib.InputInteger("계좌 정보 저장 개수");
			accounts = new WBArray(max);

			//미리 sample 3개 저장 ---------------------------------------------
			accounts.PushBack(new Account(100, 1000));
			accounts.PushBack(new FaithAccount(100, 1000));
			accounts.PushBack(new ContriAccount(101, 1000));
			//-------------------------------------------------------------------
		}
		#endregion

		public WBArray accounts { get; private set; }

        #region [메뉴2]계좌 관리 메서드
        public void AccountPrintAll()
		{
			Console.WriteLine("최대저장개수 : " + accounts.Max);
			Console.WriteLine("현재저장개수 : " + accounts.Size);

			for (int i = 0; i < accounts.Size; i++)
			{
				Account account = (Account)accounts[i];
				account.Print();
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		
		public void MemberPrintAll()
        {
			MemberControl membercon = MemberControl.Singleton;
			membercon.PrintAll();	
		}			

		public void Insert()
        {
			Console.WriteLine("[계좌 개설]\n");

			try
			{
				//회원리스트 출력
				MemberControl membercon = MemberControl.Singleton;
				membercon.PrintAll();

				int mem_number = WbLib.InputInteger("회원번호 입력");
				int money = WbLib.InputInteger("입금액 입력");
				int type = WbLib.InputInteger("[1]일반계좌 [2]신용계좌 [3]기부계좌");

				Account account = null;
				switch (type)
				{
					case 1: account = new Account(mem_number, money); break;
					case 2: account = new FaithAccount(mem_number, money); break;
					case 3: account = new ContriAccount(mem_number, money); break;
					default: throw new Exception("잘못된 계좌 선택");
				}

				accounts.PushBack(account);

				Console.WriteLine("계좌 생성 완료");
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
		}

		public void Erase()
        {
			Console.WriteLine("[계좌 삭제]\n");

			try
			{
				int num = WbLib.InputInteger("삭제할 계좌번호 입력");

				int idx = AccNumToIdx(num);
				accounts.Erase(idx);
				Console.WriteLine("삭제되었습니다");
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void Faith_printall()
        {
			Console.WriteLine("[신용 계좌 리스트]\n");

			for(int i=0; i<accounts.Size; i++)
			{
				Account account = (Account)accounts[i];
				if (account is FaithAccount)
				{
					account.Print();
					Console.WriteLine();
				}
			}
			Console.WriteLine();
		}

		public void Contri_printall()
        {
			Console.WriteLine("[기부 계좌 리스트]\n");

			for (int i = 0; i < accounts.Size; i++)
			{
				Account account = (Account)accounts[i];
				if (account is ContriAccount)
				{
					account.Print();
					Console.WriteLine();
				}
			}
			Console.WriteLine();
		}
		public void Acc_printall()
        {
			Console.WriteLine("[일반 계좌 리스트]\n");

			for (int i = 0; i < accounts.Size; i++)
			{
				Account account = (Account)accounts[i];
				if ( (account is ContriAccount == false)  && (account is FaithAccount == false))
				{
					account.Print();
					Console.WriteLine();
				}
			}
			Console.WriteLine();
		}

		private	int AccNumToIdx(int num)
        {
			for (int i = 0; i < accounts.Size; i++)
			{
				Account account = (Account)accounts[i];
				if (account.AccNumber == num)
					return i;
			}
			throw new Exception("없는 계좌번호");
		}
        #endregion

        #region [메뉴3]계좌 검색 및 입출금 관련 메서드
		public void AccountSelectNumber()
        {
			Console.WriteLine("[계좌 검색]\n");

			try
			{
				int accnumber = WbLib.InputInteger("계좌번호 입력");
				int idx = AccNumToIdx(accnumber);
				Account account = (Account)accounts[idx];
				account.Println();
			}
			catch(Exception e)
            {
				Console.WriteLine(e.Message);
            }
		}

		public void AccountSelectMemberID()
        {
			Console.WriteLine("[계좌 검색]\n");

			try
			{
				int memberid = WbLib.InputInteger("회원번호 입력");
				for (int i = 0; i < accounts.Size; i++)
				{
					Account account = (Account)accounts[i];
					if (account.MemNumber == memberid)
					{
						account.Print();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void InputMoney()
        {
			Console.WriteLine("[계좌 입금]\n");

			try
			{
				int accnumber	= WbLib.InputInteger("계좌번호 입력");
				int money		= WbLib.InputInteger("입금액 입력");
				int idx = AccNumToIdx(accnumber);
				Account account = (Account)accounts[idx];
				account.InputMoney(money);
				Console.WriteLine("입금 성공");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void OutputMoney()
        {
			Console.WriteLine("[계좌 출금]\n");

			try
			{
				int accnumber = WbLib.InputInteger("계좌번호 입력");
				int money = WbLib.InputInteger("출금액 입력");
				int idx = AccNumToIdx(accnumber);
				Account account = (Account)accounts[idx];
				account.OutputMoney(money);
				Console.WriteLine("출금 성공");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

        #endregion 
    }
}
