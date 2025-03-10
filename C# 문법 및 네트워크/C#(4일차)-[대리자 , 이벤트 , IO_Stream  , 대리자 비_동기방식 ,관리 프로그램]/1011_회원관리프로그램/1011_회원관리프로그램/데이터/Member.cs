//Member
using System;

namespace _1011_회원관리프로그램
{
    internal class Member
    {
        public string Name { get; set; }    
        public string Addr { get; set; }

        public Member(string name, string addr)
        {
            Name = name;
            Addr = addr;
        }
    
        public void Println()
        {
            Console.WriteLine("[이름]" + Name);
            Console.WriteLine("[주소]" + Addr);
        }

        public override string ToString()
        {
            return Name + "\t" + Addr;
        }
    }
}
