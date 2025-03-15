using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _231108_도서관리프로그램
{
    public class BookList : ObservableCollection<Book>
    {
        public BookList()
        {
            Add(new Book() { Title = "책먹는 여우", Price = 9900, Author = "프란치스카 비어만", Publisher = "주니어김영사" });
            Add(new Book() { Title = "애린 왕자", Price = 10000, Author = "생텍쥐페리", Publisher = "이팝" });
        }
    }
}
