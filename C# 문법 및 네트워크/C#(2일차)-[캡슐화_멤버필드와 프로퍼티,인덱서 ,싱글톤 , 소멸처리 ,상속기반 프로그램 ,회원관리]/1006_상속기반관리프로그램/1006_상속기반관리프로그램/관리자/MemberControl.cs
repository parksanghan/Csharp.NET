//MemberControl.cs
using System;

namespace _1006_상속기반관리프로그램
{
    internal class MemberControl
	{
		#region 싱글톤(생성자 초기화 처리 추가)
		public static MemberControl Singleton { get; private set; }

		static MemberControl()
		{
			Singleton = new MemberControl();
		}
		private MemberControl() 
        {
			int max = WbLib.InputInteger("회원 정보 저장 개수");
			Members = new WBArray(max);

			//미리 sample 2개 저장 ---------------------------------------------
			Members.PushBack(new Member("김길동", "010-1111-1111"));
			Members.PushBack(new Member("공길동", "010-2222-2222"));
			//-------------------------------------------------------------------
		}
		#endregion

		public WBArray Members { get; private set;  }

		#region [메뉴1]회원 기능 관련 메서드
		public void PrintAll()
        {
			Console.WriteLine("최대저장개수 : " + Members.Max);
			Console.WriteLine("저장개수 : " + Members.Size);

			for (int i = 0; i < Members.Size; i++)
			{
				Member member = (Member)Members[i];
				member.Print();
			}
			Console.WriteLine();
		}

		public void Insert()
        {
			Console.WriteLine("[회원 저장]");

			try
			{
				string name = WbLib.InputString("회원이름 입력");
				string phone = WbLib.InputString("회원전화번호 입력");

				Member member = new Member(name, phone);
				Members.PushBack(member);

				Console.WriteLine("저장되었습니다");
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void Select()
        {
			Console.WriteLine("[회원 검색]");

			try
			{
				int num = WbLib.InputInteger("회원번호 입력");

				int idx = NumberToIdx(num);
				Member member = (Member)Members[idx];

				member.Println();
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
			
		}

		public void Update()
        {
			Console.WriteLine("[회원 정보 수정]");

			try
			{
				int num = WbLib.InputInteger("수정할 대상의 회원번호 입력");
				string phone = WbLib.InputString("변경할 전화번호 입력");

				int idx = NumberToIdx(num);
				Member member = (Member)Members[idx];
				member.Phone = phone;
				Console.WriteLine("수정되었습니다");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void Erase()
        {
			Console.WriteLine("[회원 삭제]");

			try
			{
				int num = WbLib.InputInteger("삭제할 회원번호 입력");

				int idx = NumberToIdx(num);
				Members.Erase(idx);
				Console.WriteLine("삭제되었습니다");
            }
			catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
			
		}

		private	int NumberToIdx(int key)
        {
			for (int i = 0; i < Members.Size; i++)
			{
				Member member = (Member)Members[i];
				if (member.Number == key)
					return i;
			}
			throw new Exception("없는 번호");
		}

        #endregion 
    }
}
