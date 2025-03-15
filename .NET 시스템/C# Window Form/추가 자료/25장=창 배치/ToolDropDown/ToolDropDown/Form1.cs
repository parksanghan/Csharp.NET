using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ToolDropDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 빨간색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Red;
        }

        private void 초록색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Green;
        }

        private void 파란색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.Blue;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                toolStripProgressBar1.Value = i;
                System.Threading.Thread.Sleep(50);
            }
            toolStripProgressBar1.Value = 0;
        }
    }
}