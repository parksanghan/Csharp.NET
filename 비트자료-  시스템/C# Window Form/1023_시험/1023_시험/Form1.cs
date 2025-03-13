using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1023_시험
{
    public partial class Form1 : Form
    {
        private Control _control = Control.Instance;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _control.FmControl = this;
        }

        //서버 연결
        private void button1_Click(object sender, EventArgs e)
        {
            if (_control.ServerIn() == true)
            {
                this.Text = "연결상태";
            }
            else
                this.Text = "연결 실패";
        }

        //서버 연결 해제
        private void button2_Click(object sender, EventArgs e)
        {
            _control.ServerOut();
            this.Text = "연결 종료상태";
        }

        //저장버튼
        private void button3_Click(object sender, EventArgs e)
        {
            _control.Savedrink(textBox1.Text, textBox2.Text, int.Parse(textBox5.Text));
        }

        //저장 실패 성공 확인
        public void Check_bool(bool res)
        {
            if (res == true)
            {
                label1.Text = "저장 성공";
            }
            else
                label1.Text = "저장 실패";
        }

        //전체출력
        private void button4_Click(object sender, EventArgs e)
        {
            _control.PrintAlldrink(textBox1.Text);
        }
        //검색
        private void button5_Click(object sender, EventArgs e)
        {
            _control.Selectdrink(textBox1.Text, textBox3.Text);
        }
        //수정
        private void button6_Click(object sender, EventArgs e)
        {
            _control.Updatedrink();
            _control.PrintAlldrink(textBox1.Text);
        }

        //삭제
        private void button7_Click(object sender, EventArgs e)
        {
            _control.Deletedrink();
            _control.PrintAlldrink(textBox1.Text);
        }
    }
}
