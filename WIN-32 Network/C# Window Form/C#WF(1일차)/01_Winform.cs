using System;
using System.Windows.Forms;

namespace _1016_Form1
{
    internal class Program
    {
        //메시지박스 출력
        static void Exam1()
        {
            DialogResult r =  MessageBox.Show("메시지출력", "타이틀", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.OK)
                Console.WriteLine("OK");
            else
                Console.WriteLine("Cancel");
            
        }
        
        //Applicaiton : 실행흐름
        //Forms       : 윈도우
        static void Exam2()
        {
            // Application : 응용프로그램의 시작 및 중지를 위한 메서드,
            //               윈도우 메시지 처리, 응용프로그램 정보를 가져오기 
            //               위한 속성 관리등을 함

            // Run : Form클래스의 인스턴스를 등록하게 되면
            //       윈도우 메시지의 처리 루틴 동작하게 됨
            Application.Run(new Form());
            //System.Windows.Forms.Application.Run(new System.Windows.Forms.Form());
        }
        
        //속성을 통해 Form의 정보를 변경할 수 있다.
        static void Exam3()
        {
            Form obj = new Form();
            obj.Text = "Form클래스를 이용한 윈폼";   // 제목표시줄에 글자 출력
            obj.SetBounds(10, 10, 300, 150);        // 윈폼 크기 지정
            obj.MaximizeBox = false;                // 윈도우 버튼 속성 지정
            obj.StartPosition = FormStartPosition.CenterScreen; // 초기 출력위치 
                                                                // 윈폼 크기 지정시 초기 출력위치가 변경됨
            Application.Run(obj);
        }


        static string[] strText = { "빨", "주", "노", "초", "파", "남", "보" };
        static void Exam4()
        {
            //Form 저장할 수 있는 배열 7개 정의
            Form[] wnd = new Form[7];

            //Form 객체를 7개 생성
            for (int i = 0; i < wnd.Length; i++)
            {
                wnd[i] = new Form();
                wnd[i].Text = strText[i];
                wnd[i].SetBounds((i + 1) * 10, (i + 1) * 10, 300, 100);
                wnd[i].MaximizeBox = false;
                wnd[i].Show();
                Console.WriteLine("{0} 번째 윈도우 출력 성공!!!", i);
            }

            Application.Run(wnd[0]);
            // 창이 여러개일 경우에도 Run메서드에 등록하는 Form 개체 인스턴스는
            //  한번만 설정하면 됨

            // 첫번째 윈도우를 등록... : 등록된 윈도우만 메시지루프가 구동됨
            // 이를 주석처리하면 Show되었다가 사라짐
            // 등록된 놈을 제거하면 모두 제거됨

        }

        #region Form의 파생클래스 생성
        //Form : 라이브러리 클래스이기 때문에  
        //Form을 상속받아 파생클래스로 사용한다.
        class MyForm : Form
        {
            //확장!
            public MyForm(string msg)
            {
                this.Text = msg;     //타이틀바 변경

                this.Top = 10;
                this.Left = 10;
                this.Width = 250;
                this.Height = 200;

                this.MaximizeBox = false;
                this.MinimizeBox = false;

                this.Show();
            }
        }

        static void Exam5()
        {
            Application.Run(new MyForm("첫번째 내가 만든 Form"));
        }
        #endregion

        #region 이벤트 처리
        // - 폼 객체생성,보이기 전  : Load       (WM_CREATE/ WM_INITDIALOG)   
        // - 폼 객체종료,사라지기 전: FormClosed(WM_DEST ROYWINDOW / WM_CLOSE) 
        // - 폼 객체종료 전         : FormClosing
        class Exam6_Form : Form
        {
            //1. 자신의 속성 초기화
            //2. 이벤트 등록
            //3. 자식 컨트롤 객체 생성 
            public Exam6_Form()
            {
                this.Load       += new EventHandler(Exam6_Form_Load);
                this.FormClosed += Exam6_Form_FormClosed;
                this.FormClosing += Exam6_Form_FormClosing;
            }

            private void Exam6_Form_FormClosing(object sender, FormClosingEventArgs e)
            {
                DialogResult r = MessageBox.Show("FormClosing이벤트", "타이틀바",MessageBoxButtons.OKCancel);
                if(r == DialogResult.Cancel)
                    e.Cancel = true;  //종료 취소하겠다.
            }

            private void Exam6_Form_FormClosed(object sender, FormClosedEventArgs e)
            {               
                MessageBox.Show("FormClosed이벤트");
            }

            private void Exam6_Form_Load(object sender, EventArgs e)
            {                
                MessageBox.Show("Load이벤트");
            }
        }
        static void Exam6()
        {
            Application.Run(new Exam6_Form());
        }
        #endregion 
        static void Main(string[] args)
        {
            Exam6();
        }
    }
}
