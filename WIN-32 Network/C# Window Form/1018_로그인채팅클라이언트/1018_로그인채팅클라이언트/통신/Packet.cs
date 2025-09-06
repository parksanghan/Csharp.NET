using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1018_로그인채팅클라이언트
{
    static class Packet
    {
        const string PACK_MEMBERJOIN = "memberjoin";
        const string PACK_LOGIN     =  "login";

        public static string MemberJoin(string id, string pw, string name)
        {
            string packet = string.Empty;

            packet += PACK_MEMBERJOIN + "\a";
            packet += id + "#";
            packet += pw + "#";
            packet += name;

            return packet;
        }
        
        public static string LogIn(string id, string pw)
        {
            string packet = string.Empty;

            packet += PACK_LOGIN + "\a";
            packet += id + "#";
            packet += pw;

            return packet;
        }
    }
}
