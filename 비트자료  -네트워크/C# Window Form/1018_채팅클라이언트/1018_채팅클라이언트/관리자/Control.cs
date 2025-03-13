using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1018_채팅클라이언트
{
    internal class Control
    {
        private Client client = null;
        const string SERVER_IP  = "220.90.179.75";
        const int SERVER_PORT   = 7000;

        //Form_Main의 Load메서드에서 처리
        public Form_Main FormMain { get; set;  } = null;


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
            if (sp[0] == "shortmessage") 
                ShortMessage_Ack(sp[1]);
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
   
        #endregion
    }
}
