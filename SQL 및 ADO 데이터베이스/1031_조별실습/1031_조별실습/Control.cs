using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1031_조별실습
{
    internal class Control
    {
        Action<Socket, string> del = null;// => template 기반의 대리자 
        private List<Data> list = new List<Data>();
        public List<Data> List { get { return list; } }
        private Server server = null;
        private DB db = null;
        public DB DB { get { return db; } }
        
        public void RecvDel(Socket s, string msg)
        {

            string[] sp = msg.Split('\a');
            if (sp[0] == "insertmessage")
            {
                InsertAck(s, sp[1]);
            }
            else if (sp[0] == "selectmessage")
            {
                SelectAck(s, sp[1]);
            }
            else if (sp[0] == "selectallmessage")
            {
                selectAllAck(s, sp[1]);
            }
            else if (sp[0] == "deletemessage")
            {
                deleteack(s, sp[1]);
            }
            else if (sp[0] == "updatemessage")
            {
                updateack(s, sp[1]);
            }


        }
        #region update
        public void updateack(Socket s , string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                string extitle = sp[0];
                string title = sp[1];
                string link = sp[2];
                string image = sp[3];
                string author = sp[4];
                int discount = int.Parse(sp[5]);
                string publisher = sp[6];
                string pudate = sp[7];
                long isbn = int.Parse(sp[8]);
                string descriptopn = sp[9];
                if (db.Data_Update(extitle, new Data(title, link, image, author, discount, publisher,
                     pudate, isbn, descriptopn)) == true)
                {
                    string pakcet = Packet.Update_Message(true);
                    server.SendData(s,pakcet);

                }
                else
                {
                    string pakcet = Packet.Update_Message(false);
                    server.SendData(s, pakcet);
                }
               
            }
            catch
            {
                string pakcet = Packet.Update_Message(false);
                server.SendData(s, pakcet);
            }
        }
        #endregion
        #region insertack
        public void InsertAck(Socket s,string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                string title = sp[0];
                string link = sp[1];
                string image = sp[2];
                string author = sp[3];
                int discount = int.Parse(sp[4]);
                string publisher = sp[5];
                string pudate = sp[6];
                long isbn = int.Parse(sp[7]);
                string descriptopn = sp[8];
                if (db.Data_Insert(new Data(title, link, image, author, discount, publisher,
                     pudate, isbn, descriptopn)) == true)
                {
                   
                    string packet = Packet.Insert_Message(true);
                    server.SendData(s, packet);
                }
                else
                {
                    string packet = Packet.Insert_Message(false);
                    server.SendData(s, packet);
                }
            }
            catch {
                string packet = Packet.Insert_Message(false);
                server.SendData(s, packet);
            }
        }
        #endregion
        #region select
        public void SelectAck(Socket s , string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                string title = sp[0];
                Data book = db.Data_Select(title);
                Thread.Sleep(2000);
                string packet = Packet.Select_Message(true, book);
                server.SendData(s, packet);
            }
            catch {
                string packet = Packet.Select_Message(false);
                server.SendData(s, packet); }

        }
        #endregion
        #region selectall
        public void selectAllAck(Socket s, string msg)
        {
            try
            {
                List<Data> books = db.Data_SelectAll();
                string packet = Packet.SelectAll_Message( books);
                server.SendData(s, packet);
            }
            catch 
            
            {
                string packet = Packet.SelectAll_Message(null);
                server.SendData(s, packet);
            }
        }
        public void deleteack(Socket s , string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                string title = sp[0];
                if (db.Data_Delete(title) == true)
                {
                    string packet = Packet.Delete_MEssage(true);
                    server.SendData(s,packet);
                }
                else
                {
                    string packet = Packet.Delete_MEssage(false);
                    server.SendData(s, packet);
                }
            }
            catch {
                string packet = Packet.Delete_MEssage(false);
                server.SendData(s, packet);
            }
        }
        #endregion
        #region 싱글톤
        static public Control Singleton { get; private set; }
        static Control()
        {
            Singleton = new Control();
        }
        private Control()
        {
            db = new DB();
        }
        #endregion

        #region 연결과 해제
        public bool Load()
        {
            server = new Server(7000, RecvDel);
            if (db.Connect()&&server.Start())
                return true;
            else
                return false;
        }
        public bool FormClosed()
        {
            if (db.Close())
                return true;
            else
                return false;
        }

        #endregion

        #region 메서드
        //response
        public string Search(string text)
        {
            string str =  API.Search(text);

            return str; 
        }

        //parse
        public List<Data> Parse(string text)
        {
            list = API.Parse(text);
       
            DB.Data_Insert(list);
 
            return list;
        }
        #endregion
    }
}
