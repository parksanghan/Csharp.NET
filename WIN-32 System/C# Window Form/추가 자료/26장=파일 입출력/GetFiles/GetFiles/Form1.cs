using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GetFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] Files = Directory.GetFiles(Environment.SystemDirectory, "*.dll", 
                SearchOption.TopDirectoryOnly);
            foreach (string Name in Files)
            {
                listBox1.Items.Add(Name);
            }
        }
    }
}