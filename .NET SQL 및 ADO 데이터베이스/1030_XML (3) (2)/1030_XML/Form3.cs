using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace _1030_XML
{    

    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        //Book 파싱(p34~35)
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = WbXMLSample.ReadXml("data2.xml");

            List<Book> books = new List<Book>();
            XmlReader reader = XmlReader.Create("data2.xml");
            reader.MoveToContent(); 
            while (reader.Read())
            {
                if (reader.IsStartElement("book"))
                {
                    Book book = Book.MakeBook(reader);
                    books.Add(book);
                }
            }

            listBox1.DataSource = books;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = WbXMLSample.ReadXml("data3.xml");

            List<Book> books = new List<Book>();
            XmlReader reader = XmlReader.Create("data3.xml");
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement("book"))
                {
                    Book book = Book.MakeBook1(reader);
                    books.Add(book);
                }
            }

            listBox2.DataSource = books;

        }
    }

    public class Book
    {
        internal string Title { get; private set; }
        internal int Price { get; private set; }
        internal int Count { get; private set; }

        //요소
        internal static Book MakeBook(XmlReader xr)
        {
            //하위 요소 이동("title")
            xr.ReadToDescendant("title");
            //title이 가지고 있는 value
            string title = xr.ReadElementString("title");

            //형제 요소 이동("가격")
            xr.ReadToNextSibling("가격");
            //"가격이  가지고 있는 value
            int price = int.Parse(xr.ReadElementString("가격"));

            //형제 요소 이동("수량")
            xr.ReadToNextSibling("수량");
            //"가격이  가지고 있는 value
            int count = int.Parse(xr.ReadElementString("수량"));

            return new Book(title, price, count);
        }

        //요소(값 + 특성)
        internal static Book MakeBook1(XmlReader xr)
        {
            //int attr_cnt = xr.AttributeCount;

            //1. 첫번째 방법
            string title = xr[0];
            int price = int.Parse(xr[1].ToString());
            int count = int.Parse(xr[2].ToString());    

            //2. 두번째 방법
            string title1   = xr.GetAttribute("title");
            int price1      = int.Parse(xr.GetAttribute("가격"));
            int count1      = int.Parse(xr.GetAttribute("수량"));           

            return new Book(title, price, count);
        }


        Book(string title, int price, int count)
        {
            Title = title;
            Price = price;
            Count = count;
        }

        public override string ToString()
        {
            return Title + "\t" + Price + "\t" + Count;
        }
    }
}
