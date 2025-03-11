using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp4.ViewModel
{
    public class Manager
    {
        public static Manager Instance { get; private set; } = null;
        public Model.Books blist = null;
        public Model.Book model { get; set; }

        public Command btn_insert { get; set; }
        public Command btn_Delete { get; set; }

        public Command btn_Update { get; set; }

        public Command btn_SelectAll { get; set; }


        public Command btn_prev { get; set; }

        public Command btn_next { get; set; }
        // 모델객체를 생성해서 가지고 있음  => 아까 위의 메인 모델 
        // ="{Binding Model.Num1, 이거와 연결되어 바인딩됨

         public Manager()
        {
            client = new Client(SERVER_IP, SERVER_PORT, RecvDel, LogDel);
            btn_insert = new Command(Insert_Func);
            btn_Delete = new Command(Delete_Func);
            btn_Update = new Command(Update_Func);
            btn_SelectAll = new Command(SelectAll_Func);
            btn_prev = new Command(prev_Func);
            btn_next = new Command(next_Func);
            Instance = new Manager();
        }
        public void Insert_Func(object obj)
        {
          

             blist = (Model.Books)win.FindResource("books");
            Model.Book book = (Model.Book)win.FindResource("book");
            //Book booke = new Book() { book.Title, book.Publisher, book.Writer, book.Price };

            blist.Add(book);

            string packet = Packet.insertack(book.Title, book.Publisher, book.Writer, book.Price);
            client.SendData(packet);
        }
        public void Delete_Func(object obj)
        {
            blist = (Model.Books)win.FindResource("books");
            if (win.mylistbox.SelectedIndex >= 0)
            {
                int idx = win.mylistbox.SelectedIndex;
                string packet = Packet.deleteack(idx);
                 client.SendData(packet);
                blist.RemoveAt(win.mylistbox.SelectedIndex);
            }
        }
        public void Update_Func(object obj)
        {
            blist = (Model.Books)win.FindResource("books");
            Model.Book book = (Model.Book)win.FindResource("book");
            int idx = blist.IndexOf(book);
            int i = book.Price;
            Model.Book temp = book;

            blist.Remove(book);



            temp.Price = i;

            blist.Insert(idx, temp);
            string packet = Packet.updateeack(idx, i);
            client.SendData(packet);
        }
        public void SelectAll_Func(object obj)
        {
            string packet = Packet.printallack();
            client.SendData(packet);
        }

        public void prev_Func(object obj)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(win.FindResource("books"));
            view.MoveCurrentToPrevious();
            if (view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToFirst();  // 0인덱스에서 더 이전으로 갈시 다시 처음으로 즉 0 번째로 이동 
            }
        }
        public void next_Func(object obj)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(win.FindResource("books"));
            view.MoveCurrentToNext();  // 다음으로 이동 
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToLast(); // 마지막 위치에서 다음으로 갈 시 마지막 인덱스로  이동 
            }
        }
        #region
        public Client client = null;
        
        const string SERVER_IP = "127.0.0.1";
        const int SERVER_PORT = 7000;
        public MainWindow win = null;
        #endregion
        public void LogDel( Log log, string msg)
        {
            //string temp = string.Format(log.ToString() + "\t" + msg);
            //Console.WriteLine(temp);
        }
        #region 싱글톤
 
     
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
            if (sp[0] == "insertack")
            {
                FunctionInsert(s, sp[1]);
            }
            else if (sp[0] == "deleteack")
            {
                Functiondelete(s, sp[1]);
            }
            else if (sp[0] == "updateack")
            {
                FunctionUpdate(s, sp[1]);
            }
            else if (sp[0] == "printallack")
            {
                Model.Books books =  FunctionAll(sp[1]);
                win.setlist(books);
            }
        }
        public void FunctionInsert(Socket s, string msg)
        {
            string[] sp = msg.Split('#');
            bool result = bool.Parse(sp[0]);
            string meg = result ? "저장성공" : "저장실패";
            MessageBox.Show(msg);
        }
        public void Functiondelete(Socket s , string msg)
        {
            string[] sp = msg.Split('#');
            bool result = bool.Parse(sp[0]);
            string mess = result ? "저장성공" : "저장실패";
            MessageBox.Show(mess);
        }
        public void FunctionUpdate(Socket s ,string msg)
        {
            string[] sp = msg.Split('#');
            bool result = bool.Parse(sp[0]);
            string mess = result ? "저장성공" : "저장실패";
            MessageBox.Show(mess);

        }
        

        public Model.Books FunctionAll(string msg)
        {
            Model.Books books = new Model.Books();
            string[] sp = msg.Split('@');
            foreach (string  dd in sp)
            {

                if (dd==string.Empty) continue;
                string[] ssp  = dd.Split('#');
                string title = ssp[0];
                string pub = ssp[1];
                string writer = ssp[2];
                int pr = int.Parse(ssp[3]);
                books.Add(new Model.Book() { Title = title, Publisher = pub, Writer = writer, Price = pr });
            }
            return books;

        }

        public void LogDel(Socket s, Log log, string msg)
        {
            //string temp = string.Format(log.ToString() + "\t" + msg);
            //Console.WriteLine(temp);
        }
        #endregion






    }
}
