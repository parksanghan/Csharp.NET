using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace _1023_시험_클라이언트
{
    internal class Control
    {
        private Client client = null;
        const string SERVER_IP = "220.90.179.75";
        const int SERVER_PORT = 7000;

        private List<Board> boards = new List<Board>();        
        public List<Board> Boards { get { return boards; } }

        public Form1 Form1 { get; set; } = null;

        #region 싱글톤
        public static Control Instance { get; private set; } = null;
        static Control()
        {
            Instance = new Control();
        }
        private Control()
        {
            client = new Client(SERVER_IP, SERVER_PORT, RecvDel, LogDel);
        }
        #endregion

        #region Callback
        public void RecvDel(Socket s, string msg)
        {
            string[] sp = msg.Split('\a');
            if (sp[0] == "boardinsert")
                BoardInsert_Ack(sp[1]);
            else if (sp[0] == "boardprintall")
                BoardPrintAll_Ack(sp[1]);
            else if (sp[0] == "boardselect")
                BoardSelect_Ack(sp[1]);
            else if (sp[0] == "boardupdate")
                BoardUpdate_Ack(sp[1]);
            else if (sp[0] == "boarddelete")
                BoardDelete_Ack(sp[1]);
        }

        public void LogDel(Socket s, Log log, string msg)
        {
            string temp = string.Format(log.ToString() + "\t" + msg);
            Console.WriteLine(temp);
        }
        #endregion

        #region 시작 끝(네트워크 연결, 종료)
        public bool Load()
        {
            bool ret = client.Start();

            return ret;
        }
        public void FormClosed()
        {
            client.Stop();
        }
        #endregion 

        #region 1. 메시지 전송 및 수신
        public void BoardInsert(string name, string boardname, string write, DateTime date)
        {
            string packet = Packet.BoardInsert(name, boardname, write, date);
            client.SendData(packet);
        }
        public void BoardInsert_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            Form1.BoardInsert_Ack(sp[0]);
        }

        public void BoardPrintAll()
        {
            string packet = Packet.BoardPrintAll();
            client.SendData(packet);
        }
        public void BoardPrintAll_Ack(string msg)
        {
            string[] sp = msg.Split('@');

            for(int i = 0; i < sp.Length -1; i++)
            {
                string[] sp1 = sp[i].Split('#');
                boards.Add(new Board(sp1[0], sp1[1], sp1[2], DateTime.Parse(sp1[3])));
            }
            Form1.BoardPrintAll_Ack();
            boards.Clear();
        }

        public void BoardSelect(string boardname)
        {
            string packet = Packet.BoardSelect(boardname);
            client.SendData(packet);
        }
        public void BoardSelect_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            Form1.BoardSelect_Ack(bool.Parse(sp[0]), sp[1], sp[2], sp[3], DateTime.Parse(sp[4]));
        }





        public void BoardUpdate(string boardname, string changename, DateTime date)
        { 
            string packet = Packet.BoardUpdate(boardname, changename, date);
            client.SendData(packet);
        }
        public void BoardUpdate_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            Form1.BoardUpdate_Ack(bool.Parse(sp[0]));
        }
        public void BoardDelete(string boardname)
        {
            string packet = Packet.BoardDelete(boardname);
            client.SendData(packet);
        }
        public void BoardDelete_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            Form1.BoardDelete_Ack(bool.Parse(sp[0]));
        }
        #endregion
    }
}
