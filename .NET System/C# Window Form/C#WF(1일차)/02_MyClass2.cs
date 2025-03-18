//MyClass2.cs
using System;
using System.Windows.Forms;

namespace _1016_Form1
{
    //이벤트 처리용 클래스 (내가 직접 작성)
    internal partial class MyClass1 : Form
    {
        public MyClass1()
        {
            Design();
        }

        private void MyClass1_MouseMove(object sender, MouseEventArgs e)
        {
            string msg = string.Format("{0}, {1}", e.X, e.Y);
            this.Text = msg; 
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            label1.Text = txt_box1.Text; 
        }
    }
}
