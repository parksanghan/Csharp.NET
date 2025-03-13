//MyClass1.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace _1016_Form1
{
    //화면 UI 처리용 클래스 (자동 생성)
    internal partial class MyClass1
    {
        //1. 속성을 통한 초기화
        //2. 컨트롤 객체 생성
        //3. 이벤트 처리
        private TextBox txt_box1 = null;
        private Button button1 = null;
        private Label label1 = null;

       private void Design()
        {
            this.BackColor = Color.Turquoise;

            this.MouseMove += MyClass1_MouseMove;

            txt_box1 = new TextBox();
            txt_box1.Parent = this;
            txt_box1.SetBounds(10, 10, 100, 20);

            button1 = new Button();
            button1.Parent = this;
            button1.Text = "클릭!";
            button1.SetBounds(120, 10, 50, 20);
            button1.Click += Button1_Click;

            label1 = new Label();
            label1.Parent = this;
            label1.Text = "-";
            label1.SetBounds(10, 40, 100, 20);
        }        
    }
}
