//packet.cs

namespace _1018_채팅클라이언트
{
    static class Packet
    {
        const string PACK_SHORTMESSAGE      = "shortmessage";

        public static string ShortMessage(string nickname, string msg)
        {
            string packet = string.Empty;

            packet += PACK_SHORTMESSAGE + "\a";
            packet += nickname + "#";
            packet += msg;

            return packet;
        }
       
    }
}
