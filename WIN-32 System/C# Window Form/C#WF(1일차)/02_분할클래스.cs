//01_분할클래스.cs
using System;
using System.Windows.Forms;

//Partial Types 윈도우 작성
/*
1) 이름이있는 공간으로 구분!(파일개념이 아님)
2) 하나의 클래스를 2개이상의 파일로 분할해서 작성 가능
   단, namespace가 동일해야 함
   UI부분[디자인-자동생성] / 이벤트 처리 부분[코딩-직접구현]
*/
namespace _1016_Form1
{
    internal class _01_분할클래스
    {
        public static void Main(string[] args)
        {
            Application.Run(new MyClass1());
        }
    }
}
