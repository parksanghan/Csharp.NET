//member.cs
using System;

namespace _1017_회원관리프로그램
{
    public class Member
    {
        public int Id           { get; private set; }
        public string Name      { get; private set; }
        public string Phone     { get; set; }
        public bool Gender      { get; private set; }
        public DateTime Date    { get; private set; }

        public Member(int id, string name, string phone, bool gender, DateTime date)
        {
            Id      = id;
            Name    = name;
            Phone   = phone;
            Gender  = gender;
            Date    = date;
        }

        public override string ToString()
        {
            return Id + "\t" + Name + "\t" + Phone + "\t" + 
                Gender + "\t" + Date.ToShortDateString();
        }
    }
}
