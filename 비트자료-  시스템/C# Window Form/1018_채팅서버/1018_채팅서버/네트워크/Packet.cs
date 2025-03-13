//packet.cs

using System.Collections.Generic;

namespace _1018_채팅서버
{
    static class Packet
    {
        const string PACK_MEMBER_JOIN = "memberjoin";
        public static string ShortMessage(bool b, string id,string pw ,  string name)
        {
            string packet = string.Empty;

            packet += PACK_MEMBER_JOIN + "\a";
            packet += name + "#";
            packet += pw + "#";
            packet += name;

            return packet;
        }
    }
}
