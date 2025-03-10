//MemberManager.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1011_회원관리프로그램
{
    //생성자 -> 파일읽기
    //소멸자 -> Dispose() : 파일저장    
    internal class MemberManager : IDisposable
    {
        private Dictionary<string, Member> members = new Dictionary<string, Member>();

        #region 이벤트

        public event AddMemberEvent     AddMemberEventHandler       = null;
        public event SelectMemberEvent  SelectMemberEventHandler    = null;
        public event UpdateMemberEvent  UpdateMemberEventHandler    = null;
        public event DeleteMemberEvent  DeleteMemberEventHandler    = null;

        #endregion 

        #region 소멸처리
        public void Dispose()
        {
            MemberFile.FileSave(members);
            GC.SuppressFinalize(this);
        }

        ~MemberManager()
        {
            Dispose();
        }
        #endregion 

        #region 싱글톤 패턴

        public static MemberManager Singleton { get; private set; }
        static MemberManager()
        {
            Singleton = new MemberManager();            
        }
        private MemberManager() 
        {
            MemberFile.FileLoad(members);   //불러오기(주소 전달)
        }

        #endregion

        #region 기능

        public void AddMember()
        {
            Console.WriteLine("[회원 저장]\n");

            try
            {
                string name, addr;

                MemberAdder.AddMember(out name, out addr);

                if (members.ContainsKey(name) == true)
                    throw new Exception("중복된 이름입니다.");

                members.Add(name, new Member(name, addr));

                Console.WriteLine("저장 성공");

                if (AddMemberEventHandler != null)
                    AddMemberEventHandler(this, new AddMember(name, addr));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SelectMember()
        {
            Console.WriteLine("[회원 검색]\n");

            try
            {
                string name;
                MemberSelect.SelectMember(out name);
                Member member = members[name];
                member.Println();

                if (SelectMemberEventHandler != null)
                    SelectMemberEventHandler(this, new SelectMember(name));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateMember()
        {
            Console.WriteLine("[회원 주소 변경]\n");

            try
            {
                string name, addr;
                MemberUpdate.UpdateMember(out name, out addr);

                members[name].Addr = addr;
                Console.WriteLine("주소 변경 성공");

                if (UpdateMemberEventHandler != null)
                    UpdateMemberEventHandler(this, new UpdateMember(name, addr));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteMember()
        {
            Console.WriteLine("[회원 삭제]\n");

            try
            {
                string name;
                MemberDelete.DeleteMember(out name);

                members.Remove(name);

                Console.WriteLine("회원 삭제 성공");

                if (DeleteMemberEventHandler != null)
                    DeleteMemberEventHandler(this, new DeleteMember(name));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void PrintMember()
        {
            foreach(Member member in members.Values)
            {
                Console.WriteLine(member);  //member.toString()
            }
        }

      

        #endregion
    
    }
}