using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 끝말잇기_서버
{
    internal class Pakcet
    {
        const string PACK_GAMEJOIN_ACK = "gamejoinack";
        const string PACK_GAMEOUT_ACK = "gameoutack";
        const string PACK_LOGIN_ACK = "loginack"; // 메인에서 주는 ack
        const string PACK_LOGOUT_ACK = "logoutack";
        const string PACK_JOIN_ACK = "joinack"; // 메인 에서 주는 ack 
        const string PACK_SENDMESSAGE_ACK = "sendmessageack";
        const string PACK_WRONGMESSAGE_ACK = "wrongmessageack";


        public static string gamejoinack(bool r, string id)
        {
            string packet = string.Empty;
            packet += PACK_GAMEJOIN_ACK + "\a";
            packet += r + "#";
            packet += id;
            return packet;
        }
        public static string gameoutack(bool r, string id)
        {
            string packet = string.Empty;
            packet += PACK_GAMEOUT_ACK + "\a";
            packet += r + "#";
            packet += id;
            return packet;
        }

        public static string sendmessageack(bool r, string id, string msg)//bool#id#msg
        {
            string packet = string.Empty;
            packet += PACK_SENDMESSAGE_ACK + '\a';
            packet += r + "#";
            packet += id + "#";
            packet += msg;
            return packet;
        }
        public static string wrongmessage(bool r, string id) // bool#id{
        {
            string packet = string.Empty;
            packet += PACK_WRONGMESSAGE_ACK + "\a";
            packet += r + "#";
            packet += id;
            return packet;
        }


        public static string loginack(bool r, string id)
        {
            string packet = string.Empty;
            packet += PACK_LOGIN_ACK + "\a";
            packet += r + "#";
            packet = id;
            return packet;



        }
        public static string logoutack(bool r , string id)
        {
            string packet = string.Empty;
            packet += PACK_LOGOUT_ACK + "\a";
            packet += r + "#";
            packet = id;
            return packet;

        }
    }
}
