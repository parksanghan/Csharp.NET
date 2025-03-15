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
        private Form2 dlg;

        public Form1()
        {
            InitializeComponent();
        }

        private void OnApply(object sender, EventArgs e)
        {
            // 자식의 정보-> 부모 정보에 접근해서 변경 
            label1.Left = dlg.LabelX;
            label1.Top = dlg.LabelY;
            label1.Text = dlg.LabelText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dlg == null || dlg.IsDisposed)
            {
                dlg = new Form2();
                dlg.Owner = this;//부모전달 
                dlg.Apply += new EventHandler(OnApply); // 이벤트 핸들러 등록 

                // 부모 정보 -> 자식 전달
                dlg.LabelX = label1.Left;
                dlg.LabelY = label1.Top;
                dlg.LabelText = label1.Text;
                dlg.Show();
            }
        }
    }
}
