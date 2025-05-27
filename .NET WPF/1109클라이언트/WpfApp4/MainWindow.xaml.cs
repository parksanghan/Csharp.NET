using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
 
namespace WpfApp4
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public Model.Books blist = null;
        ViewModel.Manager manger = ViewModel.Manager.Instance;
        
        public MainWindow()
        {
            InitializeComponent();
            manger.win = this;
            Validation.AddErrorHandler(title, ValidationError);
            Validation.AddErrorHandler(price, ValidationError);
            Validation.AddErrorHandler(writer, ValidationError);
            Validation.AddErrorHandler(publisher, ValidationError);
            
        }
        public void setlist(Model.Books books)
        {
            blist = (Model.Books)FindResource("books");

            Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                blist.Clear();
                foreach (Model.Book bk in books)
                {
                    blist.Add(bk);

                }

            }));



        }

        void ValidationError(object sender, ValidationErrorEventArgs e)
        {
            Control con = sender as Control;
            con.ToolTip = (string)e.Error.ErrorContent;
        }
        //private void delete_Click(object sender, RoutedEventArgs e)
        //{
        //      blist = (Books)FindResource("books");
        //    if (mylistbox.SelectedIndex >= 0)
        //    {
        //        int idx = mylistbox.SelectedIndex;
        //        string packet = Packet.deleteack(idx);
        //        manger.client.SendData(packet);
        //        blist.RemoveAt(mylistbox.SelectedIndex);
        //    }

        //}

        //private void update_Click(object sender, RoutedEventArgs e)
        //{
        //      blist = (Books)FindResource("books");
        //    Book book = (Book)FindResource("book");
        //    int idx =blist.IndexOf(book);
        //    int i = book.Price;
        //    Book temp = book;
            
        //        blist.Remove(book);
            
       
            
        //    temp.Price = i;
   
        //    blist.Insert(idx, temp);
        //    string packet = Packet.updateeack(idx,i);
        //    manger.client.SendData(packet);
        //}

        //private void updateall_Click(object sender, RoutedEventArgs e)
        //{
             
        //    blist = (Books)FindResource("books");
        //    Book book = (Book)FindResource("book");
            
        //    foreach (Book bk in blist)
        //    {
        //        int idx = blist.IndexOf(book);
        //        int i = book.Price;
        //        Book temp = book;

        //        blist.Remove(book);
        //        temp.Price = i;
        //        blist.Insert(idx, temp);
              
        //    }
        //}
        //private void updateal1l_Click(object sender, RoutedEventArgs e)
        //{
        //      blist = (Books)FindResource("books");
        //    Book book = (Book)FindResource("book");
        //    string st = book.Title;
        //    int prr = book.Price;
        //    Book temp = (Book)blist.Where(m => m.Title == st);
        //    temp.Price = prr;

        //    var que = 
        //        from booke in blist
        //        where booke.Title == st
        //        select booke;

        //    foreach (Book bj in que)
        //    {
        //        bj.Price = prr;
        //    }





        //}


        //private void insert_Click(object sender, RoutedEventArgs e)
        //{
           
        //      blist = (Books)FindResource("books");
        //    Book book = (Book)FindResource("book");
        //    //Book booke = new Book() { book.Title, book.Publisher, book.Writer, book.Price };

        //    blist.Add(book);

        //    string packet = Packet.insertack(book.Title, book.Publisher, book.Writer, book.Price);
        //    manger.client.SendData(packet);
        //}

        //private void prev_Click(object sender, RoutedEventArgs e)
        //{
        //    ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("books"));
        //    view.MoveCurrentToPrevious();
        //    if (view.IsCurrentBeforeFirst)
        //    {
        //        view.MoveCurrentToFirst();  // 0인덱스에서 더 이전으로 갈시 다시 처음으로 즉 0 번째로 이동 
        //    }
        //}

        //private void next_Click(object sender, RoutedEventArgs e)
        //{

        //    ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("books"));
        //    view.MoveCurrentToNext();  // 다음으로 이동 
        //    if (view.IsCurrentAfterLast)
        //    {
        //        view.MoveCurrentToLast(); // 마지막 위치에서 다음으로 갈 시 마지막 인덱스로  이동 
        //    }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (manger.Load())
            {
                this.Title = "연결성공";
                string packet = Packet.printallack();
                manger.client.SendData(packet); 
            }
            else
            {
                this.Title = "연결실패";

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            manger.FormClosed();
        }
    }
}
