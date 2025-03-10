//packet.cs

using System.Collections.Generic;

namespace _1018_채팅서버
{
    static class Packet
    {
        const string PACK_MEMBERJOIN    = "memberjoin";
        const string PACK_LOGIN         = "login";


      
        public static string MemberJoin(bool b, string id, string pw, string name)
        {
            string packet = string.Empty;

            packet += PACK_MEMBERJOIN + "\a";
            packet += b + "#";
            packet += id + "#";
            packet += pw + "#";
            packet += name;

            return packet;
        }

        public static string LogIn(bool b, string id, string pw, string name)
        {
            string packet = string.Empty;

            packet += PACK_LOGIN + "\a";
            packet += b + "#";
            packet += id + "#";
            packet += pw + "#";
            packet += name;

            return packet;
        }
    }
}
