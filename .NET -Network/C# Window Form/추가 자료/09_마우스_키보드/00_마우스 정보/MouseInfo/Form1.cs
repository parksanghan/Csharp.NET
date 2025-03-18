using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MouseInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.button1.MouseEnter += new EventHandler(this.button_MouseEnter);
            this.button2.MouseEnter += new EventHandler(this.button_MouseEnter);
            this.button1.MouseMove  += new MouseEventHandler(this.button1_MouseMove);
            this.button1.MouseHover += new EventHandler(this.button1_MouseHover);
            this.button1.MouseDown  += new MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseWheel += new MouseEventHandler(this.button1_MouseWheel);
            this.button1.MouseUp    += new MouseEventHandler(this.button1_MouseUp);
            this.button1.MouseLeave += new EventHandler(this.button1_MouseLeave);


            this.Click += new EventHandler(Form1_Click);
            this.DoubleClick += new EventHandler(Form1_DoubleClick);
        }


        // 마우스 정보 ㅊ
        private void button_MouseEnter(object sender, System.EventArgs e)
        {
            Console.WriteLine("{0} 컨트롤에서 MouseEnter 이벤트가 발생함!", sender);

            if((Button)sender == button1)
                Console.WriteLine("[1]MouseEnter Event => Button1 에서 발생");
            else
                Console.WriteLine("[1]MouseEnter Event => Button2 에서 발생");
        }

        private void button1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine("[2]MouseMove Event");
        }

        private void button1_MouseHover(object sender, System.EventArgs e)
        {
            Console.WriteLine("[3]MouseHover Event");
        }

        private void button1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine("[3]MouseDown Event");
            Console.WriteLine("X좌표:{0}, Y좌표:{1}", e.X, e.Y);
            Console.WriteLine("마우스버튼:{0}, 마우스위치:{1}", e.Button, e.Location);            
        }

        private void button1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine("[3]MouseWheel Event");
            Console.WriteLine("마우스휠 Delta:{0}", e.Delta);        }

        private void button1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine("[4]MouseUp Event");
        }

        private void button1_MouseLeave(object sender, System.EventArgs e)
        {
            Console.WriteLine("[5]MouseLeave Event");
        }

        // 폼에서 클릭
        private void Form1_Click(object sender, System.EventArgs e)
        {
            Console.WriteLine("폼에서 클릭!");
        }

        // 폼에서 더블 클릭
        private void Form1_DoubleClick(object sender, System.EventArgs e)
        {
            Console.WriteLine("폼에서 더블 클릭!");
        }      



        // 마우스 정보 출력 버튼 핸들러
        private void button2_Click(object sender, EventArgs e)
        {
            // 마우스 정보            
            String str = String.Format(" 휠 마우스 설치:{0} \n 마우스 버튼 개수:{1} \n 버튼Swap:{2} \n 마우스속도:{3} \n 휠델타값:{4} \n 휠라인수:{5} \n 더블클릭 시간:{6}ms \n 픽셀단위 영역:{7}",
                           SystemInformation.MouseWheelPresent,
                           SystemInformation.MouseButtons,
                           SystemInformation.MouseButtonsSwapped,
                           SystemInformation.MouseSpeed,
                           SystemInformation.MouseWheelScrollDelta,
                           SystemInformation.MouseWheelScrollLines,
                           SystemInformation.DoubleClickTime,
                           SystemInformation.DoubleClickSize
                        );
            MessageBox.Show(str);
            
        }

        // 마우스 커서 변경 버튼 핸들러 
        static bool cursor_onoff = false;
        private void button3_Click(object sender, EventArgs e)
        {
            // 마우스 커서 바꾸기            
            if(cursor_onoff)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                cursor_onoff = false;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                cursor_onoff = true;
            }

        }

        // 컨트롤 속성 출력
        /*
         * Control클래스의 static 속성
         * 
         * 속성             형              설명
         * ---------------------------------------------------------------------------
         * MousePosition    Point           마우스의 현재 위치를 화면 좌표로 반환
         * MouseButton      MouseButtons    어느 마우스 버튼이 눌렸는지 반환
         * ModifierKeys     Keys            눌려진 보조키(Shift, Ctrl, Alt) 값을 반환
         */
        private void button4_Click(object sender, EventArgs e)
        {
            // Control 속성
            if((Control.ModifierKeys == Keys.Shift) )               
            {
                // 마우스 포인터의 위치를 화면에 출력
                Point point = Control.MousePosition;
                MouseButtons mouse = Control.MouseButtons;                

                MessageBox.Show("마우스 버튼 위치:" + point);
                ((Control)sender).Hide();
                MessageBox.Show("마우스 버튼 :" + mouse);
                ((Control)sender).Show();
            }
        }

        
    }
}