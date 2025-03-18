//MemberEvent.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 이벤트 Arg 만든다 . : Event args
// delegate 선언 형식 만들고
// 이벤트를 만들고  -> (필수아님)초기화 -> 구독 시키고
// 호출구조에서 Invoke 형식으로 호출 

namespace _1011_회원관리프로그램
{
    #region Arg클래스
    public class AddMember : EventArgs
    {
        public string Name { get; private set; }
        public string Addr { get; private set; }

        public DateTime EventTime { get; private set; }

        public AddMember(string name, string addr)
        {
            Name = name;
            Addr = addr;
            EventTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("[{0}] Add {1} \t {2}", EventTime.ToString(), Name, Addr);
        }
    }

    public class SelectMember : EventArgs
    {
        public string Name { get; private set; }

        public DateTime EventTime { get; private set; }

        public SelectMember(string name)
        {
            Name = name;
            EventTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("[{0}] Select {1}", EventTime.ToString(), Name);
        }
    }

    public class UpdateMember : EventArgs
    {
        public string Name { get; private set; }
        public string Addr { get; private set; }

        public DateTime EventTime { get; private set; }

        public UpdateMember(string name, string addr)
        {
            Name = name;
            Addr = addr;
            EventTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("[{0}] Update {1} \t {2}", EventTime.ToString(), Name, Addr);
        }
    }

    public class DeleteMember : EventArgs
    {
        public string Name { get; private set; }

        public DateTime EventTime { get; private set; }

        public DeleteMember(string name)
        {
            Name = name;
            EventTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("[{0}] Delete {1}", EventTime.ToString(), Name);
        }
    }
    #endregion

    #region 대리자
    public delegate void AddMemberEvent(object obj, AddMember e);
    public delegate void SelectMemberEvent(object obj, SelectMember e);
    public delegate void UpdateMemberEvent(object obj, UpdateMember e);
    public delegate void DeleteMemberEvent(object obj, DeleteMember e);

    #endregion 
}
