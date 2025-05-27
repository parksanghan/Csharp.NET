using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    internal class Control
    {
        static public Database db = new Database();
        static Server server = null;
        public Control() 
        {

        }
        private const int SERVER_PORT = 7000;
        static Server _con = Server.GetInstance(SERVER_PORT, RecvDel, LogDel);

        static public List<Book> books = new List<Book>();
        public static Control Instance { get; private set; } = null;
        static Control()
        {
            Instance = new Control();
        }

        static void RecvDel(Socket s, string msg)
        {
            Console.WriteLine(msg);
            string[] sp = msg.Split('\a');
            if (sp[0] == "insert")
            {
                FunctionInsert(s,sp[1]);
            }
            else if (sp[0] == "delete")
            {
                FunctionDelete(s, sp[1]);
            }
            else if (sp[0] == "update")
            {
                FunctionUpdate(s, sp[1]);
            }
            else if (sp[0] == "printall")
            {
                FunctionAll(s);
            }
        }

        static public void FunctionInsert(Socket s, string msg)//title#publisher#writer#price
        {
            try
            {
                Console.WriteLine(msg);
                //파싱
                string[] sp = msg.Split('#');

                //데이터처리
                bool reuslt =insertbook(sp[0], sp[1], sp[2], int.Parse(sp[3]));

                // 전송
                string packet =Packet.insertack(reuslt);
                server.SendData(s, packet);

            }
            catch(Exception ex) { Console.WriteLine(ex.Message);
                string packet = Packet.insertack(false);
                server.SendData(s, packet);
            }
        }
        static public void FunctionDelete(Socket s, string msg)//index
        {
            try
            {
                Console.WriteLine(msg);
                //파싱
                string[] sp = msg.Split('#');

                //데이터처리
                int index = int.Parse(sp[0]);
                bool reuslt = deletebook(index);
                
                // 전송
                string packet = Packet.deleteack(reuslt);
                server.SendData(s, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.deleteack(false);
                server.SendData(s, packet);
            }
        }
  
        static public void FunctionUpdate(Socket s, string msg)//index#price
        {
            try
            {
                Console.WriteLine(msg);
                // 파싱 
                string[] sp = msg.Split('#');
                // 데이터 처리
                bool reuslt = updatebook(int.Parse(sp[0]), int.Parse(sp[1]));
                // 전송
                string packet = Packet.updateeack(reuslt);
                server.SendData(s, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string packet = Packet.updateeack(false);
                server.SendData(s, packet);
            }
        }
        static public void FunctionAll(Socket s)
        {
            try
            {
                List<Book> bos = requestlist();
                string packet = Packet.printallack(bos);

                server.SendData(s, packet);
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); 
                string packet = Packet.printallack(null);
                server.SendData(s, packet);
            }
        }
        static void LogDel(Socket s, Log log, string msg)
        {

            //string temp = string.Format(log.ToString() + "\t" + msg);
            //Console.WriteLine(temp);
        }
        public void Run()
        {
            try
            {
                server = new Server(SERVER_PORT, RecvDel, LogDel);
                server.Start();
                db.Connect();
                Console.Write("DB 연결 성공");
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        public void Dispose()
        {
            db.Close();
            server.Stop();
            GC.SuppressFinalize(this);
        }
        static public bool insertbook(string t , string p , string w , int pr)
        {
            try
            {
                Book book = new Book(t, p, w, pr);


                return db.insertqury(book); 
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); return false; }
         }
        static public bool deletebook(int index)
        {
            try
            {
                db.deletedata(index);   
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        static  public bool updatebook(int index, int prc)
        {
            try
            {
                db.updateitem(index, prc);
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }

        static public List<Book> requestlist()
        {
            try
            {
                return db.selectall();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);return null; }
        }

    }

}

