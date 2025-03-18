using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using static 서브서버.Client;

namespace 서브서버
{
    internal class GameControl
    {

        private Client client = null;
        const string SERVER_IP = "127.0.0.1";
        const int SERVER_PORT = 7000;
        List<GameMember> gameMembers = new List<GameMember>();
        public static GameControl Instance { get; private set; } = null;

        static GameControl()
        {
            Instance = new GameControl();
        }
        private GameControl()
        {
            client = new Client(SERVER_IP, SERVER_PORT, RecvDel, LogDel);
        }
        public void LogDel(Socket s, Log log, string msg)
        {
            string temp = string.Format(log.ToString() + "\t" + msg);
            Console.WriteLine(temp);
        }
        public void Run()
        {
            client.Start();
            string itsme = Packet.noticeServer();

            client.SendData(itsme);
        }
        static char FirstWord = '\0';
        static char LastWord = '\0';
        public void gameover()   // game over 호출마다 game over 패킷 전송하여 클라이언트에서 나가라고 메시지 박스 
        {
            gameMembers[1].Resetsize();
            // send worng 패배 자 전송 처리 ? 할까 말까 
            gameMembers.Clear();
        }
        public void RecvDel(Socket s, string msg) // 데이터를 전송한 소켓 정보 클라이언트 정보가 있음 
        {  // GameControl 에서  singleton 메서드를 통해 Server 클래스의 소켓 리스트를 가져오고 
           // 만약에 ?  여기서 패킷에 게임 접속에 대한 메시지 발생시  게임 유저 socket 리스트에 저장
            string[] sp = msg.Split('\a');
            if (sp[0] == "gamejoin")
            {
                gameJoin(s, sp[1]); //gamejoin#id   or gamejoin#id#datetime
            }
            else if (sp[0] == "gameout")//gameout#id
            {
                gameout(s, sp[1]);
            }
            else if (sp[0] == "sendmessage") //sendmessage#id
            {// 답을 보낸경우 
                checkgame(s, sp[1]);
            }
            else
            {
                Console.WriteLine("Match 되지 않는 패킷");
            }


        }
        public void gameJoin(Socket s, string msg)
        {
            try
            {
                Console.WriteLine("입장 수신\n");
                Console.WriteLine(msg);

                string[] sp = msg.Split('#');
                string id = sp[0];
                GameMember mem = new GameMember(id);
                gameMembers.Add(mem);
                string message = Packet.GameJoin(true, id); // gamejoinack send 처리 
                client.SendData(msg);
                 
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        public void gameout(Socket s, string msg)
        {
            try
            {
                Console.WriteLine("퇴장  수신\n");
                Console.WriteLine(msg);
                // 인덱스 알아서 사라지니 size 냅둠 
                string[] sp = msg.Split('#');
                string id = sp[0];
                GameMember mem = new GameMember(id);
                gameMembers.Remove(mem);   // 4 
                int turn = mem.Getsize() % gameMembers.Count; //
                                                              // send 처리
                string message = Packet.GameOut(true, gameMembers[turn].Id);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public void checkgame(Socket s, string msg)
        {

            ////
            //static string FirstWord = null;
            //static string LastWord = null;
            Console.WriteLine("끝말잇기 메시지 수신\n");
            Console.WriteLine(msg);
            string[] sp = msg.Split('#');   //id#메시지
            string id = sp[0];
            GameMember match = gameMembers.Find(m => m.Id == id);
            int turn = match.Getsize() % gameMembers.Count;     //  1 1 2 2 3 3  4 4 5 5
            // 3   2    1    
             
            if (match.Id == gameMembers[turn].Id)
            {
                if (match.Getsize() == 1) // 처음 참여 유저 
                {
                    string getmessage = sp[1];
                    if (!string.IsNullOrEmpty(getmessage))
                    {
                        FirstWord = getmessage[0]; // 맨 앞 문자
                        LastWord = getmessage[getmessage.Length - 1]; // 맨 끝 문자
                        match.Upsize();// 정상처리 후 증가 ++
                        string message = Packet.SendMessage(true, gameMembers[turn].Id, getmessage);
                        // 
                        // send 처리
                        client.SendData(message);
                    }
                    //검사하지 않고 바로 반환 
                    else // 첫번째 사람
                    {
                        Console.WriteLine("빈문자열");
                        Console.WriteLine("게임종료 {0}패배 ", gameMembers[turn].Id);
                        // 누가 졌는지 send 
                        // send 처리
                        string message = Packet.WrongMessage(true, gameMembers[turn].Id);
                        client.SendData(message);

                        gameover();
                        // gameover 도 send? 

                    }



                }
                else // 첫번째 유저가 아닌 사람 
                {   // First Send 검사 
                    string getmessage = sp[1];
                    if (!string.IsNullOrEmpty(getmessage))
                    {
                        char matchfirst = getmessage[0]; // 맨 앞 문자
                        char matchlast = getmessage[getmessage.Length - 1]; // 맨 끝 문자
                        if (LastWord == matchfirst)
                        {
                            Console.WriteLine("통과");
                            Console.WriteLine($"맨 앞 문자: {FirstWord}");
                            Console.WriteLine($"맨 끝 문자: {LastWord}");
                            match.Upsize(); // [turn ] 증가 

                            FirstWord = matchfirst;
                            LastWord = matchlast;
                            // send 처리
                            string message = Packet.SendMessage(true, gameMembers[turn].Id, getmessage);
                            client.SendData(message);
                        }
                        else
                        {

                            Console.WriteLine("{0}유저가 틀렸습니다.", gameMembers[turn].Id);

                            // send 처리
                            string message = Packet.WrongMessage(true, gameMembers[turn].Id);
                            client.SendData(message);
                            //over 
                            gameover();
                        }

                    }
                    else
                    {
                        Console.WriteLine("빈문자열입니다.");
                        Console.WriteLine("{0}유저가 틀렸습니다.", gameMembers[turn].Id);

                        // send 처리
                        string message = Packet.WrongMessage(true, gameMembers[turn].Id);
                        client.SendData(message);
                        //over 
                        gameover();
                    }

                }
            }
        }
    }
}
