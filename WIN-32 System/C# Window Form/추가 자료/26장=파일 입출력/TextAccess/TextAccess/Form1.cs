using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TextAccess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"c:\cstest.txt");
            sw.Write(textBox1.Text);
            sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            char[] buf = new char[1024];
            int ret;
            StreamReader sr = new StreamReader(@"c:\cstest.txt");
            textBox1.Text = "";
            while (true)
            {
                ret = sr.Read(buf, 0, 1024);
                textBox1.Text += new string(buf,0,ret);
                if (ret < 1024) break;
            }
            sr.Close();
        }
    }
}