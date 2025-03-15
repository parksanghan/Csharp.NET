using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToolTipTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "이것은 버튼입니다.");
            toolTip1.SetToolTip(checkBox1, "진위적인 옵션을 선택합니다.");
            toolTip1.SetToolTip(textBox1, "문자열을 입력합니다.");
            toolTip1.SetToolTip(radioButton1, "여러 개의 옵션 중\r\n 하나를 선택합니다.");
            toolTip1.SetToolTip(label1, "단순한 문자열입니다.");
            toolTip1.SetToolTip(listBox1, "목록을 표시하는 컨트롤입니다.");
        }
    }
}