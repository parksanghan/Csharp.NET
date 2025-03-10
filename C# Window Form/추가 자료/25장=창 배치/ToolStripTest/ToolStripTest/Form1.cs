using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToolStripTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("툴바의 첫 번째 버튼을 눌렀습니다.");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (toolStripButton2.Checked)
            {
                BackColor = Color.Red;
            }
            else
            {
                BackColor = DefaultBackColor;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Text = toolStripTextBox1.Text;
        }
    }
}