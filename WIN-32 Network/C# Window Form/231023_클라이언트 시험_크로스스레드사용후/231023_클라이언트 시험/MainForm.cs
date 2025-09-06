using _231018_WBNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _231023_클라이언트_시험
{
    public partial class MainForm : Form
    {
        const string IP = "220.90.179.75";
        const int PORT = 7000;

        public Client client;

        void RecvDel(string msg)
        {
            string[] sp = msg.Split('\a');
            switch (sp[0])
            {
                case Packet.PACK_ADD:
                    AddResult(sp[1]);
                    break;
                case Packet.PACK_SELECT:
                    SelectResult(sp[1]);
                    break;
                case Packet.PACK_UPDATE:
                    UpdateResult(sp[1]);
                    break;
                case Packet.PACK_DELETE:
                    DeleteResult(sp[1]);
                    break;
                case Packet.PACK_PRINTALL:
                    PrintAllResult(sp[1]);
                    break;
            }
        }

        void LogDel(LogType log, string msg) { }

        public MainForm()
        {
            InitializeComponent();
            client = new Client(IP, PORT, RecvDel, LogDel);
        }


        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (client.Start())
            {
                MessageBox.Show("연결 성공");
            }
            else 
            {
                MessageBox.Show("연결 실패");
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            client.Stop();
            MessageBox.Show("연결 종료 상태");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string packet = Packet.PacketAdd(textBoxUserName.Text, textBoxName.Text, Int32.Parse(textBoxPrice.Text));

            client.SendData(packet);
        }

        private void buttonPrintAll_Click(object sender, EventArgs e)
        {
            string packet = Packet.PacketPrintAll(textBoxUserName.Text);

            client.SendData(packet);
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {

            string packet = Packet.PacketSelect(textBoxUserName.Text, textBoxSelectName.Text);

            client.SendData(packet);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

            string packet = Packet.PacketUpdate(textBoxUserName.Text, textBoxSelectResultName.Text, Int32.Parse(textBoxSelectResultPrice.Text));

            client.SendData(packet);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            string packet = Packet.PacketDelete(textBoxUserName.Text, textBoxSelectResultName.Text);

            client.SendData(packet);
        }




        internal void AddResult(string msg)
        {
            string[] sp = msg.Split('#');

            bool isSucceed = bool.Parse(sp[0]);

            Action<bool> del = (isSucceed1) =>
            {
                labelAddResult.Text = isSucceed ? "저장 성공" : "저장 실패";
            };
            labelAddResult.Invoke(del, new object[] { isSucceed });
        }
        internal void PrintAllResult(string msg)
        {
            string[] sp = msg.Split('@');

            Action<int> del = (count) =>
            {
                labelPrintAllCount.Text = "저장개수 : " + count  +"개";
            };
            labelAddResult.Invoke(del, new object[] { sp.Length - 1 });

            List<Drink> drinks = new List<Drink>();
            for (int i = 0; i < sp.Length - 1; i++) 
            {
                string[] spSub = sp[i].Split('#');

                drinks.Add(new Drink(spSub[0], spSub[1], Int32.Parse(spSub[2])));
            }

            Action<List<Drink>> del2 = (tdrinks) =>
            {
                listBoxPrintAll.Items.Clear();
                foreach (Drink dr in tdrinks) 
                {
                    listBoxPrintAll.Items.Add(dr.ToString());
                }
            };
            listBoxPrintAll.Invoke(del2, new object[] { drinks });

        }
        internal void SelectResult(string msg)
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);

            Action<bool> del = (isSucceed1) =>
            {
                labelSelectResult.Text = isSucceed1 ? "검색 성공" : "검색 실패";
            };
            labelSelectResult.Invoke(del, new object[] { isSucceed });

            if (isSucceed == false) return;

            Action<string> del2 = (name) => 
            {
                textBoxSelectResultName.Text = name;
            };
            textBoxSelectResultName.Invoke(del2, new object[]{ sp[2] });

            Action<int> del3 = (price) =>
            {
                textBoxSelectResultPrice.Text = price.ToString();
            };
            textBoxSelectResultPrice.Invoke(del3, new object[] { Int32.Parse(sp[3]) });
        }
        internal void UpdateResult(string msg)
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);

            Action<bool> del = (isSucceed1) =>
            {
                labelUpdateResult.Text = isSucceed1 ? "수정 성공" : "수정 실패";
            };
            labelUpdateResult.Invoke(del, new object[] { isSucceed });
        }
        internal void DeleteResult(string msg)
        {
            string[] sp = msg.Split('#');
            bool isSucceed = bool.Parse(sp[0]);

            Action<bool> del = (isSucceed1) =>
            {
                labelUpdateResult.Text = isSucceed1 ? "삭제 성공" : "삭제 실패";
            };
            labelUpdateResult.Invoke(del, new object[] { isSucceed });
        }

    }
}
