using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

/*
void fun(int); 
Action<int> del = fun;
[사용]
textBox1.Invoke(del, new object[] {10});  // fun(10);

void fun1(int n, char ch);
Action<int, char> del1 = fun1;
[사용]
textBox1.Invoke(del1, new object[]{10, 'A'});  //fun1(10, 'A');
*/

//해결1) 
/*
1) UI접근하는 함수를 별도로 구현 
   SetText
2) 아래코드를 이용하여 호출
    Action : 템플릿형태의 대리자

    Action<string> del = SetText;
    textBox1.Invoke(del, new object[] { "AAA" });
*/
namespace _1013
{
    public partial class Form2 : Form
    {
        public Form2()
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
            if (textBox1.InvokeRequired) //크로스스레드 상황인가?
            {
                Action<string> del = SetText;
                textBox1.Invoke(del, new object[] { "AAA" });
            }
            else
            {
                textBox1.Text = "AAA";
            }
        }

        public void SetText(string msg)
        {
            textBox1.Text = msg;
        }
    }
}
