using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabelPos
{
    public partial class Form2 : Form
    {   // 포로퍼티 코드 -> 컨트롤과 속성을 연결 
        public int LabelX
        {
            get { return Convert.ToInt32(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }
        public int LabelY
        {
            get { return Convert.ToInt32(textBox2.Text); }
            set { textBox2.Text = value.ToString(); }
        }
        public string LabelText // 이 프로퍼티로 textbox3.의 문자열을 get , set 
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public Form2()
        {
            InitializeComponent();
        }
    }
}