using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 서브서버
{
    internal class Packet
    {
        const string PACK_SUBSERVERNOTICE = "subservernotice"; // 나 서브 서버에요 
        const string PACK_SUBSERVER = "subserver"; // 나 서브 서버에서 보낸 패킷
        // 클라이언트에서는 팩을 까는형식이 더 추가되야함

        const string PACK_GAMEJOIN = "gamejoinack";
        const string PACK_GAMEOUT = "gameoutack";
        const string PACK_SENDMESSAGE = "sendmessageack";
        const string PACK_WRONGMESSAGE = "wrongmessageack";


        public static string noticeServer()
        {
            string packet = string.Empty;
            packet += PACK_SUBSERVERNOTICE + "\a";
            return packet;

        }

        //#gamejoin#id = > bool#gamejoin#id
        public static string GameJoin(bool r , string id)  
        {
            string pack = string.Empty;
            pack += PACK_SUBSERVER + "\a";
            pack += PACK_GAMEJOIN + "@";// 어쩔수 없이 자르기 수단변경 
            pack += r + "#";
            pack += id;

            return pack;
        }
        public static string GameOut(bool r  , string id)
        {

            string pack = string.Empty;
            pack += PACK_SUBSERVER + "\a";
            pack += PACK_GAMEOUT + "@" ;
            pack += r + "#";
            pack += id;

            return pack;
        }


        public static string  SendMessage(bool r , string id , string msg)
        {
            string pack = string.Empty;
            pack += PACK_SUBSERVER + "\a";
            pack += PACK_SENDMESSAGE + "@"; // 어쩔수 없이 자르기 수단변경 
            pack += r + "#";
            pack += id + "#";
            pack += msg;
            return pack;
        }

        public static string WrongMessage(bool r , string id)  // 이 패킷 메시지를 받으면 게임 종료임  id 가 진사람 
        {
            string pack = string.Empty;
            pack += PACK_SUBSERVER + "\a";   
            pack += PACK_WRONGMESSAGE + "@";// 어쩔수 없이 자르기 수단변경 
            pack += r + "#";// 그냥송수신 잘됐는지에 대한 여부임 
            pack += id;
            return pack;

        }
    }
}
