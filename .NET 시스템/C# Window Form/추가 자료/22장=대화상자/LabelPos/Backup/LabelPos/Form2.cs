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
    {
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
        public string LabelText
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