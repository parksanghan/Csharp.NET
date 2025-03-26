//MemberViewer.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1011_회원관리프로그램
{
    internal class MemberViewer
    {        
        public MemberViewer()
        {
            //이벤트 등록
            MemberManager mm = MemberManager.Singleton;
            mm.AddMemberEventHandler    += AddMemberHandler;
            mm.SelectMemberEventHandler += SelectMemberHandler;
            mm.UpdateMemberEventHandler += UpdateMemberHandler;
            mm.DeleteMemberEventHandler += DeleteMemberHandler;
        }

        public void AddMemberHandler(object obj, AddMember e )
        {
            MemberFile.FileLogSave(e);
        }

        public void SelectMemberHandler(object obj, SelectMember e)
        {
            MemberFile.FileLogSave(e);
        }

        public  void UpdateMemberHandler(object obj, UpdateMember e)
        {
            MemberFile.FileLogSave(e);
        }

        public  void DeleteMemberHandler(object obj, DeleteMember e)
        {
            MemberFile.FileLogSave(e);
        }
    }
}
