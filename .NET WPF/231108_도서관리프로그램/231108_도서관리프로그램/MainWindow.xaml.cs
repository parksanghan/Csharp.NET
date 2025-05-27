using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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

namespace _231108_도서관리프로그램
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Validation.AddErrorHandler(title, ValidationError);
            Validation.AddErrorHandler(price, ValidationError);
            Validation.AddErrorHandler(price2, ValidationError);
            Validation.AddErrorHandler(author, ValidationError);
            Validation.AddErrorHandler(publisher, ValidationError);
        }

        #region 예외 처리
        void ValidationError(object sender, ValidationErrorEventArgs e)
        {
            Control con = sender as Control;
            con.ToolTip = (string)e.Error.ErrorContent;
        }
        #endregion

        #region 버튼 처리
        private void insert_Click(object sender, RoutedEventArgs e)
        {
            BookList booklist = (BookList)FindResource("booklist");
            Book books = (Book)FindResource("book");

            Book book = new Book() { Title = books.Title, Price = books.Price, Author = books.Author, Publisher = books.Publisher };
            booklist.Add(book);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            BookList blist = (BookList)FindResource("booklist");
           
            if (listbox.SelectedIndex >= 0)
                blist.RemoveAt(listbox.SelectedIndex);

            int id = listbox.SelectedIndex;
            // 패킷전송 -- 인덱스 보내서 그거 없앰
        }   
        #endregion
    }
}
