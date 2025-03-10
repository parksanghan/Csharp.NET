using System;
using System.Threading;
using System.Windows.Forms;

/*
[크로스 스레드]
특정 스레드에서 생성한 UI객체들은 생성한 스레드에서만 접근 가능!

- 일반적인 상황 : Primary Thread -> UI객체 생성 
 */ 
namespace _1013
{
    //크로스 스레드 문제 발생
    //정상적으로 동작하는 것처럼 보이지만 실행 도중 오류 발생(렌덤)
    //디버깅하면 확인이 됨.
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(SetNameThread);
            tr.Start();
        }

        private void SetNameThread()
        {
            textBox1.Text = "AAA";
        }
    }
}
