using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _231106_MultiGame_Client
{
    enum PacketClientType
    {
        USER_LOGIN,     //로그인 시도
        USER_LOGOUT,    //로그아웃 시도
        USER_SIGNUP,    //회원가입 시도

        ROOM_GETIN, //방 입장
        ROOM_GETOUT, //방 입장
        ROOM_MAKE, //방 입장

        INGAME_INPUT, //인게임 입력

    }
    enum PacketServerType
    {
        USER_LOGIN_RESULT,  //로그인 결과
        USER_LOGOUT_RESULT, //로그아웃 결과
        USER_SIGNUP_RESULT, //회원가입 결과

        ROOM_LIST, //방 목록
        ROOM_GETIN_RESULT, //방 입장
        ROOM_GETOUT_RESULT, //방 입장
        ROOM_MAKE_RESULT, //방 입장

        INGAME_TICK,    //인게임 프레임
        INGAME_MESSAGE,    //인게임 메세지 - 서버 또는 사용자

        INGAME_SOUND,   //음향 효과
        INGAME_SHAKE,   //카메라 흔들림 효과
    }

    public enum MessageType
    {
        MANAGER,    //관리자
        WARNING,    //경고
        USER,       //사용자
    }
    public enum SoundType
    {
        SCREAM,
        HITTED,
        FIRE_HG,
        FIRE_SMG,
        FIRE_AR,
        FIRE_SG,
        FIRE_DMR,
    }
    internal static class Packet
    {
        #region [클라측 패킷]
        public static string UserLogin(string id, string pw)
        {
            return $"{PacketClientType.USER_LOGIN}\r{id}#{pw}";
        }
        public static string UserLogout(string id)
        {
            return $"{PacketClientType.USER_LOGOUT}\r{id}";
        }
        public static string UserSignup(string id, string pw, string name)
        {
            return $"{PacketClientType.USER_SIGNUP}\r{id}#{pw}#{name}";
        }

        public static string RoomGetIn(string id, string roomName)
        {
            return $"{PacketClientType.ROOM_GETIN}\r{id}#{roomName}";
        }
        public static string RoomGetOut(string id)
        {
            return $"{PacketClientType.ROOM_GETOUT}\r{id}";
        }
        public static string RoomMake(string id, string roomName)
        {
            return $"{PacketClientType.ROOM_MAKE}\r{id}#{roomName}";
        }

        public static string IngameInput(int upAxis, int sideAxis, float faceDir, bool isTriggered)
        {
            return $"{PacketClientType.INGAME_INPUT}\r{upAxis}#{sideAxis}#{faceDir}#{isTriggered}";
        }
        #endregion

        #region [서버측 패킷]
        public static string UserLoginResult(bool isSucceed, string msg)
        {
            return $"{PacketServerType.USER_LOGIN_RESULT}\r{isSucceed}#{msg}";
        }
        public static string UserLogoutResult(bool isSucceed, string msg)
        {
            return $"{PacketServerType.USER_LOGOUT_RESULT}\r{isSucceed}#{msg}";
        }
        public static string UserSignupResult(bool isSucceed, string msg)
        {
            return $"{PacketServerType.USER_SIGNUP_RESULT}\r{isSucceed}#{msg}";
        }

        //public static string RoomList(List<Ingame> ingames)
        //{
        //    string temp = $"{PacketServerType.ROOM_LIST}\r";

        //    foreach (Ingame ingame in ingames)
        //        temp += $"{ingame.name}#{ingame.userCount}@";

        //    return temp;
        //}
        public static string RoomGetInResult(bool isSucceed, string msg)
        {
            return $"{PacketServerType.ROOM_GETIN_RESULT}\r{isSucceed}#{msg}";
        }
        public static string RoomGetOutResult(bool isSucceed, string msg)
        {
            return $"{PacketServerType.ROOM_GETOUT_RESULT}\r{isSucceed}#{msg}";
        }
        public static string RoomMakeResult(bool isSucceed, string msg)
        {
            return $"{PacketServerType.ROOM_MAKE_RESULT}\r{isSucceed}#{msg}";
        }

        //public static string IngameTick(Ingame ingame)
        //{
        //    return $"{PacketServerType.INGAME_TICK}\r";
        //}
        public static string IngameMessage(MessageType msgType, string sender, string msg)
        {
            return $"{PacketServerType.INGAME_MESSAGE}\r{msgType}#{sender}#{msg}";
        }
        #endregion

    }
}
