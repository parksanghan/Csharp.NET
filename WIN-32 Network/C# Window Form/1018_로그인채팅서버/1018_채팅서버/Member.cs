using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1018_채팅서버
{
    internal class Member
    {
        public string Id { get; set; }
        public string Pw { get; set; }
        public string Name { get; set; }

        public Member(string id, string pw, string name)
        {
            Id = id;
            Pw = pw;
            Name = name;
        }
    }
}
