//MemberDelete.cs


namespace _1011_회원관리프로그램
{
    internal class MemberDelete
    {
        public static void DeleteMember(out string _name)
        {
            string name = WbLib.InputString("이름을 입력하세요");
            _name = name;
        }
    }
}
