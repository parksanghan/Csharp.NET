using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace 끝말잇기_서버
{
    internal class Control // 싱글톤 
    {
        // GameCotnrol 정의하여 Game유저에 대한 멤버 , 소켓 관리
        static Server server = null;
        private const int SERVER_PORT = 7000;

        static Server _con = Server.GetInstance(SERVER_PORT, RecvDel, LogDel);
       
        static Socket SubServerSocket = null;

        static GameControl g_con = GameControl.GetInstance();
        static List<Member> logins = new List<Member>();
        public static Control Instance { get; private set; } = null;
        static Control()
        {
            Instance = new Control();
        }
        private Control()
        {

        }
        public void Run()
        {
            try
            {
                server = new Server(SERVER_PORT, RecvDel, LogDel);
                server.Start();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        public void Dispose()
        {

            server.Stop();
            GC.SuppressFinalize(this);
        }
        static void gamejoinsplit(Socket s, string msg) //bool#id
        {  // 이걸 준 소켓은 서브서버인데.. 
            try
            {
                // 여긴 그냥 ack 를 또 클라이언트에게 ack를 주는것이므로 멤버추가하지 않음
                string[] sp = msg.Split('#');
                bool result = bool.Parse(sp[0]); // 서버 나뉘면서 memberlist 로 역추적해야함...
                string id = sp[1]; // id로 소켓 역추적 
                GameMember traceuser = g_con.gameuser.Find(m => m.Id == id);
                Socket tracesocket = traceuser.sock;
                string message = Pakcet.gamejoinack(result, id);
                server.SendData(tracesocket, message); // 패킷으로  줘야할텐데 

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        static void gameoutsplit(Socket s, string msg) // bool#id
        {
            try
            {
                string[] sp = msg.Split('#');
                bool result = bool.Parse(sp[0]);
                string id = sp[1]; // id로 소켓 역추적 
                GameMember traceuser = g_con.gameuser.Find(m => m.Id == id);
                Socket tracesocket = traceuser.sock;
                string message = Pakcet.gameoutack(result, id);
                server.SendAllData(s, message, false);//s 는서브서버 소켓
                // 서브서버 말고 모두에게 ack 보냄 
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void sendmessagesplit(Socket s, string msg)//bool#id#msg
        {
            string[] sp = msg.Split('#');
            bool result = bool.Parse(sp[0]);
            string id = sp[1]; // id로 소켓 역추적 
            string recentmsg = sp[2];
            string pack = Pakcet.sendmessageack(result, id, recentmsg);
            server.SendAllData(s, pack, false);
            // 모두에게 전송 id를 받은 사람이 답장을 보내야할 차례 
        }

        static void wrongmessagesplit(Socket s, string msg)//bool#id
        {
            string[] sp = msg.Split('#');
            bool result = bool.Parse(sp[0]);
            string id = sp[1]; // id로 소켓 역추적
            string message = Pakcet.wrongmessage(result, id); // 진사람 id 전송
            server.SendAllData(s, message, false);
        }
        static void RecvDel(Socket s, string msg)
        {

            string[] sp = msg.Split('\a');
            if (sp[0] == "subservernotice")
            { SubServerSocket = s;
                Console.WriteLine("서브서버확인");
            } // 나는 서브서버에요
            else if (sp[0] == "subserver") //subserver@gamejoin  OR   @gameout OR @sendmessage OR @wrongmessage# 
            {
                string[] spp = sp[1].Split('@');
                  if (spp[0] == "gamejoinack")   // 서브 서버에서 준 ack 를 통해 id 로 역추적 하여 해당소켓에 전송
                {                           //bool#id
                    gamejoinsplit(s, spp[1]);  
                }
                else if (spp[0] == "gameoutack")//bool#id
                {
                    gameoutsplit(s, spp[1]);  
                }
                else if (spp[0] == "sendmessageack")//bool#id#msg
                {   
                    // 정상적인 메세지 받아옴  id 는 이제 해야 하는 사람의 id 리턴
                    sendmessagesplit(s, spp[1]);
                }
                else if (spp[0] == "wrongmessageack")//bool#id
                {
                    // 비정상적인 메시지들어옴 * 비문자열 , 틀린 문자열인 경우 =>  틀린사람의 id 리턴
                    wrongmessagesplit(s, spp[1]);
                }
                else
                {
                    Console.WriteLine("match 되지 않는 패킷");
                }
                ///==================== 서브 서버에서 보낸 패킷=========================== 
            }
            // d } // 서브 서버에서 들어온 패킷 
            ////// 나머지는 // 클라이언트에서 보낸 패킷 
            else if (sp[0] == "sendmessage")
            {// 서브 서버에게 보내야함...
                string message = sp[0] + "\a" + sp[1];
                //send
                server.SendData(SubServerSocket, message);

            }
            else if (sp[0] == "memberlogin")  //#id#pw
            {
                MemberLogin(s, sp[1]);
            }  

            else if (sp[0] == "memberlogout") //#id
            {
                MemberLogout(s, sp[1]);
            }
            else if (sp[0] == "gamejoin")   //#id#datetime  // 클라이언트에서 게임 시작 요청
            {

                string sendtosubserver = sp[0] + sp[1];
                server.SendData(SubServerSocket, sendtosubserver);

                g_con.gamers.Add(s); // 소켓 // 필요없을 수도 있음
                string[] spp = sp[1].Split('#');
                string id = spp[0];
                DateTime dt = DateTime.Parse(spp[1]);
                GameMember gm = new GameMember(s, id, dt);
                g_con.gameuser.Add(gm); // GameMember socket , id , datetime 
                g_con.gameuser.Sort((m1, m2) => m1.Dt.CompareTo(m2.Dt)); // Datetime 전체정렬 
                // send 처리

            } 
            else if (sp[0] == "gameout")   //id  
            {
                string sendtosubserver = sp[0] + sp[1];
                server.SendData(SubServerSocket, sendtosubserver); // 그대로 보냄
                string[] spp = sp[1].Split('#'); // 안넣었다면 필요없음 
                string id = spp[0];
                Member mem = logins.Find(m => m.Id == id);
                string serachid = mem.Id;
                GameMember gcc = g_con.gameuser.Find(m => m.Id == serachid);
                g_con.gamers.Remove(gcc.sock);// 또는 s 
                g_con.gameuser.Remove(gcc);
                // send 처리
                string message = Pakcet.gameoutack(true, id);
                server.SendData(s, message);
            }
        }

        // 로그인
        static void MemberLogin(Socket s, string msg)//Id#pw
        {
            // 로그인 기능  
            string[] sp = msg.Split('#');
            string id = sp[0];
            string pw = sp[1];
            Member mem = new Member(id, pw);
            logins.Add(mem);
            // 소켓은 추가할 필요가없음 
            // send
            string message = Pakcet.loginack(true, id);
            server.SendData(s, message);
        }


        // 로그 아웃
        static void MemberLogout(Socket s, string msg)//Id#
        {
            string[] sp = msg.Split('#');
            string id = sp[0];
            Member mem = logins.Find(m => m.Id == id);
            logins.Remove(mem);
            _con.sockets.Remove(s);
            // send
            string message = Pakcet.logoutack(true, id);
            server.SendData(s, message);
        }

        static void LogDel(Socket s, Log log, string msg)
        {

            //string temp = string.Format(log.ToString() + "\t" + msg);
            //Console.WriteLine(temp);
        }


        public bool SendAllDatatoGamers(Socket client, string msg, bool isuser)   
        { // 기존 sendall 은 원본 소켓이므로 게임 서버의 멤버들에게 보내야함
            if (isuser == true)
            {
                Console.WriteLine("송신 : " + msg);
                foreach (Socket socket in g_con.gamers)
                {
                    server.SendData(socket, msg);
                }
            }
            else
            {
                foreach (Socket socket in g_con.gamers)
                {
                    if (socket != client)
                        server.SendData(socket, msg);
                }
            }
            return true;
        }


        // callback 함수 
        // 데이터를 전송한 소켓 정보 클라이언트 정보가 있음 
        // GameControl 에서 Server  singleton 메서드를 통해 Server 클래스의 소켓 리스트를 가져오고 
        // 만약에 ?  여기서 패킷에 게임 접속에 대한 메시지 발생시  게임 유저 socket 리스트에 저장
    }
}
