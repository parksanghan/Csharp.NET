//membercontrol.cs
using System;
using System.Collections.Generic;
using _1017_회원관리프로그램.파일IO;

namespace _1017_회원관리프로그램
{
    internal class MemberControl
	{
		private List<Member> members = new List<Member>();
		public List<Member> Members { get { return members; } }

		#region 싱글톤 패턴

		public static MemberControl Singleton { get; private set; }
		static MemberControl()
		{
			Singleton = new MemberControl();
		}
		private MemberControl()
		{
		}
		#endregion

		#region 메서드
		public bool Member_Insert(int id, string name, string phone, bool gender, DateTime date)
		{
			try
			{
				Member member = new Member(id, name, phone, gender, date);
				members.Add(member);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return false;
			}
		}

        public bool Member_Delete(int id)
        {
            try
            {
                Member member = members.Find(m => m.Id == id);
                return members.Remove(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

		public bool Member_Update(int id, string phone)
        {
			try
			{
				Member member = members.Find(m => m.Id == id);
				member.Phone = phone;
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		public int GetIndex(int memberid)
        {
            for (int i = 0; i < members.Count; i++)
            {
                Member member = members[i];
                if (member.Id == memberid)
                    return i;  //찾은 인덱스
            }
			return -1;
        }

		public Member GetMember(int idx)
        {
			return members[idx];
        }

		#endregion

		#region 시작 & w종료(파일IO처리)
		public void Load()
        {
			WbFile.FileLoad(Members);
        }

		public void Closed()
        {
			WbFile.FileSave(Members);
        }
		#endregion
	}
}
