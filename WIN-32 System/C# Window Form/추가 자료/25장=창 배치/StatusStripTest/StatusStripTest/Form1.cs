using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatusStripTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.X.ToString();
            toolStripStatusLabel2.Text = e.Y.ToString();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    toolStripStatusLabel3.Text = "왼쪽 버튼을 눌렀습니다.";
                    break;
                case MouseButtons.Middle:
                    toolStripStatusLabel3.Text = "가운데 버튼을 눌렀습니다.";
                    break;
                case MouseButtons.Right:
                    toolStripStatusLabel3.Text = "오른쪽 버튼을 눌렀습니다.";
                    break;
            }
        }
    }
}