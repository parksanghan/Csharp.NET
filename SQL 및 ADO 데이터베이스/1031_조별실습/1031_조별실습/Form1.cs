using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1031_조별실습
{
	public partial class Form1 : Form
	{
		Control con = Control.Singleton;

		public Form1()
		{
			InitializeComponent();
		}

        #region 연결, 해제
        private void Form1_Load(object sender, EventArgs e)
        {
            if (con.Load())
                this.Text = "서버 연결";
            else
                this.Text = "서버 연결 실패";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (con.FormClosed())
                this.Text = "서버 연결 종료";
        }

        #endregion

        #region 메서드
        //검색
        private void button1_Click(object sender, EventArgs e)
		{
            listBox1.Items.Clear();

            string msg = con.Search(textBox1.Text);
		     List<Data> books  = con.Parse(msg);  

			foreach(Data d in books)
			{
				listBox1.Items.Add(d.Title);
			}
 
			textBox2.Text = msg;
			label2.Text = "검색 개수 : " + con.List.Count.ToString();
        }

        //제목 선택
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;

            Data book = con.List[idx];

            textBox3.Text = book.Description;
            pictureBox1.ImageLocation = book.Image;

			textBox4.Text = 
				"링    크 : " + book.Link + "\r\n" +
                "저    자 : " + book.Author + "\r\n" +
                "가    격 : " + book.Discount + "\r\n" +
                "출 판 사 : " + book.Publisher + "\r\n" +
                "출 판 일 : " + book.Pubdate + "\r\n" +
                "고유번호 : " + book.Isbn;
        }

        #endregion
    }
}
