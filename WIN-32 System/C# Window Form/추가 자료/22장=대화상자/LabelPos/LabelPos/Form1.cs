using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabelPos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 모달 다이얼로그 실행코드 
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2(); // 모달 폼 객체 생성
            dlg.LabelX = label1.Left; 
            dlg.LabelY = label1.Top;
            dlg.LabelText = label1.Text;

            if (dlg.ShowDialog() == DialogResult.OK) // 모달이 종료가 ok 이면 리턴값 반환
            {
                label1.Left = dlg.LabelX;   // 값을 원본에 적용 
                label1.Top = dlg.LabelY;
                label1.Text = dlg.LabelText;
                this.close(dlg);// 폼을 죽이기 원한다면 
                // 폼죽이기 위한 함수 
            }
            dlg.Dispose();
        }
    }
}