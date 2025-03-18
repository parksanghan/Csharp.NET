using System;
using System.Drawing;
using System.Windows.Forms;

/*
 * 키보드로 부터 입력받는 키 구분
 *  
 *  1) 문자키 : 문자, 숫자 , Space, Tab, BackSpace, Esc
 * 
 *  2) 비문자키 : 방향키, 함수키, 삭제, 삽입 키(문자 입력과 직접적인 관계가 없는 키들)
 * 
 *  3) 토글키 : Caps Lock, Num Lock, Scroll Lock, Insert
 * 
 *  4) 쉬프트 키 : Shift, Alt, Ctrl등으로 다른 키나 마우스키 등과 조합되어 사용
 * 
 * 키 입력 이벤트
 * 
 *   KeyDown    OnKeyDown   컨트롤에 포커스가 있을 때 키를 누르면 발생
 *   KeyUp      OnKeyUp     컨트롤에 포커스가 있을 때 키를 놓으면 발생
 * 
 * 
 * KeyEventArgs 클래스 속성(키 입력과 함께 전달되는 객체)
 * 
 *   속성       자료형  설명
 *   ----------------------------------------------------------------
 *   Alt        bool    Alt키 상태
 *   Control    bool    Ctrl키 상태
 *   Handled    bool    이벤트 처리 여부 반환 및 설정
 *   KeyCode    keys    keyDown 또는 keyup이벤트에 대한 키보드 코드 반환
 *   KeyData    keys    누른 키에 대한 키 코드와 동시에 누른 Ctrl, Alt및 
 *                      Shift 키 조합을 나타내는 조합 반환
 *   KeyValue   int     keyCode 속성의 정수 값 반환
 *   Modifiers  keys    하나 이상의 보조키(함께 누른 키)가 플래그 반환
 *   Shift      bool    Shift 키가 눌렸는지 여부 반환
 *   SuppresskeyPress   bool    키 이벤트를 내부 컨트롤에 전달할지 여부 설정 
 *
 */


public class KeyExam : Form
{
    public static void Main()
    {
        Application.Run(new KeyExam());
    }

    string strdata = " "; // 화면에 출력할 문자
    public KeyExam()
    {
        this.Text = "키보드 입력 예제";
        this.BackColor = Color.Black;
        this.ForeColor = Color.White;
    }
    protected override void OnKeyDown(KeyEventArgs key)
    {
        if (key.KeyCode == Keys.Back) // 백스페이스 키이면, 입력한 글자 지움
        {
            if (strdata.Length > 0)
            {
                strdata = strdata.Remove(strdata.Length - 1); // 마지막 문자 지움
            }
            Invalidate();
        }
        else if (key.KeyCode == Keys.Enter)
        {
            strdata += "\r\n";
            Invalidate();
        }
        else if (key.KeyCode == Keys.Space)
        {
            strdata += " ";
            Invalidate();
        }

        // 0~9 사이의 키가 들어오면 KeyCode는 D0~D9로 들어오게 됨: 숫자로 변환
        // 숫자 코드 48부터 0으로 정의되어 있슴
        else if (key.KeyCode <= Keys.D9 && key.KeyCode >= Keys.D0)
        {
            strdata += (key.KeyValue - 48);
            Invalidate();
        }
        else if (key.KeyCode == Keys.F1)
        {
            MessageBox.Show("프로그램 도움말!");
        }
        else if ((key.KeyCode == Keys.F10) && (key.Control))  // ctrl+F10은 종료
        {
            MessageBox.Show("프로그램을 종료합니다.");
            this.Close();
        }
        else
        {
            // A~Z 사이의 대문자만 정상 출력 가능
            strdata += key.KeyCode;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.DrawString(strdata, new Font("궁서체", 20, FontStyle.Bold), Brushes.White, 20, 30);
    }
}