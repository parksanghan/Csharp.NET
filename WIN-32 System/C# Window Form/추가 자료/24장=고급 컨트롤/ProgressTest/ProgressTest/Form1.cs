using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProgressTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                progressBar1.Value = i;
                //progressBar1.PerformStep();
                System.Threading.Thread.Sleep(30);
            }
            MessageBox.Show("작업을 완료했습니다.");
            progressBar1.Value = 0;
        }
    }
}