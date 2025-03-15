using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1025_계좌실습
{
    public partial class Form1 : Form
    {
        private Control control = Control.Instance;

        public Form1()
        {
            InitializeComponent();
        }

        #region FormLoad & Closed 이벤트 핸들러
        private void Form1_Load(object sender, EventArgs e)
        {
            if (control.Load() == true)
                this.Text = "서버 연결";
            else
                this.Text = "서버 연결 실패";            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (control.FormClosed() == true)
                this.Text = "서버연결 종료";
        }
        #endregion

        //계좌개설
        private void button1_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox1.Text);
            string name = textBox2.Text;
            int balance = int.Parse(textBox3.Text);
            if (control.Account_Insert(accid, name, balance) == true)
                button3_Click(sender, e);   //*****************
            else
                MessageBox.Show("실패");
        }

        //계좌삭제
        private void button2_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox4.Text);
            if (control.Account_Delete(accid) == true)
                button3_Click(sender, e);   //*****************
            else
                MessageBox.Show("실패");
        }

        //전체출력
        private void button3_Click(object sender, EventArgs e)
        { 
            List<Account> accounts = control.Account_SelectAll();

            label5.Text = String.Format("저장개수 {0}개", accounts.Count);

            listBox1.Items.Clear();
            foreach (Account account in accounts)
            {
                listBox1.Items.Add(account.Id);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = listBox1.SelectedItem.ToString();
        }

        //계좌검색
        private void button4_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox4.Text);
            Account acc = control.Accont_Select(accid);
            if (acc == null)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else
            {
                textBox1.Text = acc.Id.ToString();
                textBox2.Text = acc.Name;
                textBox3.Text = acc.Balance.ToString();
            }
        }
    }
}
