using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataObject data = new DataObject();
            data.SetData(DataFormats.Text, textBox1.Text);
			string html = "<p>" + textBox1.Text + "</p>";
            data.SetData(DataFormats.Html, html);
            Clipboard.SetDataObject(data, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Text))
            {
                textBox2.Text = (string)data.GetData(DataFormats.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Html))
            {
                textBox3.Text = (string)data.GetData(DataFormats.Html);
            }

        }
    }
}