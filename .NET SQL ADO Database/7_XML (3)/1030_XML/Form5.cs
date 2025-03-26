using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace _1030_XML
{
    public partial class Form5 : Form
    {
        private List<MyBook> books = new List<MyBook>();    
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string xml_string   = BookSearch.Search(textBox1.Text);
            textBox2.Text       = xml_string;

            books = BookSearch.GetMyBooks(xml_string);
            listBox1.DataSource = books;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            MyBook book = books[idx]; ;
            pictureBox1.ImageLocation = book.Image;
            textBox3.Text = book.Description;
        }
    }

    public static class BookSearch
    {
        //검색키워드 -> 검색결과 XML문서 반환
        public static string Search(string msg)
        {
            string query = msg; // 검색할 문자열
            //string url = "https://openapi.naver.com/v1/search/book?query=" + query; // JSON 결과
            string url = "https://openapi.naver.com/v1/search/book.xml?query=" + query;  // XML 결과
            //url += " display=50";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "08GWcajQOjTXTlXSgmwV"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "Uwajo69VSe");       // 클라이언트 시크릿


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response. StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                return text;
            }
            else
            {
                return "Error 발생=" + status;
            }
        }
    
        //검색결과 XML문서 -> List<MyBook>
        public static List<MyBook> GetMyBooks(string xml_string)
        {
            XmlDocument doc = new XmlDocument();
            //doc.Load(query); //URL
            doc.LoadXml(xml_string); //XML문장

            XmlNode node = doc.SelectSingleNode("rss");
            XmlNode n = node.SelectSingleNode("channel");
 
            List<MyBook> books = new List<MyBook>();
            foreach (XmlNode el in n.SelectNodes("item"))
            {
                MyBook book = MyBook.MakeBook(el);
                books.Add(book);
            }
            return books;
        }
    }

    public class MyBook
    {
        internal string Title       { get; private set; }
        internal string Image       { get; private set;  }
        internal string Author      { get; private set; }
        internal int Discount       { get; private set; }
        internal string Publisher   { get; private set; }
        internal string Description { get; private set; }

        public MyBook(string t, string i, string a, int d, string p, string de)
        {
            Title = t;
            Image = i;
            Author = a;
            Discount = d;
            Publisher = p;
            Description = de;
        }

        //Dom
        public static MyBook MakeBook(XmlNode xn)
        {
            XmlNode title_node = xn.SelectSingleNode("title");
            string title = ConvertString(title_node.InnerText);

            XmlNode image_node = xn.SelectSingleNode("image");
            string image = ConvertString(image_node.InnerText);

            XmlNode author_node = xn.SelectSingleNode("author");
            string author = ConvertString(author_node.InnerText);

            XmlNode discount_node = xn.SelectSingleNode("discount");
            int discount = int.Parse(discount_node.InnerText);

            XmlNode publisher_node = xn.SelectSingleNode("publisher");
            string publisher = ConvertString(publisher_node.InnerText);

            XmlNode description_node = xn.SelectSingleNode("description");
            string description = ConvertString(description_node.InnerText);

            return new MyBook(title, image, author, discount, publisher, description);
        }
        
        private static string ConvertString(string str)
        {
            int sindex = 0;
            int eindex = 0;
            while (true)
            {
                sindex = str.IndexOf('<');
                if (sindex == -1)
                {
                    break;
                }
                eindex = str.IndexOf('>');
                str = str.Remove(sindex, eindex - sindex + 1);
            }
            return str;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
