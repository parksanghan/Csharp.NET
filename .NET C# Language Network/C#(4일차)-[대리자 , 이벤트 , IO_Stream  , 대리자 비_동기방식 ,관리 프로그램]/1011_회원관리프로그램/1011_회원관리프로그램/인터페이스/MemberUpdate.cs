//MemberUpdate.cs

namespace _1011_회원관리프로그램
{
    internal class MemberUpdate
    {
        public static void UpdateMember(out string _name, out string _addr)
        {
            string name = WbLib.InputString("이름을 입력하세요");
            string addr = WbLib.InputString("변경할 주소를 입력하세요");

            _name = name;
            _addr = addr;
        }
    }
}
