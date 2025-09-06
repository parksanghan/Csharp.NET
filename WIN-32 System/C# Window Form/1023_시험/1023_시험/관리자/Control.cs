using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _1023_시험
{
    internal class Control
    {
        const string SERVER_IP = "220.90.179.75";
        const int SERVER_PORT = 7000;
        private Client client = null;

        public Form1 FmControl
        {
            get;
            set;
        }

        #region 싱글톤(생성자에서 파일로드)
        public static Control Instance { get; private set; } = null;
        static Control()
        {
            Instance = new Control();
        }
        private Control()
        {
            
        }
        #endregion

        #region 네트워크 관련 코드 - Callback
        public void RecvDel(Socket s, string msg)
        {
            string[] sp = msg.Split('\a');
            if (sp[0] == "drinkinsert")
                Savedrink_Ack(sp[1]);
            else if (sp[0] == "drinkprintall")
                PrintAlldrink_Ack(sp[1]);
            else if (sp[0] == "drinkselect")
                Selectdrink_Ack(sp[1]);
            else if (sp[0] == "drinkupdate")
                Updatedrink_Ack(sp[1]);
            else if (sp[0] == "drinkdelete")
                Deletedrink_Ack(sp[1]);
        }
        #endregion

        #region 기능 관련 메서드 & 기능 관련 응답 메서드

        public bool ServerIn()
        {
            client = new Client(SERVER_IP, SERVER_PORT, RecvDel);
            return client.Start();
        }

        public void ServerOut()
        {
            client.Stop();
            GC.SuppressFinalize(this);
        }

        public void Savedrink(string name, string drname, int price)
        {
            string packet = Packet.Save(name, drname, price);
            client.SendData(packet);
        }
        public void Savedrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            FmControl.Check_bool(bool.Parse(sp[0]));
        }
       
        public void PrintAlldrink(string name)
        {
            string packet = Packet.PrintAll(name);
            client.SendData(packet);
        }

        public void PrintAlldrink_Ack(string msg)
        {
            string[] sp = msg.Split('@');
            FmControl.label2.Text = string.Format("저장개수 : {0}개", sp.Count() - 1);
            FmControl.listBox1.Items.Clear();
            for (int i = 0; i < sp.Count() - 1; i++)
            {
                string[] sp2 = sp[i].Split('#');
                string Form = string.Format("[{0}] {1} {2}", sp2[0], sp2[1], sp2[2]);
                FmControl.listBox1.Items.Add(Form);
            }
        }

        public void Selectdrink(string name, string drname)
        {
            string packet = Packet.Select(name, drname);
            client.SendData(packet);
        }

        public void Selectdrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
            {
                FmControl.label3.Text = "검색 성공";
                FmControl.textBox4.Text = sp[1];
                FmControl.textBox7.Text = sp[2];
                FmControl.textBox6.Text = sp[3];
            }
            else
                FmControl.label3.Text = "검색 실패";
        }

        public void Updatedrink()
        {
            string packet = Packet.Update(FmControl.textBox4.Text, FmControl.textBox7.Text, int.Parse(FmControl.textBox6.Text));
            client.SendData(packet);
        }

        public void Updatedrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
            {
                FmControl.label4.Text = "수정 성공";
            }
            else
            {
                FmControl.label4.Text = "수정 실패";
            }
            
        }

        public void Deletedrink()
        {
            string packet = Packet.Delete(FmControl.textBox4.Text, FmControl.textBox7.Text);
            client.SendData(packet);
        }

        public void Deletedrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
            {
                FmControl.label4.Text = "삭제 성공";
            }
            else
            {
                FmControl.label4.Text = "삭제 실패";
            }
        }
        #endregion
    }
}
