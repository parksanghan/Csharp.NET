//control.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _1018_Wb_Client;

namespace _1018_로그인채팅클라이언트
{
    internal class Control
    {
        private Client client = null;
        const string SERVER_IP = "220.90.179.75";
        const int SERVER_PORT = 8000;

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

        #region Callback
        public void RecvDel(Socket s, string msg)
        {
            string[] sp = msg.Split('\a');
            if (sp[0] == "memberjoin")
                MemberJoin_Ack(sp[1]);
            else if (sp[0] == "login")
                LogIn_Ack(sp[1]);
        }

        public void LogDel(Socket s, Log log, string msg)
        {
            string temp = string.Format(log.ToString() + "\t" + msg);
            Console.WriteLine(temp);
        }
        #endregion

        #region 기능 메서드
        public void MemberJoin(string id, string pw, string name)
        {
            string packet = Packet.MemberJoin(id, pw, name);
            client.SendData(packet);
        }

        public void MemberJoin_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
                MessageBox.Show("회원가입 성공");
            else
                MessageBox.Show("회원가입 실패");
        }

        public void LogIn(string id, string pw)
        {
            string packet = Packet.LogIn(id, pw);
            client.SendData(packet);
        }

        public void LogIn_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)  //Form_Run 모달 실행!
            {
                Form_Run dlg = new Form_Run();
                //부모->자식
                dlg.ShowDialog();                
            }
            else
                MessageBox.Show("로그인 실패");
        }
        #endregion
    }
}
