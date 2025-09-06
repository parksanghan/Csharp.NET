using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EnumClip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] cf = Clipboard.GetDataObject().GetFormats();

            listBox1.Items.Clear();
            foreach (string f in cf)
            {
                listBox1.Items.Add(f);
            }
        }
    }
}