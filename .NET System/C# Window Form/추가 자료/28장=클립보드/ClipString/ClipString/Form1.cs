using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClipString
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(textBox1.Text);
            Clipboard.SetDataObject(textBox1.Text, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            if (Clipboard.ContainsText())
            {
                textBox2.Text = Clipboard.GetText();
            }
            //*/

            //*
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(string)))
            {
                textBox2.Text = (string)data.GetData(typeof(string));
            }
            //*/
        }
    }
}

