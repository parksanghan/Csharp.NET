using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1018_Wb_Client;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Diagnostics;
using System.Xml.Linq;

namespace 음료수_관리_프로그램__시험_
{
    internal class DrinkControl
    {
        public Main Form_Main = null;
        private Client client = null;
        const int SERVER_PORT = 7000;
        private List<Drink> drinks = new List<Drink>();
        public List<Drink> Drinks { get { return drinks; } }

        #region 싱글톤 패턴
        public static DrinkControl Singleton { get; private set; } = null;

        static DrinkControl()
        {
            Singleton = new DrinkControl();

        }

        private DrinkControl()
        {
            client = new Client("220.90.179.75", SERVER_PORT, RecvDel, LogDel);
        }
        #endregion

        #region Callback
        void RecvDel(Socket s, string msg)
        {
            //msg : pack_shortmessage\a이름#메시지
            string[] sp = msg.Split('\a');
            if (sp[0] == "drinkinsert")
                InsertDrink_Ack(sp[1]);
            else if (sp[0] == "drinkprintall")
                PrintallDrink_Ack(sp[1]);
            else if (sp[0] == "drinkselect")
                SelectDrink_Ack(sp[1]);
            else if (sp[0] == "drinkupdate")
                UpdateDrink_Ack(sp[1]);
            else if (sp[0] == "drinkdelete")
                DeleteDrink_Ack(sp[1]);

        }

        void LogDel(Socket s, Log log, string msg)
        {
            string temp = string.Format(log.ToString() + "\t" + msg);
            Console.WriteLine(temp);
        }
        #endregion

        #region 서버연결/해제 메서드
        public bool Connect()
        {
            if (client.Start() == true)
                return true;
            else
                return false;
        }

        public void Disconnect()
        {
            client.Stop();
        }
        #endregion

        public void InsertDrink(string myname ,string d_name, int price)
        {
            string packet = Packet.DrinkInsert(myname, d_name, price);
            client.SendData(packet);
        }

        public void InsertDrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
            {
                Form_Main.insertDrink_Label("저장 성공");
            }
            else
            {
                Form_Main.insertDrink_Label("저장 실패");
            }
        }

        public void PrintallDrink(string myname)
        {
            string packet = Packet.DrinkPrintall(myname);
            client.SendData(packet);
        }

        public void PrintallDrink_Ack(string msg)
        {
            string[] sp = msg.Split('@');
            for(int i=0; i<sp.Length-1; i++)
            {
                string[] sp1 = sp[i].Split('#');
                drinks.Add(new Drink(sp1[0], sp1[1], int.Parse(sp1[2])));
            }

            Form_Main.PrintallDrink();
            Form_Main.PrintallDrinkLabel();
            drinks.Clear();
        }

        public void SelectDrink(string myname, string d_name)
        {
            string packet = Packet.DrinkSelect(myname, d_name);
            client.SendData(packet);
        }

        public void SelectDrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
            {
                Form_Main.SelectPricePrint(sp[1],sp[2],sp[3]);
                Form_Main.SelectDrink_Label("검색 성공");
            }
            else
            {
                Form_Main.SelectDrink_Label("검색 실패");
            }
        }

        public void UpdateDrink(string myname, string d_name, int price)
        {
            string packet = Packet.DrinkUpdate(myname, d_name, price);
            client.SendData(packet);
        }

        public void UpdateDrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
            {
                Form_Main.Update_Delete_Label("수정 성공");
            }
            else
            {
                Form_Main.Update_Delete_Label("수정 실패");
            }
        }

        public void DeleteDrink(string myname, string d_name)
        {
            string packet = Packet.DrinkDelete(myname, d_name);
            client.SendData(packet);
        }

        public void DeleteDrink_Ack(string msg)
        {
            string[] sp = msg.Split('#');
            if (bool.Parse(sp[0]) == true)
            {
                Form_Main.Update_Delete_Label("삭제 성공");
            }
            else
            {
                Form_Main.Update_Delete_Label("삭제 실패");
            }
        }
    }
}
