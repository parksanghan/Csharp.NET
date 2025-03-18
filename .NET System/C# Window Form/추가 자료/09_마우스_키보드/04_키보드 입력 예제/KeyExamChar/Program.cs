using System;
using System.Drawing;
using System.Windows.Forms;

/*
 *  Control 클래스의 KeyPress
 * 
 *  키관련 이벤트의 호출 순서
 *    1) KeyDown
 *    2) keyPress
 *    3) keyUp
 * 
 *    KeyDown 이나 Keyup은 문자, 비문자,토글, Shift키등이 눌리면 모두 호출됨
 * 
 *    KeyPress는 문자키가 눌릴 때만 발생함
 * 
 *  KeyPressEventArgs 클래스 
 *    KeyPress가 호출되었을 때 매개변수로 전달됨
 *    속성
 *       KeyChar    char타입    유니코드 문자
 *       Handled    bool        KeyPress이벤트가 처리되었는지 여부를 나타내는 값을 
 *                              가져오거나 설정
 */
namespace KeyExamChar
{
    public class KeyExamChar : Form
    {
        public static void Main()
        {
            Application.Run(new KeyExamChar());
        }

        string strdata = " "; // 화면에 출력할 문자
        public KeyExamChar()
        {
            this.Text = "문자 입력 예제";
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;
            this.Size = new Size(500, 300);

            this.KeyDown += new KeyEventHandler(KeyDownFunc);
            this.KeyUp += new KeyEventHandler(KeyUpFunc);
            this.KeyPress += new KeyPressEventHandler(KeyPressFunc);
        }

        private void KeyDownFunc(object sender, KeyEventArgs key)
        {
            strdata += String.Format("\n ① {0} 키가 눌림*****", key.KeyCode);
            Invalidate();
        }

        private void KeyUpFunc(object sender, KeyEventArgs key)
        {
            strdata += String.Format("\n ③ {0} 키가 놓임*****", key.KeyCode);
            Invalidate();
        }

        private void KeyPressFunc(object sender, KeyPressEventArgs key)
        {
            strdata += String.Format("\n ② 문자키 {0} 눌림*****", key.KeyChar);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString(strdata, new Font("궁서체", 20, FontStyle.Bold), Brushes.White, 20, 30);
        }
    }
}