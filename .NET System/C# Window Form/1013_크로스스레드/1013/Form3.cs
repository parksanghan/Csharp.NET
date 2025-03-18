using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//해결2) 
/*
1) UI접근하는 함수를 별도로 만들지 않고  람다식 사용
2) 아래코드를 이용하여 호출
    Action : 템플릿형태의 대리자

    Action<string> del = SetText;
    textBox1.Invoke(del, new object[] { "AAA" });
*/
namespace _1013
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //상황1) Primary Thread만 있는 상황
            //SetNameThread();

            //상황2) 2nd Thread를 생성해서 접근
            Thread tr = new Thread(SetNameThread);
            tr.Start();
        }

        private void SetNameThread()
        {
            if (textBox1.InvokeRequired)
            {
                MessageBox.Show("2nd에서 접근");
                Action<string> del = (msg) => { textBox1.Text = msg; };
                textBox1.Invoke(del, new object[] { "AAA" });
            }
            else
            {
                MessageBox.Show("기본스레드에서 접근");
                textBox1.Text = "AAA";
            }
        }
    }
}
